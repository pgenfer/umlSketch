using UmlSketch.DomainObject;

namespace UmlSketch.Command
{
    /// <summary>
    /// commands that can be executed on a single classifier.
    /// </summary>
    public interface ISingleClassifierCommands : ISingleCommandContext<Classifier>
    {
        /// <summary>
        /// command for changing the base class of this classifier
        /// </summary>
        IChangeTypeToNullCommand ChangeBaseClass { get; }
        /// <summary>
        /// changes the color of the classifier
        /// </summary>
        IChangeColorCommand ChangeClassifierColor { get; }
        /// <summary>
        /// changes the color of the note that is attached to this classifier
        /// </summary>
        IChangeColorCommand ChangeNoteColor { get; }
        /// <summary>
        /// commands for changing the text of a note
        /// </summary>
        ChangeNoteTextCommand ChangeNoteText { get; }
        /// <summary>
        /// toggles the interface state of this classifier
        /// </summary>
        MakeClassifierToInterfaceCommand ChangeIsInterface { get; }
    }
}