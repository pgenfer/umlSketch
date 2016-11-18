using Yuml.Service;

namespace Yuml.Command
{
    public class PropertySingleCommandContext : SingleCommandContextBase<Property>, ISinglePropertyCommands
    {
        public PropertySingleCommandContext(
            PropertyList properties,
            Property property,
            ClassifierDictionary availableClassifiers,
            IValidateNameService propertyValidationNameService,
            MessageSystem messageSystem,
            IAskUserBeforeDeletionService askUserBeforeDeletion):
            base(properties,property,messageSystem,askUserBeforeDeletion)
        {
            Rename = new RenameMemberCommand(
                property,
                propertyValidationNameService,
                messageSystem);
            ChangeTypeOfProperty = new ChangeTypeOfPropertyCommand(
                availableClassifiers,
                property,
                messageSystem);
        }

        public IChangeTypeCommand ChangeTypeOfProperty { get; }
    }
}
