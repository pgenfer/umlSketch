using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml
{
    public class ChangeClassifierColorEvent : IDomainEvent
    {
        public string Color { get; }

        public ChangeClassifierColorEvent(string newColor)
        {
            Color = newColor;
        }
    }
}
