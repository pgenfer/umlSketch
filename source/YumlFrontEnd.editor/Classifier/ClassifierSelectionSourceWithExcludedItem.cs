using System.Linq;
using Yuml;

namespace YumlFrontEnd.editor
{
    /// <summary>
    /// list of classifiers where a separate item can be excluded from the complete list.
    /// Used for showing base classes of a class (since the class itself should not be visible in the list)
    /// </summary>
    internal class ClassifierSelectionSourceWithExcludedItem : ClassifierSelectionItemsSource
    {
        /// <summary>
        /// initial name of the classifier that should be excluded,
        /// must be updated whenever the name changes
        /// </summary>
        private string _excludedName;

        public ClassifierSelectionSourceWithExcludedItem(
            ClassifierDictionary classifiers, 
            ClassifierNotificationService notification,
            string classifierNameToExclude) : 
                base(classifiers, 
                    notification,
                    // return all classifiers but ignore the one with the given name
                    () => classifiers.OrderBy(x => x.Name).Where(x => x.Name != classifierNameToExclude))
        {
            _excludedName = classifierNameToExclude;
        }

        protected override void OnNameChanged(string oldName, string newName)
        {
            // the item which is excluded did change, so remember the new name
            if (oldName == _excludedName)
            {
                _excludedName = newName;
                return;
            }

            base.OnNameChanged(oldName, newName);
        }
    }
}