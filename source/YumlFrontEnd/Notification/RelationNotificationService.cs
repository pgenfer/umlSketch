using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Notification
{
    public class RelationNotificationService
    {
        public event Action<string, string, string> BaseClassChanged;
        public event Action<string> BaseClassRemoved;
        public event Action<string, string> BaseClassAdded;

        public void FireBaseClassRemoved(string subClass) => 
            BaseClassRemoved?.Invoke(subClass);

        public void FireBaseClassAdded(string subClass, string newBaseClass) =>
            BaseClassAdded?.Invoke(subClass, newBaseClass);

        public void FireBaseClassChanged(
            string subClass,
            string oldBaseClass,
            string newBaseClass) =>
            BaseClassChanged?.Invoke(subClass, oldBaseClass, newBaseClass);
    }
}
