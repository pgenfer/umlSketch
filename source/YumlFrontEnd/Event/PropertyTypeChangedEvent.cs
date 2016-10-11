namespace Yuml
{
    public class PropertyTypeChangedEvent : TypeChangedEventBase
    {
        public PropertyTypeChangedEvent(string nameOfOldType, string nameOfNewType) : 
            base(nameOfOldType, nameOfNewType)
        {
        }
    }
}