using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml;
using Yuml.Command;


namespace YumlFrontEnd.editor
{
    /// <summary>
    /// view model for handling a complete list of classifiers.
    /// A separate view model is created for every classifier
    /// </summary>
    internal class ClassifierListViewModel : ListViewModelBase<Classifier>
    {
        public ClassifierListViewModel(IListCommandContext<Classifier> commands) : base(commands)
        {
        }

        /// <summary>
        /// always hide the expand button in the class list
        /// TODO: later when we have relations, we could let the user expand/collapse the class list
        /// </summary>
        public override bool CanExpand => false;

        public void Collapse()
        {
            foreach (var item in Items)
                ((ClassifierViewModel) item).Collapse();
        }

        protected override void UpdateItemList()
        {
            // we store the expand positions of the existing classifiers
            // and restore them after the update.
            // Unfortunately we must cast the single items to the correct type here
            var expandStatesOfClassifiers = Items.ToDictionary(x => x.Name, y => ((ClassifierViewModel) y).IsExpanded);
            base.UpdateItemList();
            foreach (var classifier in Items.OfType<ClassifierViewModel>())
            {
                bool isExpanded;
                // items that were not in the list will be expanded by default, otherwise
                // use the expand state the item had before
                classifier.IsExpanded = 
                    !expandStatesOfClassifiers.TryGetValue(classifier.Name, out isExpanded) || 
                    isExpanded;
            }
        }
    }
}
