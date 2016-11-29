namespace UmlSketch.Event
{
    public class PropertyTypeChangedEvent : TypeChangedEventBase
    {
        public PropertyTypeChangedEvent(string nameOfOldType, string nameOfNewType) : 
            base(nameOfOldType, nameOfNewType)
        {
        }
    }
}