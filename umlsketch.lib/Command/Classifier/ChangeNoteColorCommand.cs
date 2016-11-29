using UmlSketch.DomainObject;
using UmlSketch.Event;

namespace UmlSketch.Command
{
    internal class ChangeNoteColorCommand : IChangeColorCommand
    {
        private readonly Note _note;
        private readonly MessageSystem _messageSystem;
        public ChangeNoteColorCommand(Note note, MessageSystem messageSystem)
        {
            _note = note;
            _messageSystem = messageSystem;
        }

        public void ChangeColor(string newColor)
        {
            _note.Color = newColor;
            _messageSystem.Publish(_note,new ChangeNoteColorEvent(newColor));
        }
    }
}
