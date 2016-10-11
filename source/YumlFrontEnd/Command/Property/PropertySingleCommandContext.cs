namespace Yuml.Command
{
    public class PropertySingleCommandContext : SingleCommandContextBase, ISinglePropertyCommands
    {
        public PropertySingleCommandContext(
            ClassifierDictionary availableClassifiers,
            Property property,
            IValidateNameService propertyValidationNameService,
            MessageSystem messageSystem)
        {
            Rename = new RenameMemberCommand(
                property,
                propertyValidationNameService,
                messageSystem);
            ChangeTypeOfProperty = new ChangeTypeOfPropertyCommand(
                availableClassifiers,
                property,
                messageSystem);
            Visibility = new ShowOrHideSingleObjectCommand(property, messageSystem);
        }

        public IChangeTypeCommand ChangeTypeOfProperty { get; }
    }
}
