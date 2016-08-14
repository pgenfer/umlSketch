using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml
{
    /// <summary>
    /// notification which can be used when a new item
    /// was created. 
    /// Currently the new item itself is not propagated,
    /// the receiver has to pull the information itself
    /// (maybe this can be changed if necessary)
    /// </summary>
    internal class NewItemNotificationMixin
    {
        public event Action NewItemCreated;
        public void FireNewItemCreated() => NewItemCreated?.Invoke();
    }
}
