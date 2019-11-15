using GraphQL.Types;

namespace AnyTimerApi.GraphQL.InputTypes
{
    public class AnyTimerSenderInputType : InputObjectGraphType<AnyTimerSenderInputType>
    {
        public string User { get; set; }
        public uint Amount { get; set; }

        public AnyTimerSenderInputType()
        {
            Name = "AnyTimerSenderInput";
            Field(o => o.User);
            Field(o => o.Amount);
        }
    }
}