using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Notification
{
    public class MethodNotificationService : INameChangedNotificationService
    {
        private readonly NameChangedNotificationMixin _nameChanged = new NameChangedNotificationMixin();

        public void FireNameChange(string oldName, string newName) => _nameChanged.FireNameChange(oldName, newName);

        public event Action<string, string> NameChanged
        {
            add { _nameChanged.NameChanged += value; }
            remove { _nameChanged.NameChanged -= value; }
        }
    }
}
