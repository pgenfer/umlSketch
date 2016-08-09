using Caliburn.Micro;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Yuml;
using Yuml.Commands;
using static System.Diagnostics.Contracts.Contract;

namespace YumlFrontEnd.editor
{
    /// <summary>
    /// mixin that supports editing of a name property.
    /// An additional flag tracks the current edit mode of the mixin.
    /// Has a depenency to the validation service to check whether
    /// the new name of the entity is valid
    /// </summary>
    internal class EditableNameMixin : AutoPropertyChange
    {
        /// <summary>
        /// mixin to handle the name interaction
        /// </summary>
        private readonly NameMixin _name = new NameMixin();
        /// <summary>
        /// the name before the edit operation started
        /// </summary>
        private string _originalName = string.Empty;
        /// <summary>
        /// flag that tracks whether the name can be edited at the moment
        /// </summary>
        private bool _editable;
        /// <summary>
        /// tracks if an error occured during the rename operation
        /// </summary>
        private bool _hasNameError;
        /// <summary>
        /// the error message in case an error occured during the 
        /// rename operation
        /// </summary>
        private string _nameErrorMessage;
        /// <summary>
        /// command that will be executed when the editing of the name
        /// is completed
        /// </summary>
        private readonly IRenameCommand _renameCommand;
        /// <summary>
        /// service used to validate if the new name would be appropriate
        /// </summary>
        private readonly IValidateNameService _validationService;

        public EditableNameMixin(IValidateNameService validationService, IRenameCommand renameCommand)
        {
            Requires(renameCommand != null);
            Requires(validationService != null);

            _validationService = validationService;
            _renameCommand = renameCommand;
        }

        public void StartEditing()
        {
            IsEditable = true;
            // the original name should always be
            // valid, otherwise we would set an invalid
            // name when canceling the editing
            if (!string.IsNullOrEmpty(Name))
                _originalName = Name;
        }

        public void StopEditing(Confirmation confirmation)
        {
            switch (confirmation)
            {
                case Confirmation.Confirmed:
                    // important: we can only disable edit mode 
                    // it text is not empty, otherwise the text box would not be visible
                    if (string.IsNullOrEmpty(Name))
                        return;
                    // only fire change event if name did really change
                    if (_originalName != Name)
                        _renameCommand.Do(Name);
                    _originalName = Name;
                    IsEditable = false;
                    break;
                case Confirmation.Canceled:
                    if (string.IsNullOrEmpty(_originalName))
                        return;
                    Name = _originalName;
                    IsEditable = false;
                    break;
            }

            // TODO: handle other cancel events
        }

        public string Name
        {
            get { return _name.Name; }
            set
            {
                _name.Name = value;
                ValidateName();
                // event must be fired, because
                // Name can be changed back to original if
                // user aborts editing, in that case
                // view does not know about the correct name
                RaisePropertyChanged();
            }
        }

        private void ValidateName()
        {
            var result = _validationService.ValidateNameChange(_originalName, Name);
            HasNameError = result.HasError;
            NameErrorMessage = result.Message;
        }

        public bool IsEditable
        {
            get { return _editable; }
            set { _editable = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// tracks if an error occured during the rename operation
        /// </summary>
        public bool HasNameError
        {
            get { return _hasNameError; }
            set { _hasNameError = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// the error message in case an error occured during the 
        /// rename operation
        /// </summary>
        public string NameErrorMessage
        {
            get { return _nameErrorMessage; }
            set { _nameErrorMessage = value; RaisePropertyChanged(); }
        }

        public override string ToString() => _name.ToString();
    }
}
