using UmlSketch.DomainObject;
using UmlSketch.Event;

namespace UmlSketch.Command
{
    public class ListParameterContext : ListCommandContextBase<Parameter>
    {
        public ListParameterContext(
            ParameterList parameters,
            ClassifierDictionary classifiers,
            MessageSystem messageSystem)
            :base(parameters,classifiers,messageSystem)
        {
        }
    }
}
