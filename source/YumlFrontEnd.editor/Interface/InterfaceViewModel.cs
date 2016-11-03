using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml;
using Yuml.Command;

namespace YumlFrontEnd.editor
{
    public class InterfaceViewModel : SingleItemViewModelBase<Classifier>
    {
        private readonly SingleInterfaceCommandContext _commands;

        public InterfaceViewModel(SingleInterfaceCommandContext commands) : base(commands)
        {
            _commands = commands;
        }
    }
}
