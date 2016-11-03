using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Diagnostics.Contracts.Contract;

namespace Yuml
{
    /// <summary>
    /// an implementation is a special kind of 
    /// relation where the start nodes implements the interface 
    /// at the end node.
    /// </summary>
    public class Implementation : Relation
    {
        private RelationType _Type = RelationType.Implementation;

        public Implementation(Classifier start, Classifier end)
            :base(start,end, RelationType.Implementation, string.Empty)
        {
            Requires(start != end);
            Requires(end.IsInterface);
        }

        public override RelationType Type
        {
            get { return _Type; }
            set
            {
                // implementations can only have an "implementation" relation type.
                Requires(value == RelationType.Implementation);
                _Type = value; 
            }
        }
    }
}
