using System.Collections;
using System.Collections.Generic;
using static System.Diagnostics.Contracts.Contract;

namespace Yuml
{
    public class ParameterList : BaseList<Parameter>
    {
        public Parameter CreateParameter(Classifier type, string name)
        {
            // TODO: also check that type is not void
            Requires(type != null);
            Requires(!string.IsNullOrEmpty(name));

            return AddNewMember(new Parameter(type, name));
        }
    }
}