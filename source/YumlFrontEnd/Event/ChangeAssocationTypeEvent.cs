using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml
{
    /// <summary>
    /// event is fired when the type of an association changes
    /// </summary>
    public class ChangeAssocationTypeEvent : IDomainEvent
    {
        public Relation Relation { get; }

        public ChangeAssocationTypeEvent(Relation relation)
        {
            Relation = relation;
        }
    }
}
