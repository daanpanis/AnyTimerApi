using GraphQL.Types;

namespace AnyTimerApi.GraphQL.InputTypes
{
    public class AnyTimerSenderInputType : InputObjectGraphType
    {
        public AnyTimerSenderInputType()
        {
            Name = "AnyTimerSenderInput";
            Field<NonNullGraphType<StringGraphType>>("user");
            Field<NonNullGraphType<UIntGraphType>>("amount");
        }
    }
}