using System.Collections.Generic;
using GraphQL.Types;

namespace AnyTimerApi.GraphQL.InputTypes
{
    public class AnyTimerInputType : InputObjectGraphType<AnyTimerInputType>
    {
        public IList<AnyTimerSenderInputType> Senders { get; set; }
        public string Reason { get; set; }

        public AnyTimerInputType()
        {
            Name = "AnyTimerInput";
            Field<NonNullGraphType<ListGraphType<AnyTimerSenderInputType>>>("senders");
            Field(o => o.Reason);
        }
    }
}