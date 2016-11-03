using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Event
{
    /// <summary>
    /// event is fired when a new interface was added
    /// to a classifier
    /// </summary>
    public class AddInterfaceToClassEvent : IDomainEvent
    {
        public string InterfaceName { get;}

        public AddInterfaceToClassEvent(string interfaceName)
        {
            InterfaceName = interfaceName;
        }
    }
}
