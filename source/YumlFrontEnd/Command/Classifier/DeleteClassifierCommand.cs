using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml;
using Yuml.Service;

namespace Yuml.Command
{
    public class DeleteClassifierCommand : DomainObjectBaseCommand<Classifier>, IDeleteCommand
    {
        private readonly DeletionService _deletionService;

        public DeleteClassifierCommand(
            Classifier classifier,
            DeletionService deletionService) : base(classifier) // message handling is done by service
        {
            _deletionService = deletionService;
        }

        public void DeleteItem()
        {
            _deletionService.DeleteClassifier(_domainObject);
        }
    }
}
