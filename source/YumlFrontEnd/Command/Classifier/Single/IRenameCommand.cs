using System.Diagnostics.Contracts;
using static System.Diagnostics.Contracts.Contract;

namespace Yuml.Commands
{
    /// <summary>
    /// base interface used for renaming
    /// </summary>
    [ContractClass(typeof(RenameCommandContract))]
    public interface IRenameCommand
    {
        void Do(string newName);
    }

    /// <summary>
    /// contract definition for rename command
    /// </summary>
    [ContractClassFor(typeof(IRenameCommand))]
    internal abstract class RenameCommandContract : IRenameCommand
    {
        /// <summary>
        /// rename can only be executed when the new name is set
        /// </summary>
        /// <param name="newName"></param>
        void IRenameCommand.Do(string newName) => Requires(!string.IsNullOrEmpty(newName));
    }
}