using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class NameMixin : INamed
    {
        public string Name { get; set; }
        public override string ToString() => Name;
    }
}
