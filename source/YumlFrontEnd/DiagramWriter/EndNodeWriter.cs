namespace UmlSketch.DiagramWriter
{
    /// <summary>
    /// defines the end node (the target classifier) of a relation.
    /// Call "Finish" to complete the relation and return to the diagram writer.
    /// </summary>
    public class EndNodeWriter
    {
        private readonly DiagramContentMixin _content;

        public EndNodeWriter(string classifier, DiagramContentMixin content = null)
        {
            _content = content;
            AppendToken("[");
            AppendIdentifier(classifier);
            AppendToken("]");
        }
        public void AppendIdentifier(string identifier) => _content.AppendIdentifier(identifier);
        public void AppendToken(string token) => _content.AppendToken(token);
        public override string ToString() => _content.ToString();

        public RelationWriter Finish() => new RelationWriter(_content);
    }
}