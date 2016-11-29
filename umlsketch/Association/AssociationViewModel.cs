using System.Collections.Generic;
using UmlSketch.Command;
using UmlSketch.DomainObject;

namespace UmlSketch.Editor
{
    internal class AssociationViewModel : SingleItemViewModelBase<Association,ISingleAssociationCommands>
    {
        /// <summary>
        /// viewmodel that holds the current association
        /// </summary>
        private AssociationItemViewModel _association;
        /// <summary>
        /// mixin that handles the setting of the target classifier
        /// </summary>
        private SelectClassifierMixin _selectedClassifierMixin;

        /// <summary>
        /// property is only used for retrieving the inital
        /// association value from the domain object
        /// </summary>
        public RelationType InitialAssociationType { get; set; }
        /// <summary>
        /// name of the target classifier of this relation
        /// </summary>
        public string InitialTargetClassiferName { get; set; }

        internal override void InitCommands(ISingleAssociationCommands commands)
        {
            base.InitCommands(commands);

            // TO DO:
            // add commands for
            // 1.) changing relation type
            // 2.) changing relation end
            // 3.) change name of relation
            // 4.) draw relation
        }

        public AssociationItemViewModel SelectedAssociation
        {
            get { return _association; }
            set
            {
                _association = value;
                _commands.ChangeAssociationTypeCommand.ChangeAssociation(_association.AssociationType);
                NotifyOfPropertyChange();
            }
        }

        /// <summary>
        /// list of available associations for this type
        /// </summary>
        public AssociationItemList Associations { get; } = new AssociationItemList();

        protected override void CustomInit()
        {
            // set the correct assocation, not via property, otherwise command is fired
            _association = Associations[InitialAssociationType];
            // and the target of the association
            _selectedClassifierMixin = new SelectClassifierMixin(
                 Context.CreateClassifierItemSource(x => !x.IsSystemType), 
                _commands.ChangeAssociationTargetCommand);
            SelectClassifierByName(InitialTargetClassiferName);
        }

        public IEnumerable<ClassifierItemViewModel> Classifiers => _selectedClassifierMixin.Classifiers;

        public ClassifierItemViewModel SelectedClassifier
        {
            get { return _selectedClassifierMixin.SelectedClassifier; }
            set { _selectedClassifierMixin.SelectedClassifier = value; }
        }

        public void SelectClassifierByName(string classifierName) => _selectedClassifierMixin.SelectClassifierByName(classifierName);
    }
}
