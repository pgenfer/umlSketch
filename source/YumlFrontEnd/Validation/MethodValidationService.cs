using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml
{
    internal class MethodValidationService : IMethodNameValidationService
    {
        private readonly IEnumerable<Method> _methods;

        public MethodValidationService(IEnumerable<Method> methods)
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
            // TODO: implement method name comparision (check also parameters)
            return new Error("Not implemented");
        }
    }
}
