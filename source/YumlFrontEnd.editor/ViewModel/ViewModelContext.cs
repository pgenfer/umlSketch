using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml;
using static System.Diagnostics.Contracts.Contract;

namespace YumlFrontEnd.editor
{
    /// <summary>
    /// class that supports every view model with its context.
    /// The context contains information that can be shared by all view models.
    /// </summary>
    public class ViewModelContext
    {
        /// <summary>
        /// all classifiers that are available in the system
        /// </summary>
        public ClassifierDictionary Classifiers { get; }
        /// <summary>
        /// message system for communicating domain events
        /// </summary>
        public MessageSystem MessageSystem { get; }

        /// <summary>
        /// factory can be used to create other view models
        /// </summary>
        public ViewModelFactory Factory { get; }

        public ViewModelContext(
            ClassifierDictionary availableClassifiers,
            MessageSystem messageSystem)
        {
            Requires(availableClassifiers != null);
            Requires(messageSystem != null);

            Classifiers = availableClassifiers;
            MessageSystem = messageSystem;
            Factory = new ViewModelFactory(this);
        }

        public IClassifierSelectionItemsSource CreateClassifierItemSource(Predicate<Classifier> filter,bool addNullItem=false) =>
            new ClassifierSelectionItemsSource(Classifiers, filter, MessageSystem,addNullItem);

    }
}
