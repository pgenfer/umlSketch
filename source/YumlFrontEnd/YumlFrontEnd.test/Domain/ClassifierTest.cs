using NUnit.Framework;
using Yuml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using static NSubstitute.Substitute;

namespace Yuml.Test
{
    public class ClassifierTest : TestBase
    {
        [TestDescription("Ensure that implementation root is set")]
        public void Classifier_InterfaceImplementations_SetRoot()
        {
            var classifier = new Classifier();
            var implementations = For<ImplementationList>();

            classifier.InterfaceImplementations = implementations;

            implementations.Received().Root = classifier;
        }
    }
}