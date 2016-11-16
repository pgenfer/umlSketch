namespace Yuml.Command
{
    public class MethodSingleCommandContext : SingleCommandContextBase<Method>
    {
        public MethodSingleCommandContext(
            MethodList methods,
            Method method,
            ClassifierDictionary availableClassifiers,
            IMethodNameValidationService validateName,
            MessageSystem messageSystem):base(methods,method,messageSystem)
        {
            Rename = new RenameMethodCommand(
                method,
                validateName,
                messageSystem);
            ChangeReturnType = new ChangeMethodReturnTypeCommand(
                availableClassifiers,
                method,
                messageSystem);
        }

        public IChangeTypeCommand ChangeReturnType { get;}
    }
}
