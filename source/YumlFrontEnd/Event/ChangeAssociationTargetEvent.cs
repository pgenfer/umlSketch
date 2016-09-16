using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml
{
    public class ChangeAssociationTargetEvent : IDomainEvent
    {
        private readonly Relation _relation;
        private readonly Classifier _newTarget;

        public ChangeAssociationTargetEvent(Relation relation, Classifier newTarget)
        {
            _relation = relation;
            _newTarget = newTarget;
        }
    }
}
