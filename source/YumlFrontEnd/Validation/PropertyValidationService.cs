using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Diagnostics.Contracts.Contract;

namespace Yuml
{
    public class PropertyValidationService : ValidateNameBase
    {
        private readonly IEnumerable<Property> _properties;

        public PropertyValidationService(IEnumerable<Property> properties)
        {
            Requires(properties != null);

            _properties = properties;
        }

        protected override bool NameAlreadyInUse(string newName) => _properties.Any(x => x.Name == newName);
    }
}
