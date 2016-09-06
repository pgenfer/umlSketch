using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Command
{
    /// <summary>
    /// command interface is used when an assigned classifier 
    /// is changed (e.g. the return type of a method, the type of a property etc...)
    /// </summary>
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
}
