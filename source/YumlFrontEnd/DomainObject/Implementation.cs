using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Diagnostics.Contracts.Contract;

namespace Yuml
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
            Requires(start != end);
            Requires(end.IsInterface);
        }

        /// <summary>
        /// invariant: implementation associations must always have the correct relation type.
        /// </summary>
        [ContractInvariantMethod]
        private void ImplementationHasCorrectRelationType() => Contract.Invariant(Type == RelationType.Implementation);

        public void ReplaceInterface(Classifier newInterface)
        {
            Requires(newInterface != null);
            Requires(newInterface.IsInterface);

            End.Classifier = newInterface;
        }
    }
}
