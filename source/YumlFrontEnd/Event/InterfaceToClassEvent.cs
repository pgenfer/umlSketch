namespace Yuml
{
    /// <summary>
    /// fired when an interface was changed to a class
    /// </summary>
    public class InterfaceToClassEvent : IDomainEvent
    {
        public string InterfaceName { get;}

        public InterfaceToClassEvent(string interfaceName)
        {
            InterfaceName = interfaceName;
        }
    }
}