namespace UmlSketch.Event
{
    /// <summary>
    /// generic event that can be used as base class for any type changes that occur.
    /// </summary>
    public abstract class TypeChangedEventBase : IDomainEvent
    {
        public string NameOfOldType { get; }
        public string NameOfNewType { get; }

        protected TypeChangedEventBase(
            string nameOfOldType, 
            string nameOfNewType)
        {
            NameOfOldType = nameOfOldType;
            NameOfNewType = nameOfNewType;
        }
    }
}
