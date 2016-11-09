using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Command
{
    public class InterfaceListCommandContext : ListCommandContextBase<Implementation>
    {
        private readonly ImplementationList _implementations;

        public InterfaceListCommandContext(
            ImplementationList implementations,
            ClassifierDictionary classifiers,
            MessageSystem messageSystem )
        {
            _implementations = implementations;

            All = new Query<Implementation>(() => _implementations);
            New = new NewInterfaceCommand(implementations, classifiers, messageSystem);
            // TODO: check if visibility is also changed if interface was later added to list
            Visibility = new ShowOrHideAllObjectsInListCommand(_implementations, messageSystem);
        }
    }
}
