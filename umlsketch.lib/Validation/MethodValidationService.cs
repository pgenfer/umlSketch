using UmlSketch.DomainObject;

namespace UmlSketch.Validation
{
    internal class MethodValidationService : IMethodNameValidationService
    {
        private readonly MethodList _methods;

        public MethodValidationService(MethodList methods)
        {
            _methods = methods;
        }

        public ValidationResult ValidateNameChange(Method method, string newName)
        {
            var oldName = method.Name;
            if (string.IsNullOrEmpty(newName))
                return new Error(Strings.NameMustNotBeEmpty);
            // name has not changed, so it can be reused
            if (oldName == newName || string.IsNullOrEmpty(oldName)) // no old name => initial case
                return new Success();
            var hasMethodWithSameSignature = _methods.ContainsMethodWithSignature(newName, method.Parameters);
            if (hasMethodWithSameSignature)
                return new Error(Strings.MethodWithSameSignatureExists);
            return new Success();
        }
    }
}
