using NUnit.Framework;
using Yuml.Command;
using NSubstitute;
using static NSubstitute.Substitute;

namespace Yuml.Test
{
    public class RenameClassifierCommandTest : SimpleTestBase
    {
        private ClassifierNotificationService _notificationService;
        private IValidateNameService _validationService;
        private ClassifierDictionary _classifierDictionary;
        private IRenameCommand _renameCommand;
        private Classifier _classifier;
        private string _newName;


        [SetUp]
        public void Init()
        {
            _notificationService = For<ClassifierNotificationService>();
            _validationService = For<IValidateNameService>();

            _classifierDictionary = For<ClassifierDictionary>();
            _classifier = new Classifier("Integer");

            _renameCommand = new RenameClassifierCommand(
                _classifier, 
                _classifierDictionary,
                _validationService,
                _notificationService);

            _newName = RandomString();
        }

        [TestDescription("Rename command should change dictionary")]
        public void RenameClassifier()
        {
            _renameCommand.Rename(_newName);
            _classifierDictionary.Received().RenameClassifier(_classifier, _newName);
        }

        [TestDescription("Rename command should notify listeners")]
        public void RenameClassifier_Notify()
        {
            var oldName = _classifier.Name;
            _renameCommand.Rename(_newName);
            _notificationService.Received().FireNameChange(oldName, _newName);
        }
    }
}