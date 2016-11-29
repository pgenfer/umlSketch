using UmlSketch.DomainObject;
using UmlSketch.Event;
using UmlSketch.Service;

namespace UmlSketch.Command
{
    public class SingleInterfaceCommandContext : SingleCommandContextBase<Implementation>
    {
        public ChangeInterfaceOfClassifierCommand ChangeInterface { get; }
        public QueryAvailableInterfaces AvailableInterfaces { get; }

        public SingleInterfaceCommandContext(
            ImplementationList implementationList,
            Implementation existingInterface,
            ClassifierDictionary classifiers,
            MessageSystem messageSystem, IAskUserBeforeDeletionService askUserBeforeDeletion) :
            base(implementationList, existingInterface, messageSystem, askUserBeforeDeletion)
        {
            ChangeInterface = new ChangeInterfaceOfClassifierCommand(
                existingInterface,
                classifiers,
                messageSystem);
            AvailableInterfaces = new QueryAvailableInterfaces(existingInterface, classifiers);
        }
    }
}

