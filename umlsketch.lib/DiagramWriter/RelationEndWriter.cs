namespace UmlSketch.DiagramWriter
{
    /// <summary>
    /// The ending link before the relation
    /// reaches its end node.
    /// </summary>
    public class RelationEndWriter
    {
        private readonly DiagramContentMixin _content;

        public void AppendIdentifier(string identifier) => _content.AppendIdentifier(identifier);
        public void AppendToken(string token) => _content.AppendToken(token);
        public override string ToString() => _content.ToString();

        public RelationEndWriter(DiagramContentMixin content = null)
        {
            _content = content;
        }

        public RelationEndWriter WithName(string name)
        {
            AppendIdentifier(name);
            return this;
        }

        // NOTE: cases are not used because the relations will always
        // be defined by the starting point. If a reverse relation is necessary,
        // start and end can be changed. This makes the handling much easier as we only
        // have to store one relation type (between start and end)

        //public RelationEndWriter AsComposition()
        //{
        //    AppendToken("++");
        //    return this;
        //}

        //public RelationEndWriter AsAggregate()
        //{
        //    AppendToken("<>");
        //    return this;
        //}

        public RelationEndWriter WithNavigation()
        {
            AppendToken(">");
            return this;
        }

        public EndNodeWriter AsInterface(string interfaceClass)
        {
            AppendToken(".-^");
            return new EndNodeWriter(interfaceClass, _content);
        }

        public EndNodeWriter AsBaseClass(string baseClass)
        {
            AppendToken("^");
            return new EndNodeWriter(baseClass, _content);
        }

        public EndNodeWriter ToClassifier(string endClassifier) => new EndNodeWriter(endClassifier, _content);
    }
}