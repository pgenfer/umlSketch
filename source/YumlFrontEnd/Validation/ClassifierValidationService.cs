using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml
{
    public class ClassifierValidationService : IValidateNameService
    {
        private readonly ClassifierDictionary _classifiers;

        public ClassifierValidationService(ClassifierDictionary classifiers)
        {
            _classifiers = classifiers;
        }

        /// <summary>
        /// checks if the name of a classifier can be changed
        /// from old name to new name.
        /// Possible if there is no other classifier with the same name
        /// </summary>
        /// <param name="oldName"></param>
        /// <param name="newName"></param>
        /// <returns></returns>
        public ValidationResult ValidateNameChange(string oldName, string newName)
        {
            if (string.IsNullOrEmpty(newName))
                return new Error(Strings.ClassNameMustNotBeEmpty);
            // name has not changed, so it can be reused
            if (oldName == newName || string.IsNullOrEmpty(oldName)) // no old name => initial case
                return new Success();
            // otherwise another class uses this name already
            if (!_classifiers.IsClassNameFree(newName))
                return new Error(Strings.ClassNameAlreadyExists);
            return new Success();
        }
    }
}
