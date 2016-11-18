using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    public class ClassifierAssociationList : BaseList<Relation>
    {
        /// <summary>
        /// the root classifier where all relations of this list start.
        /// Should be set after the association list is assigned to a classifier
        /// </summary>
        public Classifier Root { get; set; }
        
        /// <summary>
        /// creates a new association with default values, adds it to the assocation
        /// list and returns the newly created assocication.
        /// </summary>
        /// <param name="classifiers"></param>
        /// <returns></returns>
        public override Relation CreateNew(ClassifierDictionary classifiers)
        {
            // ensure that the relation is not bound to a system type
            Ensures(!Result<Relation>().End.Classifier.IsSystemType);

            // try to find a classifier that is suitable for this relation.
            // we take the first one which is not used already and which is not the
            // source itself
            var usedClassifiers = _list.Select(x => x.End.Classifier).Concat(new[] { Root });
            var firstUnusedClassifier = classifiers
                .Except(usedClassifiers)
                .FirstOrDefault(x => !x.IsSystemType);
            // all classifiers are already used, so take the first one which is used
            // there must always be one, at least the source itself
            firstUnusedClassifier = firstUnusedClassifier ?? classifiers.First(x => !x.IsSystemType);

            var newRelation = new Relation(Root, firstUnusedClassifier);
            AddNewMember(newRelation);

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
            Requires(Root != null);

            var relation = new Relation(Root, target, associationType, startName, endName );
            AddNewMember(relation);

            return relation;
        }

        public void AddExistingRelation(Relation relation)
        {
            // implementation or derivation are not stored
            // in the classes relation list
            Requires(relation != null);
            Requires(relation.Type != RelationType.Implementation);
            Requires(relation.Type != RelationType.Inheritance);

            AddExistingMember(relation);
        }

        public SubSet FindAssociationsThatDependOnClassifier(Classifier classifier) => 
            Filter(x => x.End.Classifier == classifier);
    }
}
