using Common;

namespace Yuml
{
    /// <summary>
    /// event that is fired when a domain object changes its visibility
    /// </summary>
    public class VisibilityChangedEvent : IDomainEvent
    {
        private readonly IVisible _visibleObject;

        public VisibilityChangedEvent(IVisible visibleObject)
        {
            _visibleObject = visibleObject;
        }
    }
}