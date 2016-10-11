using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using static System.Diagnostics.Contracts.Contract;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Command
{
    /// <summary>
    /// command interface is used when an assigned classifier 
    /// is changed (e.g. the return type of a method, the type of a property etc...)
    /// </summary>
    [ContractClass(typeof(ChangeTypeCommandContract))]
    public interface IChangeTypeCommand
    {
        /// <summary>
        /// the type was changed from old to new
        /// </summary>
        /// <param name="nameOfOldType"></param>
        /// <param name="nameOfNewType"></param>
        void ChangeType(string nameOfOldType, string nameOfNewType);
    }

    /// <summary>
    /// extension of the change type command.
    /// The existing type can also be set to null (e.g. when changing base class of a class)
    /// </summary>
    [ContractClass(typeof(ChangeTypeToNullCommandContract))]
    public interface IChangeTypeToNullCommand : IChangeTypeCommand
    {
        /// <summary>
        /// type was changed from old to null (e.g. the base class of a class).
        /// This method is optional, not all command implementations will need it.
        /// </summary>
        /// <param name="nameOfOldType"></param>
        void ClearType(string nameOfOldType);
        /// <summary>
        /// Called when the type was null before
        /// and is now set to a new value
        /// </summary>
        /// <param name="nameOfNewType"></param>
        void SetNewType(string nameOfNewType);
    }

    [ContractClassFor(typeof(IChangeTypeCommand))]
    internal abstract class ChangeTypeCommandContract : IChangeTypeCommand
    {
        void IChangeTypeCommand.ChangeType(string nameOfOldType, string nameOfNewType) =>
            Requires(nameOfOldType != nameOfNewType);
    }

    [ContractClassFor(typeof(IChangeTypeToNullCommand))]
    internal abstract class ChangeTypeToNullCommandContract : IChangeTypeToNullCommand
    {
        public abstract void ChangeType(string nameOfOldType, string nameOfNewType);

        void IChangeTypeToNullCommand.ClearType(string nameOfOldType) =>
            Requires(!string.IsNullOrEmpty(nameOfOldType));

        void IChangeTypeToNullCommand.SetNewType(string nameOfNewType) =>
            Requires(!string.IsNullOrEmpty(nameOfNewType));
    }
}
