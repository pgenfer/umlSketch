using System.Linq;

namespace Yuml
{
    /// <summary>
    /// starting classifier and assocation start point of the relation.
    /// After defining the starting type of the relation,
    /// the ending type of the relation can be defined.
    /// </summary>
    public class StartNodeWriter
    {
        private readonly DiagramDirection _direction;
        private readonly string _relationName;
        private readonly DiagramContentMixin _content;

        public void AppendIdentifier(string identifier) => _content.AppendIdentifier(identifier);
        public void AppendToken(string token) => _content.AppendToken(token);
        public override string ToString() => _content.ToString();

        public StartNodeWriter(
            string classifier,
            DiagramDirection direction,
            string relationName = "",
            DiagramContentMixin content = null)
        {
            _direction = direction;
            _relationName = relationName;
            _content = content;

            AppendToken("[");
            AppendIdentifier(classifier);
            AppendToken("]");
        }

        private void WriteName()
        {
            if (string.IsNullOrEmpty(_relationName))
                return;

            switch (_direction)
            {
                case DiagramDirection.LeftToRight:
                    AppendToken($"{_relationName}{new string(Enumerable.Repeat(' ', 8).ToArray())}");
                    return;
                case DiagramDirection.RightToLeft:
                    AppendToken($"{new string(Enumerable.Repeat(' ', 8).ToArray())}{_relationName}");
                    return;
                case DiagramDirection.TopDown:
                    AppendToken($"{new string(Enumerable.Repeat(' ', 3).ToArray())}{_relationName}");
                    return;
            }
        }

        public RelationEndWriter WithNavigation()
        {
            AppendToken("<-");
            WriteName();
            return new RelationEndWriter(_content);
        }

        public RelationEndWriter AsSimpleAssociation()
        {
            AppendToken("-");
            WriteName();
            return new RelationEndWriter(_content);
        }

        public RelationEndWriter AsUsesRelation()
        {
            AppendToken("-.-");
            WriteName();
            return new RelationEndWriter(_content);
        }

        public RelationEndWriter AsCompositeOwner()
        {
            AppendToken("++-");
            WriteName();
            return new RelationEndWriter(_content);
        }

        public RelationEndWriter AsAggregateOwner()
        {
            AppendToken("<>-");
            WriteName();
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
            return new EndNodeWriter(implementationClass, _content);
        }

        public StartNodeWriter WithName(string name)
        {
            AppendIdentifier(name);
            return this;
        }
    }
}