using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Diagnostics.Contracts.Contract;

namespace Yuml
{
    /// <summary>
    /// mixin that contains code that is used to propagate 
    /// changes of a name
    /// </summary>
    internal class NameChangedNotificationMixin
    {
        /// <summary>
        /// fired when the name of a classifier changed
        /// </summary>
        public event Action<string, string> NameChanged;

        /// <summary>
        /// should be called when the name of a classifier
        /// was changed by a command
        /// </summary>
        /// <param name="oldName">old name of the classifier</param>
        /// <param name="newName">new name of the classifier</param>
        public virtual void FireNameChange(string oldName, string newName)
        {
            Requires(oldName != newName);

            NameChanged?.Invoke(oldName, newName);
        }
    }
}
