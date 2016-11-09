namespace Yuml.Command
{
    public class MethodSingleCommandContext : SingleCommandContextBase<Method>
    {
        public MethodSingleCommandContext(
            Method method,
            IMethodNameValidationService validateName,
            MessageSystem messageSystem)
        {
            Rename = new RenameMethodCommand(
                method,
                validateName,
                messageSystem);
            Visibility = new ShowOrHideSingleObjectCommand(method, messageSystem);
        }
    }
}
