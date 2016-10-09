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
            ClassifierDictionary availableClassifiers,
            Property property,
            IValidateNameService propertyValidationNameService,
            PropertyNotificationService propertyNotifcationService,
            MessageSystem messageSystem)
        {
            Rename = new RenameMemberCommand(
                property,
                propertyValidationNameService,
                propertyNotifcationService);
            ChangeTypeOfProperty = new ChangeTypeOfPropertyCommand(
                availableClassifiers,
                property,
                propertyNotifcationService);
            Visibility = new ShowOrHideSingleObjectCommand(property, messageSystem);
        }

        public IChangeTypeCommand ChangeTypeOfProperty { get; }
    }
}
