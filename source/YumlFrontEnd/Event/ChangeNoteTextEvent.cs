namespace UmlSketch.Event
{
    public class ChangeNoteTextEvent : IDomainEvent
    {
        private readonly string _text;

        public ChangeNoteTextEvent(string text)
        {
            _text = text;
        }
    }
}