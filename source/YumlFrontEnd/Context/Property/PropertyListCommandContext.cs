using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Command
{
    public class PropertyListCommandContext : ListCommandContextBase<Property>
    {
        private readonly IValidateNameService _propertyValidationNameService;

        public PropertyListCommandContext(
            Classifier classifier,
            IValidateNameService propertyValidationNameService)
        {
            _propertyValidationNameService = propertyValidationNameService;
            All = new Query<Property>(() => classifier.Properties);
        }

        public override ISingleCommandContext GetCommandsForSingleItem(Property domainObject)
        {
            return new PropertySingleCommandContext(
                domainObject,
                _propertyValidationNameService);
        }
    }
}
