namespace Yuml.Command
{
    public class MethodListCommandContext : ListCommandContextBase<Method>
    {
        public MethodListCommandContext(
            MethodList methods,
            ClassifierDictionary availableClassifiers,
            MessageSystem messageSystem)
        {
            All = new Query<Method>(() => methods);
            Visibility = new ShowOrHideAllObjectsInListCommand(methods, messageSystem);
            New = new NewMethodCommand(methods, availableClassifiers, messageSystem);
        }
    }
}
