using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml
{
    public class ChangeClassifierColorEvent : ChangeColorEventBase
    {
        public ChangeClassifierColorEvent(string newColor) : base(newColor) { }
    }
}
