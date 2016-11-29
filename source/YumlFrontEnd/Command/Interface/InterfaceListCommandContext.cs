using UmlSketch.DomainObject;
using UmlSketch.Event;

namespace UmlSketch.Command
{
    public class InterfaceListCommandContext : ListCommandContextBase<Implementation>
    {
        public InterfaceListCommandContext(
            ImplementationList implementations,
            ClassifierDictionary classifiers,
            MessageSystem messageSystem )
            :base(implementations,classifiers,messageSystem)
        {
            // TODO: check if visibility is also changed if interface was later added to list
        }
    }
}
