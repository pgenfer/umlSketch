namespace Yuml.Command
{
    /// <summary>
    /// all commands that can be executed on a list of properties
    /// </summary>
    public class PropertyListCommandContext : ListCommandContextBase<Property>
    {
        public PropertyListCommandContext(
            PropertyList properties,
            ClassifierDictionary classifiers,
            MessageSystem messageSystem)
        {
            All = new Query<Property>(() => properties);
            New = new NewPropertyCommand(classifiers, properties, messageSystem);
            Visibility = new ShowOrHideAllObjectsInListCommand(properties, messageSystem);
        }
    }
}
