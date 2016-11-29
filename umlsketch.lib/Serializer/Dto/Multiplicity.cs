namespace UmlSketch.Serializer.Dto
{

    /// <summary>
    /// defines the multiplicity
    /// of a relation
    /// </summary>
    public enum Multiplicity
    {
        /// <summary>
        /// multiplicity is not defined
        /// </summary>
        None,
        /// <summary>
        /// Unique, there can only be one element
        /// </summary>
        One,
        /// <summary>
        /// optional, element can be there but is not needed
        /// </summary>
        ZeroToOne,
        /// <summary>
        /// unlimited, there can be an unlimited number of 
        /// elements connected to this element
        /// </summary>
        ZeroToMany,
        /// <summary>
        /// polyvalent, can have unlimited relations but relation must
        /// at least be set.
        /// </summary>
        OneToMany,
    }
}