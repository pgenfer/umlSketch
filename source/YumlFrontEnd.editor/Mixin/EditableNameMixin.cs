﻿using Caliburn.Micro;
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
        /// the name before the edit operation started
        /// </summary>
        private string _originalName = string.Empty;
        /// <summary>
        /// flag that tracks whether the name can be edited at the moment
        /// </summary>
        private bool _editable = false;

        public void StartEditing()
        {
            IsEditable = true;
            _originalName = Name;
        }

        public void StopEditing(EventArgs args)
        {
            var keyboardArgs = args as KeyEventArgs;
            if(keyboardArgs != null)
            {
                switch(keyboardArgs.Key)
                {
                    case Key.Enter:
                        // important: we can only disable edit mode 
                        // it text is not empty, otherwise the text box would not be visible
                        if (string.IsNullOrEmpty(Name))
                            return;
                        _originalName = Name;
                        IsEditable = false;
                        break;
                    case Key.Escape:
                        if (string.IsNullOrEmpty(_originalName))
                            return;
                        Name = _originalName;
                        IsEditable = false;
                        break;
                }
            }
            // TODO: handle other cancel events
        }

        public string Name
        {
            get { return _name.Name; }
            set { _name.Name = value; NotifyOfPropertyChange(nameof(Name)); }
        }

        /// <summary>
        /// the name that is stored in this mixin before 
        /// the edit operation completes
        /// </summary>
        public string OriginalName { get { return _originalName; } }

        public bool IsEditable
        {
            get { return _editable; }
            set { _editable = value; NotifyOfPropertyChange(nameof(IsEditable)); }
        }

        public override string ToString() => _name.ToString();
    }
}