using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml
{
    /// <summary>
    /// basic implementation for validation 
    /// </summary>
    public abstract class ValidateNameBase : IValidateNameService
    {
        /// <summary>
        /// checks if the given name is already in used.
        /// Must be implemented by derived classes
        /// </summary>
        /// <param name="newName"></param>
        /// <returns></returns>
        protected abstract bool NameAlreadyInUse(string newName);

        /// <summary>
        /// validates if the given name can be used.
        /// </summary>
        /// <param name="oldName">old name that was used before</param>
        /// <param name="newName">new name that should be used</param>
        /// <returns>a ValidationResult object. If an error occured, the validation object
        /// contains the error message.</returns>
        public ValidationResult ValidateNameChange(string oldName, string newName)
        {
            if (string.IsNullOrEmpty(newName))
                return new Error(Strings.NameMustNotBeEmpty);
            // name has not changed, so it can be reused
            if (oldName == newName || string.IsNullOrEmpty(oldName)) // no old name => initial case
                return new Success();
            if (NameAlreadyInUse(newName))
                return new Error(Strings.NameAlreadyExists);
            return new Success();
        }
    }
}
