using NSubstitute;
using UmlSketch.Command;
using UmlSketch.DomainObject;
using UmlSketch.Service;
using static NSubstitute.Substitute;

namespace UmlSketch.Test
{
    
    public class MakeClassifierToInterfaceCommandTest : TestBase
    {
        private readonly Classifier _classifier = new Classifier("TestClass");
        private IRelationService _relationService;

        protected override void Init()
        {
           
            _relationService = For<IRelationService>();
        }

        [TestDescription("Check that correct event is fired when changing interface to class")]
        public void ToggleInterfaceFlagTest_FromInterfaceToClass()
        {
            _classifier.IsInterface = true;
            var command = new MakeClassifierToInterfaceCommand(_classifier, _relationService);
            command.ToggleInterfaceFlag();
            _relationService.Received().ChangeFromInterfaceToClass(_classifier);
        }

        [TestDescription("Check that correct event is fired when changing class to interface")]
        public void ToggleInterfaceFlagTest_FromClassToInterface()
        {
            _classifier.IsInterface = false;
            var command = new MakeClassifierToInterfaceCommand(_classifier, _relationService);
            command.ToggleInterfaceFlag();
            _relationService.Received().ChangeFromClassToInterface(_classifier);
        }
    }
}