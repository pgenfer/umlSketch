using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml
{
    /// <summary>
    /// domain event is fired when the classifier of a
    /// base class is set to null
    /// </summary>
    public class ClearBaseClassEvent : IDomainEvent
    { 
    }
}
