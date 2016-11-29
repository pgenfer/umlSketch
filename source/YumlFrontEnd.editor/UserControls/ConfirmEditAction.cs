using System.Windows.Input;
using Caliburn.Micro;

namespace UmlSketch.Editor
{
    /// <summary>
    /// confirmation of the edit operation currently depends
    /// on the KeyEventArgs, this is not very good because
    /// the EditableNameMixin would then depend on the system specific
    /// input handling.
    /// Instead, we define this special value here which works as a
    /// converter. The action will be registered during configuration of the Application.
    /// (see Bootstrapper).
    /// See following link for more details:
    /// http://www.jerriepelser.com/blog/passing-custom-parameters-to-caliburn-micro-actions
    /// </summary>
    public class ConfirmEditAction
    {
        
        /// <summary>
        /// key that is used to identify this confirmation
        /// in the special value dictionary
        /// </summary>
        public string ParameterName { get; } = "$confirmation";

        /// <summary>
        /// gets the given keyboard event arguments and converts
        /// them to the correct confirmation result.
        /// </summary>
        /// <param name="context"></param>
        /// <returns>A value of type Confirmation</returns>
        public object ConvertKeyToConfirmation(ActionExecutionContext context)
        {
            var keyArgs = context?.EventArgs as KeyEventArgs;
            if (keyArgs == null)
                return Confirmation.None;
            switch (keyArgs.Key)
            {
                case Key.Enter:
                    return Confirmation.Confirmed;
                case Key.Escape:
                    return Confirmation.Canceled;
            }
            return Confirmation.None;
        }
    }
}
