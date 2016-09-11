using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Diagnostics.Contracts.Contract;

namespace Yuml
{
    /// <summary>
    /// stores all relations of the UML Model.
    /// </summary>
    public class RelationList
    {
        private readonly List<Relation> _relations = new List<Relation>();

        // TODO: this is just a stub, creation should be done better
        public void AddRelation(Relation relation) => _relations.Add(relation);
        
        /// <summary>
        /// renders all relations to
        /// the diagram writer.
        /// </summary>
        /// <param name="writer"></param>
        public void WriteTo(DiagramWriter writer)
        {
            Requires(writer != null);

            foreach (var relation in _relations)
            {
                var relationWriter = writer.StartRelation();
                relationWriter = relation.WriteTo(relationWriter);
                relationWriter.Finish();
            }
        }
    }
}
