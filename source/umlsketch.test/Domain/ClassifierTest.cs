using NSubstitute;
using UmlSketch.DomainObject;
using static NSubstitute.Substitute;

namespace UmlSketch.Test
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