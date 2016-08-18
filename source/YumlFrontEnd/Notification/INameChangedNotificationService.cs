using System;
using System.Diagnostics.Contracts;
using Yuml.Command;
using static System.Diagnostics.Contracts.Contract;

namespace Yuml
{
    [ContractClass(typeof(NameChangedNotificationContract))]
    public interface INameChangedNotificationService
    {
        event Action<string, string> NameChanged;
        void FireNameChange(string oldName, string newName);
    }

    /// <summary>
    /// contract definition for rename command
    /// </summary>
    [ContractClassFor(typeof(INameChangedNotificationService))]
    internal abstract class NameChangedNotificationContract : INameChangedNotificationService
    {
        event Action<string, string> INameChangedNotificationService.NameChanged
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        void INameChangedNotificationService.FireNameChange(string oldName, string newName) =>
            Requires(oldName != newName);
    }
}