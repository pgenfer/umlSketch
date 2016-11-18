using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Command
{
    public class InterfaceListCommandContext : ListCommandContextBase<Implementation>
    {
        public InterfaceListCommandContext(
            ImplementationList implementations,
            ClassifierDictionary classifiers,
            MessageSystem messageSystem )
            :base(implementations,classifiers,messageSystem)
        {
            // TODO: check if visibility is also changed if interface was later added to list
        }
    }
}
