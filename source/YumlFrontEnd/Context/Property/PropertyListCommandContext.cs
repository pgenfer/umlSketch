using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml.Notification;

namespace Yuml.Command
{
    /// <summary>
    /// all commands that can be executed on a list of properties
    /// </summary>
    internal class PropertyListCommandContext : ListCommandContextBase<Property>
    {
        private readonly IValidateNameService _propertyValidationNameService;
        private readonly PropertyNotificationService _notificationService;

        public PropertyListCommandContext(
            Classifier classifier,
            ClassifierDictionary classifiers,
            IValidateNameService propertyValidationNameService,
            PropertyNotificationService notificationService)
        {
            _propertyValidationNameService = propertyValidationNameService;
            _notificationService = notificationService;
            All = new Query<Property>(() => classifier.Properties);
            New = new NewPropertyCommand(classifiers,classifier,notificationService);
        }

        /// <summary>
        /// returns the commands which are available for this single
        /// property item
        /// </summary>
        /// <param name="domainObject"></param>
        /// <returns></returns>
        public override ISingleCommandContext GetCommandsForSingleItem(Property domainObject)
        {
            return new PropertySingleCommandContext(
                domainObject,
                _propertyValidationNameService,
                _notificationService);
        }
    }
}
