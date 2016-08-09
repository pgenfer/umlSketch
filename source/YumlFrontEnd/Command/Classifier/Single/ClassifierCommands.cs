namespace Yuml.Commands
{
    /// <summary>
    /// class holds a list of all commands which are
    /// available on a single classifier.
    /// The commands are private fields,
    /// from outside they will simply be accessed via a method,
    /// in that way it looks like that a single method is called but
    /// instead the whole command is invoked
    /// </summary>
    public class ClassifierCommands : IClassiferCommands
    {
        /// <summary>
        /// TODO: might be used later
        /// </summary>
        private readonly DeleteClassifierCommand _delete;

        public ClassifierCommands(
            ClassifierDictionary classifiers,
            Classifier classifier,
            ClassifierNotificationService notificationService)
        {
            RenameCommand = new RenameClassifierCommand(classifier, classifiers,notificationService);
            _delete = new DeleteClassifierCommand(classifier, classifiers);
        }

        public IRenameCommand RenameCommand { get; }
    }
}