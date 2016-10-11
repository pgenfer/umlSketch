namespace Yuml.Command
{
    internal class MethodListCommandContext : ListCommandContextBase<Method>
    {
        private readonly IMethodNameValidationService _nameValidation;
        private readonly MessageSystem _messageSystem;

        public MethodListCommandContext(
            Classifier classifier,
            ClassifierDictionary availableClassifiers,
            IMethodNameValidationService nameValidation,
            MessageSystem messageSystem)
        {
            _nameValidation = nameValidation;
            _messageSystem = messageSystem;

            All = new Query<Method>(() => classifier.Methods);
            Visibility = new ShowOrHideAllObjectsInListCommand(classifier.Methods, messageSystem);
            New = new NewMethodCommand(classifier, availableClassifiers, messageSystem);
        }

        public override ISingleCommandContext GetCommandsForSingleItem(Method domainObject) =>
            new MethodSingleCommandContext(domainObject, _nameValidation, _messageSystem);
       
    }
}
