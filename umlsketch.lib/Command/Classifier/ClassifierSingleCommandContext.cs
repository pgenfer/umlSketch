using UmlSketch.DomainObject;
using UmlSketch.Event;
using UmlSketch.Service;
using UmlSketch.Validation;

namespace UmlSketch.Command
{
    /// <summary>
    /// classifier command context does not inherit from SingleCommandContext
    /// because it needs different parameters and also has a different Delete command
    /// </summary>
    public class ClassifierSingleCommandContext : ISingleClassifierCommands
    {
        public ClassifierSingleCommandContext(
            Classifier classifier,
            ClassifierDictionary classifierDictionary,
            DeletionService deletionService,
            IRelationService relationService,
            IAskUserBeforeDeletionService askUserService,
            MessageSystem messageSystem)
        {
            Rename = new RenameClassifierCommand(
                classifier,
                classifierDictionary,
                new ClassifierValidationService(classifierDictionary),
                messageSystem);
            ChangeBaseClass = new ChangeBaseClassCommand(
                classifier,
                classifierDictionary,
                messageSystem);
            Delete = new DeleteClassifierCommand(classifier, deletionService,askUserService);
            Visibility = new ShowOrHideSingleObjectCommand(classifier, messageSystem);
            ChangeClassifierColor = new ChangeColorCommand(classifier, messageSystem);
            ChangeNoteColor = new ChangeNoteColorCommand(classifier.Note,messageSystem);
            ChangeNoteText = new ChangeNoteTextCommand(classifier.Note,messageSystem);
            ChangeIsInterface = new MakeClassifierToInterfaceCommand(classifier, relationService);
        }

        public IChangeTypeToNullCommand ChangeBaseClass { get; }
        public IChangeColorCommand ChangeClassifierColor { get; }
        public IChangeColorCommand ChangeNoteColor { get; }
        public ChangeNoteTextCommand ChangeNoteText { get; }
        public MakeClassifierToInterfaceCommand ChangeIsInterface { get; }
        public IRenameCommand Rename { get; }
        public IDeleteCommand Delete { get; }
        public IShowOrHideCommand Visibility { get; }
    }
}
