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
        private readonly PropertyList _properties;
        private readonly MessageSystem _messageSystem;

        public NewPropertyCommand(
            ClassifierDictionary availableClassifiers,
            PropertyList properties,
            MessageSystem messageSystem)
        {
            Requires(properties != null);
            Requires(messageSystem != null);
            Requires(availableClassifiers != null);

            _availableClassifiers = availableClassifiers;
            _properties = properties;
            _messageSystem = messageSystem;
        }

        public void CreateNew()
        {
            var property = _properties.CreateNewPropertyWithBestInitialValues(_availableClassifiers);
            _messageSystem.PublishCreated(_properties, property);
        }
    }
}
