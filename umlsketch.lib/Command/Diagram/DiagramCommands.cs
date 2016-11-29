using UmlSketch.DomainObject;
using UmlSketch.Event;

namespace UmlSketch.Command
{
    /// <summary>
    /// general commands that operate on the diagram level
    /// </summary>
    public class DiagramCommands
    {
        public DiagramCommands(Diagram diagram, MessageSystem messageSystem)
        {
            ChangeNoteColor = new ChangeNoteColorCommand(diagram.Note,messageSystem);
            ChangeNoteText = new ChangeNoteTextCommand(diagram.Note, messageSystem);
        }

        public IChangeColorCommand ChangeNoteColor { get; }
        public ChangeNoteTextCommand ChangeNoteText { get; }
    }
}
