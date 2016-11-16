namespace Yuml.Command
{
    public class PropertySingleCommandContext : SingleCommandContextBase<Property>, ISinglePropertyCommands
    {
        public PropertySingleCommandContext(
            PropertyList properties,
            Property property,
            ClassifierDictionary availableClassifiers,
            IValidateNameService propertyValidationNameService,
            MessageSystem messageSystem):base(properties,property,messageSystem)
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
