namespace UmlSketch.Event
{
    /// <summary>
    /// event should be fired when the base class of a classifier
    /// is set to a new base class and the classifier did not have
    /// a base class before.
    /// </summary>
    public class BaseClassSetEvent : IDomainEvent
    {
        public string NameOfNewBaseClass { get; }

        public BaseClassSetEvent(string nameOfNewBaseClass)
        {
            NameOfNewBaseClass = nameOfNewBaseClass;
        }
    }
}
