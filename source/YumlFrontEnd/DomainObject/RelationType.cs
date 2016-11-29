namespace UmlSketch.DomainObject
{

    /// <summary>
    /// defines the type a relation can have.
    /// </summary>
    public enum RelationType
    {
        /// <summary>
        /// type of relation is not specified directly
        /// </summary>
        NotSpecified,
        /// <summary>
        /// a simple association between 
        /// two classifiers. The association
        /// relation is not specified any further.
        /// </summary>
        Association,
        /// <summary>
        /// In an Aggregation,
        /// two classifiers are related to each other
        /// without any defined ownership. If one
        /// classifier is destroyed, the other one
        /// stays alive.
        /// </summary>
        Aggregation,
        /// <summary>
        /// One part of the composition owns the other part.
        /// If the owning part is destroyed, the children will also
        /// be destroyed. This is a quite strong relationship.
        /// </summary>
        Composition,
        /// <summary>
        /// A classifier implements an interface.
        /// </summary>
        Implementation,
        /// <summary>
        /// A classifier depends on another classifier as a base class.
        /// </summary>
        Inheritance,
        /// <summary>
        /// One classifier is connected to another one with a weak
        /// relationship. This often means that the relation is only temporary
        /// (e.g. used for expressing method parameter relations).
        /// </summary>
        Uses
    }
}