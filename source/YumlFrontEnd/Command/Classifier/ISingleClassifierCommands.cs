namespace Yuml.Command
{
    /// <summary>
    /// commands that can be executed on a single classifier.
    /// </summary>
    public interface ISingleClassifierCommands : ISingleCommandContext
    {
        /// <summary>
        /// returns all available commands that can be executed on the
        /// properties of this class
        /// </summary>
        IListCommandContext<Property> CommandsForProperties { get; }
        /// <summary>
        /// returns all available commands for the methods of this class
        /// </summary>
        IListCommandContext<Method> CommandsForMethods { get; }
        /// <summary>
        /// commands that are available for the associations of a class
        /// </summary>
        IListCommandContext<Relation> CommandsForAssociations { get;  }
        /// <summary>
        /// commands available for the interface list of a classifier
        /// </summary>
        IListCommandContext<Implementation> CommandsForInterfaceImplementations { get;}
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