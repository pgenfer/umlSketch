using System.Diagnostics.Contracts;
using static System.Diagnostics.Contracts.Contract;

namespace Yuml.Command
{
    /// <summary>
    /// generic command used to rename a domain object.
    /// </summary>
    public interface IRenameCommand
    {
        void Rename(string newName);
        ValidationResult CanRenameWith(string newName);
    }
}