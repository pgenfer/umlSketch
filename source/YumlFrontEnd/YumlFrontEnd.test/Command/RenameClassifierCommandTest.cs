using NSubstitute;
using UmlSketch.Command;
using UmlSketch.DomainObject;
using UmlSketch.Event;
using UmlSketch.Validation;
using static NSubstitute.Substitute;

namespace UmlSketch.Test
{
    public class RenameClassifierCommandTest : SimpleTestBase
    {
        private IValidateNameService _validationService;
        private ClassifierDictionary _classifierDictionary;
        private IRenameCommand _renameCommand;
        private Classifier _classifier;
        private MessageSystem _messageSystem;
        private string _newName;

        
        protected override void Init()
        {
            _validationService = For<IValidateNameService>();
            _messageSystem = For<MessageSystem>();

            _classifierDictionary = CreateDictionaryWithoutSystemTypes();
            _classifier = new Classifier("Integer");

            _renameCommand = new RenameClassifierCommand(
                _classifier, 
                _classifierDictionary,
                _validationService,
                _messageSystem);

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
            _messageSystem.Received().Publish(
                _classifier,
                Arg.Is<NameChangedEvent>(x => x.OldName == oldName && x.NewName == _newName));
        }
    }
}