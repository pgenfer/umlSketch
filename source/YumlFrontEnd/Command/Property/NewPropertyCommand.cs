using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml.Notification;
using static System.Diagnostics.Contracts.Contract;

namespace Yuml.Command
{
    /// <summary>
    /// command used to create new new property for a classifier
    /// </summary>
    public class NewPropertyCommand : INewCommand
    {
        private readonly ClassifierDictionary _availableClassifiers;

        /// <summary>
        /// the classifier where the new property will be placed
        /// </summary>
        private readonly Classifier _classifier;

        private readonly PropertyNotificationService _notificationService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="availableClassifiers"></param>
        /// <param name="classifier"></param>
        /// <param name="notificationService"></param>
        public NewPropertyCommand(
            ClassifierDictionary availableClassifiers,
            Classifier classifier,
            PropertyNotificationService notificationService)
        {
            Requires(classifier != null);
            Requires(notificationService != null);
            Requires(availableClassifiers != null);

            _availableClassifiers = availableClassifiers;
            _classifier = classifier;
            _notificationService = notificationService;
        }

        public void CreateNew()
        {
            var property = _classifier.CreateNewPropertyWithBestInitialValues(_availableClassifiers);
            _notificationService.FireNewItemCreated(property.Name);
        }
    }
}
