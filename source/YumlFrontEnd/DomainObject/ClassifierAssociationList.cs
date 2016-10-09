using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using static System.Diagnostics.Contracts.Contract;

namespace Yuml.DomainObject
{
    /// <summary>
    /// contains a list of all associations 
    /// that are linked to or from this class.
    /// Interface implementations and base classes
    /// are not stored in this as this list only handles
    /// assocations
    /// </summary>
    public class ClassifierAssociationList : IEnumerable<Relation>, IVisible
    {
        /// <summary>
        /// the classifier that is the owner of this association list
        /// </summary>
        private readonly Classifier _start;

        public ClassifierAssociationList(Classifier start)
        {
            _start = start;
        }

        /// <summary>
        /// internal list that stores the relations
        /// </summary>
        private readonly List<Relation> _relations = new List<Relation>();

        public void DeleteRelation(Relation relation)
        {
            Requires(relation != null);
            Requires(this.Contains(relation));

            _relations.Add(relation);
        }

        /// <summary>
        /// creates a new association with default values, adds it to the assocation
        /// list and returns the newly created assocication.
        /// </summary>
        /// <param name="classifiers"></param>
        /// <returns></returns>
        public Relation CreateNewAssociationWithBestInitialValues(ClassifierDictionary classifiers)
        {
            // try to find a classifier that is suitable for this relation.
            // we take the first one which is not used already and which is not the
            // source itslef
            var usedClassifiers = _relations.Select(x => x.End.Classifier).Concat(new[] {_start});
            var firstUnusedClassifier = classifiers
                .Except(usedClassifiers)
                .FirstOrDefault();
            // all classifiers are already used, so take the first one which is used
            // there must always be one, at least the source itself
            firstUnusedClassifier = firstUnusedClassifier ?? classifiers.First();

            var newRelation = new Relation(_start, firstUnusedClassifier);
            _relations.Add(newRelation);

            return newRelation;
        }

        public Relation AddNewRelation(
            Classifier target,
            RelationType associationType = RelationType.Association,
            string startName = "",
            string endName = "")
        {
            // implementation or derivation are not stored
            // in the classes relation list
            Requires(associationType != RelationType.Implementation);
            Requires(associationType != RelationType.Inheritance);

            var relation = new Relation(_start, target, associationType, startName, endName );
            _relations.Add(relation);

            return relation;
        }

        public IEnumerator<Relation> GetEnumerator() => _relations.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _relations.GetEnumerator();

        public bool IsVisible
        {
            get { return _relations.All(x => x.IsVisible); }
            set { _relations.ForEach(x => x.IsVisible = value);}
        }
    }
}
