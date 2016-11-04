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
    /// Methods here are public because unit testing, production code
    /// should access class only via interface to know which methods are available
    /// </summary>
    public class ClassifierSelectionItemsSource :
        BindableCollection<ClassifierItemViewModel>,
        IClassifierSelectionItemsSource
    {
        private readonly MessageSystem _messageSystem;
        private readonly Func<IEnumerable<Classifier>> _queryForAvailableClassifiers;

        /// <summary>
        /// only for test cases, should not be used in production code
        /// </summary>
        public ClassifierSelectionItemsSource(){ }

        protected ClassifierSelectionItemsSource(
            ClassifierDictionary classifiers,
            Func<IEnumerable<Classifier>> queryForAvailableClassifiers,
            MessageSystem messageSystem,
            bool addNullItem = false)
        {
            // apply the filter on the list of classifiers and sort them by name
            // before retrieving
            _queryForAvailableClassifiers = queryForAvailableClassifiers;
            _messageSystem = messageSystem;

            UpdateClassifierList();

            // register events fired by the whole classifier list
            messageSystem.Subscribe<DomainObjectCreatedEvent<Classifier>>(
                classifiers, OnNewClassifierCreated);
            messageSystem.Subscribe<ClassifiersResetEvent>(classifiers, OnClassifiersReset);

            if (addNullItem)
                Insert(0, ClassifierItemViewModel.None);
        }

        public ClassifierSelectionItemsSource(
           ClassifierDictionary classifiers,
           Predicate<Classifier> filter,
           MessageSystem messageSystem,
           bool addNullItem = false):
            this(classifiers, () => classifiers.Where(x => filter(x)),messageSystem,addNullItem)
        {
        }

        /// <summary>
        /// should be called whenever the complete list of classifier should be regenerated.
        /// Can happen if a new diagram is loaded from persistant storage or during initialization.
        /// </summary>
        private void UpdateClassifierList()
        {
            foreach (var classifier in _queryForAvailableClassifiers().OrderBy(x => x.Name))
            {
                Add(new ClassifierItemViewModel(classifier.Name));
                // register for changes of this classifier
                _messageSystem.Subscribe<DomainObjectDeletedEvent<Classifier>>(
                    classifier, OnClassiferDeleted);
                _messageSystem.Subscribe<NameChangedEvent>(
                    classifier, OnNameChanged);
            }
        }

        /// <summary>
        /// event handler which is called when a classifier is removed
        /// </summary>
        /// <param name="classifierDeletedEvent"></param>
        private void OnClassiferDeleted(DomainObjectDeletedEvent<Classifier> classifierDeletedEvent)
        {
            var viewModel = ByName(classifierDeletedEvent.DomainObject.Name);
            if (viewModel != null)
                Remove(viewModel);
        }

        private void OnClassifiersReset(ClassifiersResetEvent classifiersResetEvent)
        {
            Clear();
            UpdateClassifierList();
        }

        public void OnNewClassifierCreated(DomainObjectCreatedEvent<Classifier> newClassifierEvent)
        {
            var newClassifier = newClassifierEvent.DomainObject;
            var newItem = new ClassifierItemViewModel(newClassifier.Name);
            var newIndex = FindNewItemPosition(newItem);
            if (newIndex == -1) // list was empty before
                Add(newItem);
            else
                Insert(newIndex, newItem);
        }

        /// <summary>
        /// method that handles the name changes of an item.
        /// Can be overridden by derived class (e.g. when the name is exlcuded from the list)
        /// </summary>
        public virtual void OnNameChanged(NameChangedEvent nameChangedEvent)
        {
            var item = ByName(nameChangedEvent.OldName);
            // check that classifier was not filtered out from list
            if (item == null)
                return;
            // create a temporary item so that we get the new index
            var tmp = new ClassifierItemViewModel(nameChangedEvent.NewName);
            var oldIndex = IndexOf(item);
            // since all item names must be unique, the
            // new item can never be in the list
            // so the index we get is always the index where the item should be added
            var newIndex = FindNewItemPosition(tmp);
            // rename the item and move it to the new position
            item.Name = nameChangedEvent.NewName;
            if (oldIndex != newIndex)
                Move(oldIndex, newIndex);
        }

        /// <summary>
        /// returns the classifier with the given name.
        /// </summary>
        /// <param name="name">name of the classifier. It can be
        /// that the classifier is not in the list (in case it was excluded from the list before)</param>
        /// <returns>ViewModel of the classifier or null if the given classifier does not exist in this list.</returns>
        public ClassifierItemViewModel ByName(string name)
        {
           return this.FirstOrDefault(x => x.Name == name);
        }

        private int FindNewItemPosition(INamed item) => BinarySearch(item, 0, Count - 1);

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
            return max > -1 ? max : 0;
        }
    }
}