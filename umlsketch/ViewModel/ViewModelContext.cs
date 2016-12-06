using System;
using System.Diagnostics.Contracts;
using Caliburn.Micro;
using UmlSketch.Command;
using UmlSketch.DomainObject;
using UmlSketch.Event;

namespace UmlSketch.Editor
{
    /// <summary>
    /// class that supports every view model with its context.
    /// The context contains information that can be shared by all view models.
    /// </summary>
    public class ViewModelContext
    {
        private readonly Diagram _diagram;

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
        public ViewModelFactory ViewModelFactory { get; }
        /// <summary>
        /// factory used to create domain commands
        /// </summary>
        public CommandFactory CommandFactory { get; }
        /// <summary>
        /// can be used by view models to open
        /// dialogs.
        /// </summary>
        public IWindowManager WindowManager { get; }

        /// <summary>
        /// stores the editname mixin that is currently in edit mode
        /// (if any)
        /// </summary>
        public IEditableName CurrentEdit { get; set; }

        public DiagramColorPalette ColorPalette => _diagram.ColorPalette;

        /// <summary>
        /// constructor should only be used for testing
        /// </summary>
        public ViewModelContext()
        {
            /* only for testing */
        }
        
        public ViewModelContext(
            Diagram diagram,
            ClassifierDictionary availableClassifiers,
            CommandFactory commandFactory,
            MessageSystem messageSystem,
            IWindowManager windowManager)
        {
            _diagram = diagram;
            Contract.Requires(availableClassifiers != null);
            Contract.Requires(messageSystem != null);

            Classifiers = availableClassifiers;
            MessageSystem = messageSystem;
            WindowManager = windowManager;
            ViewModelFactory = new ViewModelFactory(this);
            CommandFactory = commandFactory;
        }

        public IClassifierSelectionItemsSource CreateClassifierItemSource(Predicate<Classifier> filter,bool addNullItem=false) =>
            new ClassifierSelectionItemsSource(Classifiers, filter, MessageSystem,addNullItem);

    }
}
