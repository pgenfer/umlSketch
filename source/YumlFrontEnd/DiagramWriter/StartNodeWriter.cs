namespace Yuml
{
    /// <summary>
    /// starting classifier and assocation start point of the relation.
    /// After defining the starting type of the relation,
    /// the ending type of the relation can be defined.
    /// </summary>
    public class StartNodeWriter
    {
        private readonly DiagramContentMixin _content;

        public void AppendIdentifier(string identifier) => _content.AppendIdentifier(identifier);
        public void AppendToken(string token) => _content.AppendToken(token);
        public override string ToString() => _content.ToString();

        public StartNodeWriter(string classifier,DiagramContentMixin content = null)
        {
            _content = content;

            AppendToken("[");
            AppendIdentifier(classifier);
            AppendToken("]");
        }

        public RelationEndWriter WithNavigation()
        {
            AppendToken("<-");
            return new RelationEndWriter(_content);
        }

        public RelationEndWriter AsSimpleAssociation()
        {
            AppendToken("-");
            return new RelationEndWriter(_content);
        }

        public RelationEndWriter AsCompositeOwner()
        {
            AppendToken("++-");
            return new RelationEndWriter(_content);
        }

        public RelationEndWriter AsAggregateOwner()
        {
            AppendToken("<>-");
            return new RelationEndWriter(_content);
        }

        public EndNodeWriter WithDerivation(string derivedClass)
        {
            AppendToken("^-");
            return new EndNodeWriter(derivedClass, _content);
        }

        public EndNodeWriter WithInterfaceImplementation(string implementationClass)
        {
            AppendToken("^-.-");
            return new EndNodeWriter(implementationClass,_content);
        }

        public StartNodeWriter WithName(string name)
        {
            AppendIdentifier(name);
            return this;
        }
    }
}