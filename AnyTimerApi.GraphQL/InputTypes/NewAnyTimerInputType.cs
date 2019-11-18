using GraphQL.Types;

namespace AnyTimerApi.GraphQL.InputTypes
{
    public class NewAnyTimerInputType : AnyTimerInputType
    {
        public string Receiver { get; set; }

        public NewAnyTimerInputType()
        {
            Name = "EditAnyTimerInput";
            Field<NonNullGraphType<StringGraphType>>("receiver");
            Field<NonNullGraphType<ListGraphType<AnyTimerSenderInputType>>>("senders");
        }
    }
}