using System;
using System.Diagnostics.Contracts;
using System.Linq;
using Caliburn.Micro;
using Common;
using Yuml;

namespace YumlFrontEnd.editor
{
    /// <summary>
    /// list of available classifiers.
    /// All list controls will have the same list
    /// of classifiers available.
    /// </summary>
    public class ClassifierSelectionItemsSource  : BindableCollection<ClassifierItemViewModel>
    {
        public ClassifierSelectionItemsSource(
            ClassifierDictionary classifiers,
            ClassifierNotificationService notification)
        {
            foreach (var classifier in classifiers.OrderBy(x => x.Name))
                Add(new ClassifierItemViewModel(classifier.Name));
            
            // react on renaming of item
            notification.NameChanged += (old, @new) =>
            {
                var item = ByName(old);
                // create a temporary item so that we get the new index
                var tmp = new ClassifierItemViewModel(@new);
                var oldIndex = IndexOf(item);
                // since all item names must be unique, the
                // new item can never be in the list
                // so the index we get is always the index where the item should be added
                var newIndex = FindNewItemPosition(tmp);
                // rename the item and move it to the new position
                item.Name = @new;
                Move(oldIndex, newIndex);
            };
            // add new item to list
            notification.NewItemCreated += x =>
            {
                var newItem = new ClassifierItemViewModel(x);
                var newIndex = FindNewItemPosition(newItem);
                Insert(newIndex, newItem);
            };
            
            // TODO: if item is deleted, remove it from the list
        }

        /// <summary>
        /// returns the classifier with the given name.
        /// </summary>
        /// <param name="name">name of the classifier. A classifier
        /// with this name must exist in the list and there may only be one 
        /// classifier with this name</param>
        /// <returns></returns>
        public ClassifierItemViewModel ByName(string name)
        {
            Contract.Requires(!string.IsNullOrEmpty(name));
            Contract.Requires(this.Count(x => x.Name == name) == 1);
            
            return this.Single(x => x.Name == name);
        }

        private int FindNewItemPosition(INamed item) => BinarySearch(item, 0,Count - 1);

        private int BinarySearch(INamed item, int min, int max)
        {
            while (min < max)
            {
                var mid = (min + max) / 2;
                var result = string.Compare(item.Name, this[mid].Name, StringComparison.OrdinalIgnoreCase);
                if (result == 0)
                    return mid;
                if (result < 0)
                    max = mid - 1;
                if (result > 0)
                    min = mid + 1;
            }
            return max;
        }
    }
}