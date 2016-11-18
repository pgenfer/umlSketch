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