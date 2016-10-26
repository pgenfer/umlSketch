using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml
{
    /// <summary>
    /// event is fired when the classifier list was completly reset,
    /// so all existing classifiers were removed from the list.
    /// Will in general be fired when a new classifier list was loaded from
    /// a persistant storage
    /// </summary>
    public class ClassifiersResetEvent : IDomainEvent
    {
    }
}
