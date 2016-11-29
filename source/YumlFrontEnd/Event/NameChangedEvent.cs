namespace UmlSketch.Event
{
    public class NameChangedEvent : IDomainEvent
    {
        public string OldName { get; }
        public string NewName { get; }

        public NameChangedEvent(string oldName,string newName)
        {
            OldName = oldName;
            NewName = newName;
        }
    }
}
