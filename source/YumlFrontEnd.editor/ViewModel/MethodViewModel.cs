using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml;
using Yuml.Command;

namespace YumlFrontEnd.editor
{
    internal class MethodViewModel : SingleItemViewModelBase<Method>
    {
        public MethodViewModel(ISingleCommandContext commands) : base(commands)
        {
        }
    }
}
