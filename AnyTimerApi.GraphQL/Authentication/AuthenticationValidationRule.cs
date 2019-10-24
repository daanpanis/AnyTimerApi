using System.Linq;
using GraphQL;
using GraphQL.Language.AST;
using GraphQL.Types;
using GraphQL.Validation;

namespace AnyTimerApi.GraphQL.Authentication
{
    public class AuthenticationValidationRule : IValidationRule
    {
        public INodeVisitor Validate(ValidationContext context)
        {
            var userContext = context.UserContext as GraphQLUserContext;
            return new EnterLeaveListener(_ =>
            {
                var operationType = OperationType.Query;

                _.Match<Operation>(type =>
                {
                    operationType = type.OperationType;
                    CheckAuth(context.TypeInfo.GetLastType(), userContext, context, operationType);
                });

                _.Match<ObjectField>(type =>
                {
                    var argumentType = context.TypeInfo.GetArgument().ResolvedType.GetNamedType() as IComplexGraphType;
                    if (argumentType == null)
                        return;

                    var fieldType = argumentType.GetField(type.Name);
                    CheckAuth(fieldType, userContext, context, operationType);
                });

                _.Match<Field>(type =>
                {
                    var fieldDef = context.TypeInfo.GetFieldDef();

                    if (fieldDef == null) return;

                    CheckAuth(fieldDef, userContext, context, operationType);
                    CheckAuth(fieldDef.ResolvedType.GetNamedType(), userContext, context, operationType);
                });
            });
        }

        private static void CheckAuth(IProvideMetadata type,
            GraphQLUserContext userContext,
            ValidationContext context,
            OperationType operationType)
        {
            if (context.Errors.Any(error => error.Code.Equals("authentication")) || type == null ||
                !type.DoesRequireAuthentication())
            {
                return;
            }

            if (!(userContext?.User?.Identity != null && userContext.User.Identity.IsAuthenticated))
            {
                context.ReportError(new ValidationError(
                    context.OriginalQuery,
                    "authentication",
                    $"You must be authenticated to run this {operationType.ToString().ToLower()}."
                ));
            }
        }
    }
}