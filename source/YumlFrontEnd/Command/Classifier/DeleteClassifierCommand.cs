using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml;

namespace Yuml.Command
{
    public class DeleteClassifierBaseCommand : DomainObjectBaseCommand<Classifier>
    {
        private readonly ClassifierDictionary _classifierDictionary;

        public DeleteClassifierBaseCommand(
            Classifier classifier,
            ClassifierDictionary classifierDictionary) : base(classifier)
        {
            _classifierDictionary = classifierDictionary;
        }

        public void Do()
        {
            _classifierDictionary.RemoveClassifier(_domainObject);
            // TODO: do notification here by using a service
        }
    }
}
