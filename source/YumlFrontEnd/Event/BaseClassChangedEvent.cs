namespace UmlSketch.Event
{
    public class BaseClassChangedEvent : TypeChangedEventBase
    {
        public BaseClassChangedEvent(string nameOfOldType, string nameOfNewType) : 
            base(nameOfOldType, nameOfNewType)
        {
        }
    }
}
