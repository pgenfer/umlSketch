using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml
{
    /// <summary>
    /// interface for a service that is used to validate the name of an entity.
    /// There can be different implementations of the service,
    /// depending on which entity has the name property (Classifier, Property, Method etc...)
    /// </summary>
    public interface IValidateNameService
    {
        /// <summary>
        /// validates if old name can be changed to new name.
        /// </summary>
        /// <param name="oldName">old name of the entity</param>
        /// <param name="newName">new name the entity would have</param>
        /// <returns>success result if names can be changed, otherwise an error</returns>
        ValidationResult ValidateNameChange(string oldName, string newName);
    }
}
