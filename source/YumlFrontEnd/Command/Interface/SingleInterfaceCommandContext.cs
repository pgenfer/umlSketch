using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml.Command.Interface;

namespace Yuml.Command
{
    public class SingleInterfaceCommandContext : SingleCommandContextBase<Implementation>
    {
        public ChangeInterfaceOfClassifierCommand ChangeInterface { get; }
        public QueryAvailableInterfaces AvailableInterfaces { get; }

        public SingleInterfaceCommandContext(
            ImplementationList implementationList,
            Implementation existingInterface,
            ClassifierDictionary classifiers,
            MessageSystem messageSystem):
            base(implementationList,existingInterface,messageSystem)
        {
            ChangeInterface = new ChangeInterfaceOfClassifierCommand(
                existingInterface,
                classifiers,
                messageSystem);
            AvailableInterfaces = new QueryAvailableInterfaces(existingInterface, classifiers);
        }
    }
}

