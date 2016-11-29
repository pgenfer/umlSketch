using UmlSketch.DomainObject;
using UmlSketch.Service;

namespace UmlSketch.Command
{
    public class DeleteClassifierCommand : DomainObjectBaseCommand<Classifier>, IDeleteCommand
    {
        private readonly DeletionService _deletionService;
        private readonly IAskUserBeforeDeletionService _askUserBeforeDeletion;

        public DeleteClassifierCommand(
            Classifier classifier,
            DeletionService deletionService,
            IAskUserBeforeDeletionService askUserBeforeDeletion) : base(classifier) // message handling is done by service
        {
            _deletionService = deletionService;
            _askUserBeforeDeletion = askUserBeforeDeletion;
        }

        public void DeleteItem()
        {
            if (_askUserBeforeDeletion == null ||
                _askUserBeforeDeletion.ShouldDomainObjectBeDeleted(Strings.AskBeforeDeletingClassifier))
                _deletionService.DeleteClassifier(_domainObject);
        }
    }
}
