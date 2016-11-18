using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Command
{
    public class ListParameterContext : ListCommandContextBase<Parameter>
    {
        public ListParameterContext(
            ParameterList parameters,
            ClassifierDictionary classifiers,
            MessageSystem messageSystem)
            :base(parameters,classifiers,messageSystem)
        {
        }
    }
}
