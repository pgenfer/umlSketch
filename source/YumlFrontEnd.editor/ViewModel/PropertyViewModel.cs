using Caliburn.Micro;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml;
using Yuml.Command;

namespace YumlFrontEnd.editor
{
    internal class PropertyViewModel : SingleItemViewModelBase<Property>
    {
        public PropertyViewModel(ISinglePropertyCommands commands):base(commands)
        {
            // TODO: get the command for changing the property type here
        }
    }
}
