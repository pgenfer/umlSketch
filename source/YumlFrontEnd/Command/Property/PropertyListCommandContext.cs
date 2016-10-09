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
        private readonly ClassifierDictionary _classifiers;
        private readonly IValidateNameService _propertyValidationNameService;
        private readonly PropertyNotificationService _notificationService;
        private readonly MessageSystem _messageSystem;

        public PropertyListCommandContext(
            Classifier classifier,
            ClassifierDictionary classifiers,
            IValidateNameService propertyValidationNameService,
            PropertyNotificationService notificationService,
            MessageSystem messageSystem)
        {
            _classifiers = classifiers;
            _propertyValidationNameService = propertyValidationNameService;
            _notificationService = notificationService;
            _messageSystem = messageSystem;
            All = new Query<Property>(() => classifier.Properties);
            New = new NewPropertyCommand(classifiers,classifier,notificationService);
            Visibility = new ShowOrHideAllObjectsInListCommand(classifier.Properties, messageSystem);
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
                _classifiers,
                domainObject,
                _propertyValidationNameService,
                _notificationService,_messageSystem);
        }
    }
}
