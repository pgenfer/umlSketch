using NUnit.Framework;
using Yuml.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using Yuml.Test;
using static NSubstitute.Substitute;

namespace Yuml.Command.Test
{
    public class NewInterfaceCommandTest : SimpleTestBase
    {
        [TestDescription("Check that correct event is fired if interface was created")]
        public void CreateNewInterfaceImplementation()
        {
            var implementations = For<ImplementationList>();
            var classifiers = For<IList<Classifier>>();
            var messageSystem = For<MessageSystem>();
            var implementation = For<Implementation>();
            implementations.AddNewImplementation(classifiers).Returns(implementation);

            var newCommand = new NewInterfaceCommand(implementations, classifiers, messageSystem);
            newCommand.CreateNew();

            messageSystem.Received().PublishCreated(implementations, implementation);
        }
    }
}