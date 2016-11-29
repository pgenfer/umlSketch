using NUnit.Framework;
using System.Linq;
using UmlSketch.DomainObject;

namespace UmlSketch.Test
{
    public class SerializationMixinTest : TestBase
    {
        [TestDescription("Check that system types are loaded after diagram was reset")]
        public void ResetDiagram_SystemTypesAvailable()
        {
            var diagram = new Diagram();
            diagram.Reset();
            Assert.AreEqual(new SystemTypes().Count(),diagram.Classifiers.Count);
        }
    }
}