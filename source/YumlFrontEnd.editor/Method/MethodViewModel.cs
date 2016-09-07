using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml;
using Yuml.Command;

namespace YumlFrontEnd.editor
{
    internal class MethodViewModel : SingleItemViewModelBase<Method,ISingleCommandContext>
    {
        public MethodViewModel(ISingleCommandContext commands) : base(commands)
        {
        }

        protected override void CustomInit()
        {
            // TODO: add custom implementation
        }
    }
}
