﻿using System.Diagnostics.Contracts;
using Caliburn.Micro;
using Common;
using UmlSketch.Command;
using UmlSketch.Validation;

namespace UmlSketch.Editor
{
    /// <summary>
    /// mixin that supports editing of a name property.
    /// An additional flag tracks the current edit mode of the mixin.
    /// Has a depenency to the validation service to check whether
    /// the new name of the entity is valid
    /// </summary>
    public class EditableNameMixin : PropertyChangedBase, IEditableName
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
        private readonly IRenameCommand _command;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="renameCommand">command that is executed to perform the rename action</param>
        public EditableNameMixin(IRenameCommand renameCommand)
        {
            Contract.Requires(renameCommand != null);
        
            _command = renameCommand;
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
                    //if (string.IsNullOrEmpty(Name))
                    //    return;
                    // only fire change event if name did really change
                    if (_originalName != Name)
                    {
                        if (!HasNameError)
                        {
                            Rename(Name);
                            _originalName = Name;
                        }
                        else
                        {
                            // error: reset the name
                            Name = _originalName;
                        }
                        
                    }
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
                if (_name.Name == value)
                    return;
                _name.Name = value;
                ValidateName();
                // event must be fired, because
                // Name can be changed back to original if
                // user aborts editing, in that case
                // view does not know about the correct name
                NotifyOfPropertyChange();
            }
        }

        private void ValidateName()
        {
            var result = CanRenameWith(Name);
            HasNameError = result.HasError;
            NameErrorMessage = result.Message;
        }

        public bool IsEditable
        {
            get { return _editable; }
            set { _editable = value; NotifyOfPropertyChange(); }
        }

        /// <summary>
        /// tracks if an error occured during the rename operation
        /// </summary>
        public bool HasNameError
        {
            get { return _hasNameError; }
            set { _hasNameError = value; NotifyOfPropertyChange(); }
        }

        /// <summary>
        /// the error message in case an error occured during the 
        /// rename operation
        /// </summary>
        public string NameErrorMessage
        {
            get { return _nameErrorMessage; }
            set { _nameErrorMessage = value; NotifyOfPropertyChange(); }
        }

        public override string ToString() => _name.ToString();
        public void Rename(string newName) => _command.Rename(newName);
        public ValidationResult CanRenameWith(string newName) => _command.CanRenameWith(newName);

        public void ValidateAndClose()
        {
            var confirmation = HasNameError ? Confirmation.Canceled : Confirmation.Confirmed;
            StopEditing(confirmation);
        }
    }
}
