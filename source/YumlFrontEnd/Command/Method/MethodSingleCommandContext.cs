using UmlSketch.DomainObject;
using UmlSketch.Event;
using UmlSketch.Service;
using UmlSketch.Validation;

namespace UmlSketch.Command
{
    public class MethodSingleCommandContext : SingleCommandContextBase<Method>
    {
        public MethodSingleCommandContext(
            MethodList methods,
            Method method,
            ClassifierDictionary availableClassifiers,
            IMethodNameValidationService validateName,
            MessageSystem messageSystem,
            IAskUserBeforeDeletionService askUserBeforeDeletion):base(methods,method,messageSystem, askUserBeforeDeletion)
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
