using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using UmlSketch.DomainObject;

namespace UmlSketch.Validation
{
    public class PropertyValidationService : ValidateNameBase
    {
        private readonly IEnumerable<Property> _properties;

        public PropertyValidationService(IEnumerable<Property> properties)
        {
            Contract.Requires(properties != null);

            _properties = properties;
        }

        protected override bool NameAlreadyInUse(string newName) => _properties.Any(x => x.Name == newName);
    }
}
