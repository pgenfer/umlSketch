using Caliburn.Micro;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace YumlFrontEnd.editor
{
    /// <summary>
    /// mixin that supports editing of a name property.
    /// An additional flag tracks the current edit mode of the mixin.
    /// </summary>
    internal class EditableNameMixin : PropertyChangedBase
    {
        /// <summary>
        /// mixin to handle the name interaction
        /// </summary>
        private readonly NameMixin _name = new NameMixin();
        /// <summary>
        /// flag that tracks whether the name can be edited at the moment
        /// </summary>
        private bool _editable = false;

        public void StartEditing()
        {
            IsEditable = true;
        }

        public void StopEditing(EventArgs args)
        {
            var keyboardArgs = args as KeyEventArgs;
            // stop edit mode if special key is pressed
            if (keyboardArgs != null &&
               (keyboardArgs.Key == Key.Enter ||
                keyboardArgs.Key == Key.Return ||
                keyboardArgs.Key == Key.Escape))
                IsEditable = false;
            // TODO: handle other cancel events
        }

        public string Name
        {
            get { return _name.Name; }
            set { _name.Name = value; NotifyOfPropertyChange(nameof(Name)); }
        }

        public bool IsEditable
        {
            get { return _editable; }
            set { _editable = value; NotifyOfPropertyChange(nameof(IsEditable)); }
        }

        public override string ToString() => _name.ToString();
    }
}
