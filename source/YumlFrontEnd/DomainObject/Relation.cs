using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using static System.Diagnostics.Contracts.Contract;

namespace Yuml
{
    public class Relation : IVisible
    {
        private readonly IVisible _visible = new VisibleMixin();

        public bool IsVisible
        {
            get { return _visible.IsVisible; }
            set { _visible.IsVisible = value; }
        }

        public StartNode Start { get; set; }
        public EndNode End { get; set; }

        /// <summary>
        /// type of the relation (from start -> end)
        /// </summary>
        public RelationType Type { get; set; }

        public RelationWriter WriteTo(RelationWriter relationWriter)
        {
            Requires(relationWriter != null);

            var startNode = Start.WriteTo(relationWriter);
            EndNodeWriter relationEnd;
            // normally in UML, we could have an association and a navigation,
            // but Yuml.me only supports one or the other, so we have to check that here
            if (Start.IsNavigatable)
                relationEnd = End.WriteTo(startNode.WithNavigation());
            else
            {
                switch (Type)
                {
                    case RelationType.Aggregation:
                        relationEnd = End.WriteTo(startNode.AsAggregateOwner());
                        break;
                    case RelationType.Composition:
                        relationEnd = End.WriteTo(startNode.AsCompositeOwner());
                        break;
                    case RelationType.Implementation:
                        // start node is the implementation and
                        // end node is the interface
                        relationEnd = startNode
                            .AsSimpleAssociation()
                            .AsInterface(End.Classifier.Name);
                        break;
                    case RelationType.Inheritance:
                        // start node is sub class,
                        // end node is base class
                        relationEnd = startNode
                            .AsSimpleAssociation()
                            .AsBaseClass(End.Classifier.Name);
                        break;
                    case RelationType.Association:
                    default: // default case is a normal association
                        relationEnd = End.WriteTo(startNode.AsSimpleAssociation());
                        break;
                }
            }
            return relationEnd.Finish();
        }
    }

    public class RelationNode : INamed
    {
        private readonly INamed _named = new NameMixin();

        public Classifier Classifier { get; set; }

        public string Name
        {
            get { return _named.Name; }
            set { _named.Name = value; }
        }

        public bool IsNavigatable { get; set; }
    }
}


