namespace UmlSketch.Event
{
    /// <summary>
    /// this event is fired
    /// whenever an application setting changes that affects 
    /// the diagram.
    /// </summary>
    public class DiagramSettingChangedEvent : IDomainEvent
    {
        private readonly string _settingName;

        public DiagramSettingChangedEvent(string settingName)
        {
            _settingName = settingName;
        }
    }
}
