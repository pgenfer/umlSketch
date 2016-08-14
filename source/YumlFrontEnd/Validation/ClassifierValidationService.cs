using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml;

namespace Yuml
{ 
    public class ClassifierValidationService : ValidateNameBase
    {
        private readonly ClassifierDictionary _classifiers;

        public ClassifierValidationService(ClassifierDictionary classifiers)
        {
            _classifiers = classifiers;
        }

        protected override bool NameAlreadyInUse(string newName) => !_classifiers.IsClassNameFree(newName);
    }
}
