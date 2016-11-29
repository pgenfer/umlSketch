using UmlSketch.DomainObject;

namespace UmlSketch.Event
{
    /// <summary>
    /// event is fired when the type of an association changes
    /// </summary>
    public class ChangeAssocationTypeEvent : IDomainEvent
    {
        public Relation Relation { get; }

        public ChangeAssocationTypeEvent(Relation relation)
        {
            Relation = relation;
        }
    }
}
