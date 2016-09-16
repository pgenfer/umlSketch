namespace Yuml.Command
{
    /// <summary>
    /// commands that can be executed on the association list of a classifier
    /// </summary>
    public class AssociationListCommandContext : ListCommandContextBase<Relation>
    {
        private readonly ClassifierDictionary _classifiers;
        private readonly MessageSystem _messageSystem;

        public AssociationListCommandContext(
            Classifier classifier,
            ClassifierDictionary classifiers,
            MessageSystem messageSystem)
        {
            _classifiers = classifiers;
            _messageSystem = messageSystem;
            // TODO: implement visible
            New = new NewAssociationCommand(classifiers, classifier, messageSystem);
            All = new Query<Relation>(() => classifier.Associations);
        }

        public override ISingleCommandContext GetCommandsForSingleItem(Relation domainObject)
        {
            return new SingleAssociationCommands(_classifiers,domainObject,_messageSystem);
        }
    }
}