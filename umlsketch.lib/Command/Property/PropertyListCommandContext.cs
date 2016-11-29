using UmlSketch.DomainObject;
using UmlSketch.Event;

namespace UmlSketch.Command
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
            :base(properties,classifiers,messageSystem)
        {
        }
    }
}
