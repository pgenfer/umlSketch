using UmlSketch.DomainObject;
using UmlSketch.Event;

namespace UmlSketch.Command
{
    public class ChangeNoteTextCommand
    {
        private readonly Note _note;
        private readonly MessageSystem _messageSystem;

        public ChangeNoteTextCommand(Note note,MessageSystem messageSystem)
        {
            _note = note;
            _messageSystem = messageSystem;
        }

        public void ChangeText(string newText)
        {
            _note.Text = newText;
            _messageSystem.Publish(_note,new ChangeNoteTextEvent(newText));
        }
    }
}
