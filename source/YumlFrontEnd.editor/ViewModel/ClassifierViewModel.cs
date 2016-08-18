using Caliburn.Micro;
using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Yuml;
using Yuml.Command;
using static System.Diagnostics.Contracts.Contract;

namespace YumlFrontEnd.editor
{
    /// <summary>
    /// view model for interaction with a single classifier object
    /// </summary>
    internal class ClassifierViewModel : SingleItemViewModelBase<Classifier>
    {
        private readonly ExpandableMixin _expanded = new ExpandableMixin();
       
        public ClassifierViewModel(
            ISingleClassifierCommands commands):base(commands)
        {
            Properties = new PropertyListViewModel(commands.CommandsForProperties);
            Methods = new MethodListViewModel(commands.CommandsForMethods);
                
            _expanded.PropertyChanged += (s, e) => NotifyOfPropertyChange(e.PropertyName);
        }

        public PropertyListViewModel Properties { get; private set; }
        public MethodListViewModel Methods { get; private set; }

        public bool IsExpanded => _expanded.IsExpanded;
        public void ExpandOrCollapse() => _expanded.ExpandOrCollapse();
    }
}
