using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml
{
    /// <summary>
    /// fired when an item was deleted.
    /// The event contains the name of the item which was deleted.
    /// </summary>
    internal class ItemDeletedNotificationMixin
    {
        public event Action<string> ItemDeleted;
        public void FireItemDeleted(string nameOfItem) => ItemDeleted?.Invoke(nameOfItem);
    }
}
