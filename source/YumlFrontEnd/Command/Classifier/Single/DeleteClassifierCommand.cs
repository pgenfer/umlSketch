using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml;

namespace Yuml.Commands
{
    public class DeleteClassifierCommand : SingleClassifierCommandBase
    {
        private readonly ClassifierDictionary _classifierDictionary;

        public DeleteClassifierCommand(
            Classifier classifier,
            ClassifierDictionary classifierDictionary) : base(classifier)
        {
            _classifierDictionary = classifierDictionary;
        }

        public void Do()
        {
            _classifierDictionary.RemoveClassifier(_classifier);
            // TODO: do notification here by using a service
        }
    }
}
