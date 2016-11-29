using UmlSketch.DomainObject;

namespace UmlSketch.Event
{
    public class ChangeAssociationTargetEvent : IDomainEvent
    {
        private readonly Relation _relation;
        private readonly Classifier _newTarget;

        public ChangeAssociationTargetEvent(Relation relation, Classifier newTarget)
        {
            _relation = relation;
            _newTarget = newTarget;
        }
    }
}
