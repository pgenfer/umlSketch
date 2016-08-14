using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Notification
{
    public class PropertyNotificationService
    {
        private readonly NameChangedNotificationMixin _nameChanged = new NameChangedNotificationMixin();
        private readonly NewItemNotificationMixin _newItemAdded = new NewItemNotificationMixin();

        public void FireNameChange(string oldName, string newName) => _nameChanged.FireNameChange(oldName, newName);

        public event Action<string, string> NameChanged
        {
            add { _nameChanged.NameChanged += value; }
            remove { _nameChanged.NameChanged -= value; }
        }

        public void FireNewItemCreated() => _newItemAdded.FireNewItemCreated();

        public event Action NewItemCreated
        {
            add { _newItemAdded.NewItemCreated += value; }
            remove { _newItemAdded.NewItemCreated -= value; }
        }
    }
}
