using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
