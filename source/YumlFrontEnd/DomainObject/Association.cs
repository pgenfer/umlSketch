using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using static System.Diagnostics.Contracts.Contract;

namespace Yuml
{
    /// <summary>
    /// an association is a more specific type of 
    /// a relation. It means that one classifier
    /// references another one.
    /// </summary>
    public class Association : Relation, INamed
    {
        private readonly INamed _named = new NameMixin();

        public string Name
        {
            get { return _named.Name; }
            set { _named.Name = value; }
        }

        /// <summary>
        /// should only be used for mappoing and testing
        /// </summary>
        public Association() { }

        /// <summary>
        /// creates a new relation between two nodes
        /// </summary>
        /// <param name="start">classifier where the relation starts</param>
        /// <param name="end">classifier where the relation ends</param>
        /// <param name="name">name of the association</param>
        /// <param name="startName">optional name of the start node of the relation</param>
        /// <param name="endName">optional name of the end node of the relation</param>
        /// <param name="relation">type of the relation</param>
        public Association(
            Classifier start,
            Classifier end,
            RelationType relation = RelationType.Association,
            string name = "",
            string startName = "",
            string endName = "")
        {
            Requires(start != null);
            Requires(end != null);
            // ensure that only specific relation types are allowed here
            Requires(relation != RelationType.Implementation);
            Requires(relation != RelationType.Inheritance);

            Name = name;
            Start = new StartNode(start, startName);
            End = new EndNode(end, endName);
            Type = relation;
            IsVisible = true;
        }

        public override RelationWriter WriteTo(RelationWriter relationWriter,DiagramDirection direction)
        {
            relationWriter = relationWriter.WithName(Name);
            return base.WriteTo(relationWriter,direction);
        }
    }
}
