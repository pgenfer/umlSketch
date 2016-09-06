using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml.Notification;

namespace Yuml
{
    /// <summary>
    /// collection of available notification services
    /// </summary>
    public class NotificationServices
    {
        public NotificationServices(
            ClassifierNotificationService classifier = null, 
            PropertyNotificationService property = null,
            MethodNotificationService method = null,
            RelationNotificationService relation = null)
        {
            Classifier = classifier;
            Property = property;
            Method = method;
            Relation = relation;
        }

        public ClassifierNotificationService Classifier { get; }
        public PropertyNotificationService Property { get; }
        public MethodNotificationService Method { get; }
        public RelationNotificationService Relation { get;}
    }
}
