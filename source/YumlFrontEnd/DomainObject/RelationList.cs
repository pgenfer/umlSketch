using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using UmlSketch.Settings;

namespace UmlSketch.DomainObject
{
    /// <summary>
    /// stores all relations of the UML Model.
    /// </summary>
    public class RelationList : IEnumerable<Relation>
    {
        private readonly List<Relation> _relations = new List<Relation>();

        public RelationList(IEnumerable<Relation> relations = null)
        {
            if(relations != null)
               _relations.AddRange(relations);
        }

        public void AddRelations(IEnumerable<Relation> relations) => _relations.AddRange(relations);

        // TODO: this is just a stub, creation should be done better
        public void AddRelation(Relation relation) => _relations.Add(relation);
        
        /// <summary>
        /// renders all relations to
        /// the diagram writer.
        /// </summary>
        /// <param name="writer"></param>
        public void WriteTo(DiagramWriter.DiagramWriter writer,DiagramDirection direction)
        {
            Contract.Requires(writer != null);

            // skip if no relations are available
            if (_relations.Count == 0)
                return;

            // relations are always after classifiers,
            // so add a separator
            writer.AddSeparator();

            foreach (var relation in _relations.Where(x => 
                x.IsVisible && 
                x.Start.Classifier.IsVisible && 
                x.End.Classifier.IsVisible))
            {
                var relationWriter = writer.StartRelation();
                relationWriter = relation.WriteTo(relationWriter,direction);
                relationWriter.Finish();
            }
        }

        public IEnumerator<Relation> GetEnumerator() => _relations.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _relations.GetEnumerator();
    }
}
