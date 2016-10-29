using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using static System.Diagnostics.Contracts.Contract;

namespace Yuml
{
    /// <summary>
    /// class that handles the relation between
    /// two classifiers. A relation has a start node
    /// and an end node.
    /// </summary>
    public class Relation : IVisible
    {
        private readonly IVisible _visible = new VisibleMixin();

        /// <summary>
        /// should only be used for testing
        /// </summary>
        public Relation(){}
        
        /// <summary>
        /// creates a new relation between two nodes
        /// </summary>
        /// <param name="start">classifier where the relation starts</param>
        /// <param name="end">classifier where the relation ends</param>
        /// <param name="startName">optional name of the start node of the relation</param>
        /// <param name="endName">optional name of the end node of the relation</param>
        /// <param name="relation">type of the relation</param>
        public Relation(
            Classifier start,
            Classifier end,
            RelationType relation = RelationType.Association,
            string startName = "",
            string endName = "")
        {
            Requires(start != null);
            Requires(end != null);

            Start = new StartNode(start,startName);
            End = new EndNode(end, endName);
            Type = relation;
            IsVisible = true;
        }

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
                    case RelationType.Uses:
                        relationEnd = End.WriteTo(
                            startNode
                                .AsUsesRelation()
                                .WithNavigation()); // dependencies are often drawn as navigatable, so we reflect this here also
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
}


