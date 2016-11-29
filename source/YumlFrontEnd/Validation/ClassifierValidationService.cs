using UmlSketch.DomainObject;

namespace UmlSketch.Validation
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
