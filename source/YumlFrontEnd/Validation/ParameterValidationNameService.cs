using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml
{
    public class ParameterValidationNameService : ValidateNameBase
    {
        private readonly IEnumerable<Parameter> _parameters;

        public ParameterValidationNameService(IEnumerable<Parameter> parameters)
        {
            _parameters = parameters;
        }

        protected override bool NameAlreadyInUse(string newName) => _parameters.Any(x => x.Name == newName);
    }
}
