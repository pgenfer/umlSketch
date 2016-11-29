using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using Common;
using UmlSketch.DomainObject;
using UmlSketch.Event;

namespace UmlSketch.Editor
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
        /// This is a template method that can be extended by derived classes to control which items
        /// should be visible in the list and on which events the itemsource should react.
        /// </summary>
        private void UpdateClassifierList()
        {
            foreach (var classifier in _queryForAvailableClassifiers().OrderBy(x => x.Name))
            {
                RegisterForClassifierEvent(_messageSystem, classifier);
                
                // derived class can control whether this item should be visible
                if (!ShouldItemBeVisible(classifier))
                    continue;

                Add(new ClassifierItemViewModel(classifier.Name));
                // register for changes of this classifier
                _messageSystem.Subscribe<DomainObjectDeletedEvent<Classifier>>(
                    classifier, OnClassiferDeleted);
                _messageSystem.Subscribe<NameChangedEvent>(
                    classifier, OnNameChanged);
            }
        }

        /// <summary>
        /// derived classes can register additional message handling for a classifier here
        /// </summary>
        /// <param name="messageSystem"></param>
        /// <param name="classifier"></param>
        protected virtual void RegisterForClassifierEvent(MessageSystem messageSystem, Classifier classifier)
        {
            // base implementation does nothing
        }

        /// <summary>
        /// additional filter method that can be implemented by
        /// derived classes, can be used to narrow the list of elements that should be visible in
        /// the class list.
        /// Base class items source needs this because the condition whether a class is an interface or not 
        /// can change so the itemssource also has to react on items that were not in the list before
        /// </summary>
        /// <param name="classifier">The classifier for which an additional
        /// check should be done.</param>
        /// <returns></returns>
        protected virtual bool ShouldItemBeVisible(Classifier classifier) => true;

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
            // check if item must be filtered out
            if (!ShouldItemBeVisible(newClassifier))
                return;

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
        public virtual ClassifierItemViewModel ByName(string name) => this.FirstOrDefault(x => x.Name == name);

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