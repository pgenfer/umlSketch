using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml.Notification;

namespace Yuml.Command
{
    public class PropertySingleCommandContext : SingleCommandContextBase, ISinglePropertyCommands
    {
        public PropertySingleCommandContext(
            Property property,
            IValidateNameService propertyValidationNameService,
            PropertyNotificationService propertyNotifcationService)
        {
            Rename = new RenamePropertyCommand(
                property,
                propertyValidationNameService,
                propertyNotifcationService);
            // TODO: implement change property type command
        }
    }
}
