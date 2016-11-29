namespace UmlSketch.Event
{
    /// <summary>
    /// fired when the classifier with the given name
    /// was changed to an interface
    /// </summary>
    public class ClassToInterfaceEvent : IDomainEvent
    {
        public string ClassName { get; }

        public ClassToInterfaceEvent(string className)
        {
            ClassName = className;
        }
    }
}
