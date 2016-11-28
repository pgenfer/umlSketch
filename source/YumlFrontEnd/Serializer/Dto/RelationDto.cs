namespace Yuml.Serializer.Dto
{
    /// <summary>
    /// stores the information of a relation
    /// between two classifiers.
    /// A relation always has a start and an end node.
    /// </summary>
    internal class RelationDto
    {
        /// <summary>
        /// start node of the relation
        /// </summary>
        public ClassifierDto Start { get; set; }
        /// <summary>
        /// end  node of the relation
        /// </summary>
        public ClassifierDto End { get; set; }
        /// <summary>
        /// defines whether this relation is bidirectional,
        /// otherwise it would only go from start -> end
        /// </summary>
        public bool BiDirectional { get; set; }
        /// <summary>
        /// multiplicity of the start relation
        /// </summary>
        public Multiplicity StartMultiplicity { get; set; }
        /// <summary>
        /// multiplicity of the end relation
        /// </summary>
        public Multiplicity EndMultiplicity { get; set; }
        /// <summary>
        /// the type of the relation
        /// </summary>
        public RelationType RelationType { get; set; }
        /// <summary>
        /// a relation can be visible or hidden
        /// </summary>
        public bool IsVisible { get; set; }
    }
}
