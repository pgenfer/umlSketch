using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml;
using Yuml.Service;

namespace Yuml.Command
{
    public class SingleParameterCommandContext : SingleCommandContextBase<Parameter>
    {
        public SingleParameterCommandContext(
            BaseList<Parameter> memberList,
            Parameter member,
            ClassifierDictionary classifiers,
            MessageSystem messageSystem,
            IAskUserBeforeDeletionService askUserService) : 
            base(memberList, member, messageSystem,askUserService)
        {
            ChangeType = new ChangeParameterTypeCommand(classifiers, member, messageSystem);
        }

        public IChangeTypeCommand ChangeType { get; }
    }
}
