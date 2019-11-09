using AnyTimerApi.Database.Entities;
using GraphQL.Types;

namespace AnyTimerApi.GraphQL.Types
{
    public class StatusEventType : ObjectGraphType<StatusEvent>
    {
        public StatusEventType()
        {
            Name = "StatusEvent";
            Field(e => e.Status);
            Field(e => e.EventTime);
        }
    }
}