using UmlSketch.DomainObject;
using UmlSketch.Event;

namespace UmlSketch.Command
{
    public class MethodListCommandContext : ListCommandContextBase<Method>
    {
        public MethodListCommandContext(
            MethodList methods,
            ClassifierDictionary availableClassifiers,
            MessageSystem messageSystem)
            :base(methods,availableClassifiers,messageSystem)
        {
        }
    }
}
