using NUnit.Framework;
using UmlSketch.DomainObject;

namespace UmlSketch.Test
{
    [TestFixture()]
    public class ImplementationListTest : TestBase
    {
        private ImplementationList _implementationList;
        private readonly Classifier _classifier = new Classifier("class");
        private readonly Classifier _interface = new Classifier("interface") { IsInterface = true };

        protected override void Init()
        {
            _implementationList = new ImplementationList();
        }


        [TestDescription(
            "Ensure that the implementation list of a class is not empty if there is an interface implementation")]
        public void ImplementationList_WithInterface_NotEmpty()
        {
            _implementationList.AddInterfaceToList(new Implementation(_classifier, _interface));
            var implementations = _implementationList.FindImplementationsOfInterface(_interface);

            Assert.IsTrue(implementations.IsNotEmpty());
        }

        [TestDescription(
            "Check that implementations are returned even if parameter is no interface (needed when changing interface to class)")]
        public void FindImplementationsOfInterfaceTest()
        {
            var noInterface = new Classifier("noInterface") {IsInterface = true};
            _implementationList.AddInterfaceToList(new Implementation(_classifier, noInterface));
            noInterface.IsInterface = false;
            var implementations = _implementationList.FindImplementationsOfInterface(noInterface);

            Assert.IsTrue(implementations.IsNotEmpty());
        }
    }
}