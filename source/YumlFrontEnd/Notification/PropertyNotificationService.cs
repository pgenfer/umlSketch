using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Notification
{
    public class PropertyNotificationService : INameChangedNotificationService
    {
        private readonly NameChangedNotificationMixin _nameChanged = new NameChangedNotificationMixin();
        private readonly NewItemNotificationMixin _newItemAdded = new NewItemNotificationMixin();
        private readonly TypeChangeNotificationMixin _typeChanged = new TypeChangeNotificationMixin();

        public void FireNameChange(string oldName, string newName) => _nameChanged.FireNameChange(oldName, newName);
        public event Action<string, string> NameChanged
        {
            add { _nameChanged.NameChanged += value; }
            remove { _nameChanged.NameChanged -= value; }
        }

        public void FireNewItemCreated(string name) => _newItemAdded.FireNewItemCreated(name);
        public event Action<string> NewItemCreated
        {
            add { _newItemAdded.NewItemCreated += value; }
            remove { _newItemAdded.NewItemCreated -= value; }
        }

        public virtual void FireTypeChanged(string oldName, string newName) => _typeChanged.FireTypeChanged(oldName, newName);
        public event Action<string, string> TypeChanged
        {
            add { _typeChanged.TypeChanged += value; }
            remove { _typeChanged.TypeChanged -= value; }
        }
    }
}
