using NSubstitute;
using UmlSketch.Command;
using UmlSketch.DomainObject;
using UmlSketch.Event;
using UmlSketch.Test;
using static NSubstitute.Substitute;

namespace Yuml.Command.Test
{
    public class NewInterfaceCommandTest : SimpleTestBase
    {
        [TestDescription("Check that correct event is fired if interface was created")]
        public void CreateNewInterfaceImplementation()
        {
            var implementations = For<ImplementationList>();
            var classifiers = new ClassifierDictionary();
            var messageSystem = For<MessageSystem>();
            var implementation = For<Implementation>();
            implementations.CreateNew(classifiers).Returns(implementation);

            var newCommand = new NewCommand<Implementation>(implementations, classifiers, messageSystem);
            newCommand.CreateNew();

            messageSystem.Received().PublishCreated(implementations, implementation);
        }
    }
}