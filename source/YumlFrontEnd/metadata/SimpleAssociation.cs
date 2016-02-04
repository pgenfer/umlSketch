using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yuml.metadata
{
    public class SimpleAssociation : Association
    {
        public SimpleAssociation(Class first, Class second)
        {
            FirstConnection = new Connection(@class: first);
            SecondConnection = new Connection(@class: second);
        }
    }
}
