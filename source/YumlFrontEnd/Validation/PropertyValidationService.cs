using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml
{
    public class PropertyValidationService : ValidateNameBase
    {
        private readonly IEnumerable<Property> _properties;

        public PropertyValidationService(IEnumerable<Property> properties)
        {
            _properties = properties;
        }

        protected override bool NameAlreadyInUse(string newName) => _properties.Any(x => x.Name == newName);
    }
}
