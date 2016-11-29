namespace UmlSketch.Event
{
    public class ChangeNoteColorEvent : ChangeColorEventBase
    {
        public ChangeNoteColorEvent(string newColor) : base(newColor) { }
    }
}