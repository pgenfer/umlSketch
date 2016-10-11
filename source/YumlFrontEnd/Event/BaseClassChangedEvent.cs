using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml
{
    public class BaseClassChangedEvent : TypeChangedEventBase
    {
        public BaseClassChangedEvent(string nameOfOldType, string nameOfNewType) : 
            base(nameOfOldType, nameOfNewType)
        {
        }
    }
}
