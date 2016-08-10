using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Command
{
    public class PropertySingleCommandContext : SingleCommandContextBase, ISinglePropertyCommands
    {
        public PropertySingleCommandContext(
            Property property,
            IValidateNameService propertyValidationNameService)
        {
            Rename = new RenamePropertyCommand(property);
            // TODO: implement change property type command
        }
    }
}
