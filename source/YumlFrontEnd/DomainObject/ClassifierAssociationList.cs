﻿using System;
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
    public class ClassifierAssociationList : BaseList<Relation>
    {
        /// <summary>
        /// creates a new association with default values, adds it to the assocation
        /// list and returns the newly created assocication.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="classifiers"></param>
        /// <returns></returns>
        public Relation CreateNewAssociationWithBestInitialValues(Classifier start,ClassifierDictionary classifiers)
        {
            Requires(start != null);
            Requires(classifiers.Count > 0);

            // try to find a classifier that is suitable for this relation.
            // we take the first one which is not used already and which is not the
            // source itself
            var usedClassifiers = _list.Select(x => x.End.Classifier).Concat(new[] { start });
            var firstUnusedClassifier = classifiers
                .Except(usedClassifiers)
                .FirstOrDefault();
            // all classifiers are already used, so take the first one which is used
            // there must always be one, at least the source itself
            firstUnusedClassifier = firstUnusedClassifier ?? classifiers.First();

            var newRelation = new Relation(start, firstUnusedClassifier);
            AddNewMember(newRelation);

            return newRelation;
        }

        public Relation AddNewRelation(
            Classifier start,
            Classifier target,
            RelationType associationType = RelationType.Association,
            string startName = "",
            string endName = "")
        {
            // implementation or derivation are not stored
            // in the classes relation list
            Requires(associationType != RelationType.Implementation);
            Requires(associationType != RelationType.Inheritance);
            Requires(start != null);

            var relation = new Relation(start, target, associationType, startName, endName );
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
