namespace Yuml.Command
{
    public class MethodReturnTypeChanged : TypeChangedEventBase
    {
        public MethodReturnTypeChanged(string nameOfOldType, string nameOfNewType)
            :base(nameOfOldType,nameOfNewType)
        {
        }
    }
}