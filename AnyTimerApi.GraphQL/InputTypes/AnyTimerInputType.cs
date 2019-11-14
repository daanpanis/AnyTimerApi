using GraphQL.Types;

namespace AnyTimerApi.GraphQL.InputTypes
{
    public class AnyTimerInputType : InputObjectGraphType
    {
        public AnyTimerInputType()
        {
            Name = "AnyTimerInput";
            Field<NonNullGraphType<StringGraphType>>("receiver");
            Field<NonNullGraphType<ListGraphType<AnyTimerSenderInputType>>>("senders");
            Field<NonNullGraphType<StringGraphType>>("reason");
        }
    }
}