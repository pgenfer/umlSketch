using System.Diagnostics.Contracts;
using Caliburn.Micro;
using UmlSketch.Command;

namespace UmlSketch.Editor
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
            Contract.Requires(showOrHideCommand != null);

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
