namespace UmlSketch.Event
{
    public abstract class ChangeColorEventBase : IDomainEvent
    {
        public string Color { get; }

        protected ChangeColorEventBase(string newColor)
        {
            Color = newColor;
        }
    }
}