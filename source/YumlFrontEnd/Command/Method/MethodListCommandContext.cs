namespace Yuml.Command
{
    public class MethodListCommandContext : ListCommandContextBase<Method>
    {
        public MethodListCommandContext(
            MethodList methods,
            ClassifierDictionary availableClassifiers,
            MessageSystem messageSystem)
            :base(methods,availableClassifiers,messageSystem)
        {
        }
    }
}
