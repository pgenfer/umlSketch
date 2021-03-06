﻿using System.Diagnostics.Contracts;

namespace UmlSketch.DomainObject
{
    /// <summary>
    /// an implementation is a special kind of 
    /// relation where the start nodes implements the interface 
    /// at the end node.
    /// </summary>
    public class Implementation : Relation
    {
        /// <summary>
        /// should only be used for testing and mapping
        /// </summary>
        public Implementation()
        {
            Type = RelationType.Implementation;
        }

        public Implementation(Classifier start, Classifier end)
            :base(start,end, RelationType.Implementation, string.Empty)
        {
            Contract.Requires(start != end);
            Contract.Requires(end.IsInterface);

        }

        /// <summary>
        /// invariant: implementation associations must always have the correct relation type.
        /// </summary>
        [ContractInvariantMethod]
        private void ImplementationHasCorrectRelationType() => 
            Contract.Invariant(Type == RelationType.Implementation);

        public void ReplaceInterface(Classifier newInterface)
        {
            Contract.Requires(newInterface != null);
            Contract.Requires(newInterface.IsInterface);

            End.Classifier = newInterface;
        }
    }

    /// <summary>
    /// relations that represents an inheritance between a
    /// class and its base class
    /// </summary>
    public class Inheritance : Relation
    {
        /// <summary>
        /// should only be used for testing and mapping
        /// </summary>
        public Inheritance()
        {
            Type = RelationType.Inheritance;
        }

        public Inheritance(Classifier start, Classifier end)
            : base(start, end, RelationType.Inheritance, string.Empty)
        {
            Contract.Requires(start != end);
            Contract.Requires(end != null);
        }

        /// <summary>
        /// invariant: implementation associations must always have the correct relation type.
        /// </summary>
        [ContractInvariantMethod]
        private void InheritanceHasCorrectRelationType() =>
            Contract.Invariant(Type == RelationType.Inheritance);

    }
}
