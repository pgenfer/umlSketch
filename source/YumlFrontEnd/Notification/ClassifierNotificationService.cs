using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Yuml.Notification;
using static System.Diagnostics.Contracts.Contract;

namespace Yuml
{
    /// <summary>
    /// service that is used to propagate any changes
    /// to classifiers
    /// </summary>
    public class ClassifierNotificationService
    {
        private readonly NameChangedNotificationMixin _nameChanged =  new NameChangedNotificationMixin();
        private readonly NewItemNotificationMixin _newItemAdded = new NewItemNotificationMixin();
        private readonly ItemDeletedNotificationMixin _itemDeleted = new ItemDeletedNotificationMixin();

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

        public void FireItemDeleted(string nameOfItem) => _itemDeleted.FireItemDeleted(nameOfItem);
        public event Action<string> ItemDeleted
        {
            add { _itemDeleted.ItemDeleted += value; }
            remove { _itemDeleted.ItemDeleted -= value; }
        }

        public void FirePropertiesRemoved(Classifier classifier, IEnumerable<string> names)
        {
            

        }
    }
}
