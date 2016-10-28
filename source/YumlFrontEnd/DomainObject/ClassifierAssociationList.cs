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
    public class ClassifierAssociationList : IVisibleObjectList, IEnumerable<Relation>
    {
        /// <summary>
        /// the classifier that is the owner of this association list
        /// </summary>
        private Classifier _start;

        public ClassifierAssociationList(Classifier start)
        {
            _start = start;
        }

        /// <summary>
        /// empty constructor is only used for serialization,
        /// should not be used in production. Start classifier
        /// will be set when first existing relation is added to the list
        /// </summary>
        internal ClassifierAssociationList()
        {
            
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
            // source itself
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

        public void AddExistingRelation(Relation relation)
        {
            // implementation or derivation are not stored
            // in the classes relation list
            Requires(relation != null);
            Requires(relation.Type != RelationType.Implementation);
            Requires(relation.Type != RelationType.Inheritance);
            Ensures(_start == relation.Start.Classifier);
            // ensure that if start was already set it did not change or 
            // if it was not set it now points to the start of this relation
            Ensures(OldValue(_start) != null ? _start == OldValue(_start) : _start == relation.Start.Classifier);

            _relations.Add(relation);
            _start = relation.Start.Classifier;
        }

        public IEnumerator<Relation> GetEnumerator() => _relations.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _relations.GetEnumerator();

        public bool IsVisible
        {
            get { return _relations.All(x => x.IsVisible); }
            set { _relations.ForEach(x => x.IsVisible = value);}
        }

        public IEnumerable<IVisible> VisibleObjects => _relations;
    }
}
