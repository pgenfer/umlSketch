namespace UmlSketch.Event
{
    public class ChangeParameterTypeEvent : TypeChangedEventBase
    {
        public ChangeParameterTypeEvent(string nameOfOldType, string nameOfNewType) : 
            base(nameOfOldType, nameOfNewType)
        {
        }
    }
}
