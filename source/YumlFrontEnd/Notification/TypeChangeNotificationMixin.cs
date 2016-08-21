using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml
{
    /// <summary>
    /// notification that is fired when the type
    /// of a member is changed (e.g. the type of a property
    /// or the return type of a method)
    /// </summary>
    public class TypeChangeNotificationMixin
    {
        public event Action<string, string> TypeChanged;
        public void FireTypeChanged(string oldName, string newName) => TypeChanged?.Invoke(oldName, newName);
    }
}
