using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml
{
    public class ClassifierValidationService
    {
        private readonly ClassifierDictionary _classifiers;

        public ClassifierValidationService(ClassifierDictionary classifiers)
        {
            _classifiers = classifiers;
        }

        public ClassifierValidationService() { }

        /// <summary>
        /// validates whether the given name would be valid for a
        /// classifier
        /// </summary>
        /// <param name="oldName">old name of this classifier</param>
        /// <param name="newName">new name the classifier would get</param>
        /// <returns></returns>
        public ValidationResult CheckName(string oldName, string newName)
        {
            if (string.IsNullOrEmpty(newName))
                return new Error(Strings.ClassNameMustNotBeEmpty);
            // name has not changed, so it can be reused
            if (oldName == newName || string.IsNullOrEmpty(oldName)) // no old name => initial case
                return new Success();
            // otherwise another class uses this name already
            if (!_classifiers.IsClassNameFree(newName))
                return  new Error(Strings.ClassNameAlreadyExists);
            return new Success();
        }
    }
}
