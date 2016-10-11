using System;
using System.Collections.Generic;
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
    public class ClassifierSelectionItemsSource : BindableCollection<ClassifierItemViewModel>
    {
        private readonly MessageSystem _messageSystem;
        private readonly ClassifierDictionary _classifiers;

        /// <summary>
        /// only for test, should not be used in production code
        /// </summary>
        public ClassifierSelectionItemsSource()
        {
            _classifiers = new ClassifierDictionary();
        }


        /// <summary>
        /// internal constructor allows user to define
        /// a query that is used to retrieve the classifiers from the dictionary.
        /// In that way, classifiers can be filtered for interfaces, for a specific name etc...
        /// </summary>
        /// <param name="classifiers">dictionary where all classifiers are stored</param>
        /// <param name="notification">notification service that fires an update
        /// when the classifiers change</param>
        /// <param name="queryForAvailableClassifiers">
        /// query that is used to retrieve the classifiers from the classifier dictionary</param>
        /// <param name="messageSystem"></param>
        protected ClassifierSelectionItemsSource(
            ClassifierDictionary classifiers,
            Func<IEnumerable<Classifier>> queryForAvailableClassifiers,
            MessageSystem messageSystem)
        {
            _messageSystem = messageSystem;
            _classifiers = classifiers;
            foreach (var classifier in queryForAvailableClassifiers())
            {
                Add(new ClassifierItemViewModel(classifier.Name));
                // register for changes of this classifier
                messageSystem.Subscribe<DomainObjectDeletedEvent<Classifier>>(
                    classifier,OnClassiferDeleted);
                messageSystem.Subscribe<NameChangedEvent>(
                    classifier,OnNameChanged);
            }
            // register for creation event of a new classifier
            messageSystem.Subscribe<DomainObjectCreatedEvent<Classifier>>(
                classifiers, OnNewClassifierCreated);
        }

        /// <summary>
        /// event handler which is called when a classifier is removed
        /// </summary>
        /// <param name="classifierDeletedEvent"></param>
        private void OnClassiferDeleted(DomainObjectDeletedEvent<Classifier> classifierDeletedEvent)
        {
            var viewModel = ByName(classifierDeletedEvent.DomainObject.Name);
            Remove(viewModel);
        }

        private void OnNewClassifierCreated(DomainObjectCreatedEvent<Classifier> newClassifierEvent)
        {
            var newClassifier = newClassifierEvent.DomainObject;
            var newItem = new ClassifierItemViewModel(newClassifier.Name);
            var newIndex = FindNewItemPosition(newItem);
            Insert(newIndex, newItem);
        }

        /// <summary>
        /// method that handles the name changes of an item.
        /// Can be overridden by derived class (e.g. when the name is exlcuded from the list)
        /// </summary>
        protected virtual void OnNameChanged(NameChangedEvent nameChangedEvent)
        {
            var item = ByName(nameChangedEvent.OldName);
            // create a temporary item so that we get the new index
            var tmp = new ClassifierItemViewModel(nameChangedEvent.NewName);
            var oldIndex = IndexOf(item);
            // since all item names must be unique, the
            // new item can never be in the list
            // so the index we get is always the index where the item should be added
            var newIndex = FindNewItemPosition(tmp);
            // rename the item and move it to the new position
            item.Name = nameChangedEvent.NewName;
            if(oldIndex != newIndex)
               Move(oldIndex, newIndex);
        }

        public ClassifierSelectionItemsSource(
            ClassifierDictionary classifiers,
            MessageSystem messageSystem)
            :this(classifiers,() => classifiers.OrderBy(x => x.Name),messageSystem)
        {
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

        private int FindNewItemPosition(INamed item) => BinarySearch(item, 0, Count - 1);

        private int BinarySearch(INamed item, int min, int max)
        {
            while (min < max)
            {
                var mid = (min + max)/2;
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

        /// <summary>
        /// excludes the given item from the list and returns a new list
        /// without the excluded item.
        /// </summary>
        /// <param name="classifierName"></param>
        /// <param name="withNullItem">if true, the list will also contain a "null" entry</param>
        /// <returns></returns>
        public ClassifierSelectionItemsSource Exclude(string classifierName, bool withNullItem = true)
        {
            var newSource = new ClassifierSelectionSourceWithExcludedItem(
                _classifiers,
                _messageSystem,
                classifierName);
            if(withNullItem)
                newSource.Insert(0, ClassifierItemViewModel.None);
            return newSource;
        }
    }
}