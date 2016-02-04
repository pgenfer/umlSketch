using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuml.metadata;

namespace yuml.mixins
{
    public class AssociationList 
    {
        private List<Association> _associations = new List<Association>();

        public void AddAssociation(Association association) => _associations.Add(association);
        public IEnumerable<Association> Associations => _associations;
    }
}
