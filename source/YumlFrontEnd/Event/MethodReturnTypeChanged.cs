namespace UmlSketch.Event
{
    public class MethodReturnTypeChanged : TypeChangedEventBase
    {
        public MethodReturnTypeChanged(string nameOfOldType, string nameOfNewType)
            :base(nameOfOldType,nameOfNewType)
        {
        }
    }
}