namespace UmlSketch.Event
{
    public class DomainObjectCreatedEvent<T> : IDomainEvent
    {
        /// <summary>
        /// the domain object that was removed
        /// </summary>
        public T DomainObject { get; }

        /// <summary>
        /// creates the event
        /// </summary>
        /// <param name="domainObject">domain object that was deleted</param>
        public DomainObjectCreatedEvent(T domainObject)
        {
            DomainObject = domainObject;
        }
    }
}
