namespace Yuml.Event
{
    /// <summary>
    /// fired when an interface was removed from the classifier's interface list
    /// </summary>
    public class RemoveInterfaceFromClassEvent : IDomainEvent
    {
        public string InterfaceName { get; }

        public RemoveInterfaceFromClassEvent(string interfaceName)
        {
            InterfaceName = interfaceName;
        }
    }
}