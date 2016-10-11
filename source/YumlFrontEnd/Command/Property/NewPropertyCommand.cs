using static System.Diagnostics.Contracts.Contract;

namespace Yuml.Command
{
    /// <summary>
    /// command used to create new new property for a classifier
    /// </summary>
    public class NewPropertyCommand : INewCommand
    {
        private readonly ClassifierDictionary _availableClassifiers;

        /// <summary>
        /// the classifier where the new property will be placed
        /// </summary>
        private readonly Classifier _classifier;

        private readonly MessageSystem _messageSystem;

        public NewPropertyCommand(
            ClassifierDictionary availableClassifiers,
            Classifier classifier,
            MessageSystem messageSystem)
        {
            Requires(classifier != null);
            Requires(messageSystem != null);
            Requires(availableClassifiers != null);

            _availableClassifiers = availableClassifiers;
            _classifier = classifier;
            _messageSystem = messageSystem;
        }

        public void CreateNew()
        {
            var property = _classifier.CreateNewPropertyWithBestInitialValues(_availableClassifiers);
            _messageSystem.PublishCreated(_classifier, property);
        }
    }
}
