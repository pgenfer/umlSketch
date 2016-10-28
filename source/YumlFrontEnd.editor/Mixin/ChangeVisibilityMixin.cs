using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Yuml.Command;
using static System.Diagnostics.Contracts.Contract;

namespace YumlFrontEnd.editor
{
    /// <summary>
    /// interaction logic with the change visibility singleObjectCommand.
    /// Mixin can be used to map the singleObjectCommand with the UI.
    /// </summary>
    public class ChangeVisibilityMixin : PropertyChangedBase
    {
        private readonly IShowOrHideCommand _showOrHideCommand;
       
        public ChangeVisibilityMixin(IShowOrHideCommand showOrHideCommand)
        {
            Requires(showOrHideCommand != null);

            _showOrHideCommand = showOrHideCommand;
        }

        public bool IsVisible => _showOrHideCommand.IsVisible;
        
        public void ShowOrHide()
        {
            _showOrHideCommand.ChangeVisibility();
            NotifyOfPropertyChange(nameof(IsVisible));
        }
    }
}
