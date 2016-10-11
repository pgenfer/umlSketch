using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Yuml
{
    public class NameChangedEvent : IDomainEvent
    {
        public string OldName { get; }
        public string NewName { get; }

        public NameChangedEvent(string oldName,string newName)
        {
            OldName = oldName;
            NewName = newName;
        }
    }
}
