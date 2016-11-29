namespace UmlSketch.Event
{
    public class ChangeClassifierColorEvent : ChangeColorEventBase
    {
        public ChangeClassifierColorEvent(string newColor) : base(newColor) { }
    }
}
