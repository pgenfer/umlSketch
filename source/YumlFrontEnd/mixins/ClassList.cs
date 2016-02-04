using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuml.metadata;

namespace yuml.mixins
{
    public class ClassList
    {
        private HashSet<Class> _classes = new HashSet<Class>();

        public void AddClass(Class @class) => _classes.Add(@class);
        public IEnumerable<Class> Classes => _classes;
        public void RemoveClass(Class @class) => _classes.Remove(@class);
    }
}
