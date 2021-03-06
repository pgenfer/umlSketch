﻿using UmlSketch.DomainObject;
using UmlSketch.Event;
using static System.Diagnostics.Contracts.Contract;

namespace UmlSketch.Command
{
    /// <summary>
    /// creates a new classifier with a default name.
    /// </summary>
    public class NewClassifierCommand : INewCommand
    {
        private readonly ClassifierDictionary _classifiers;
        private readonly MessageSystem _messageSystem;

        public NewClassifierCommand(
            ClassifierDictionary classifiers,
            MessageSystem messageSystem)
        {
            Requires(classifiers != null);
            Requires(messageSystem != null);

            _classifiers = classifiers;
            _messageSystem = messageSystem;
        }

        /// <summary>
        /// creates the new classifier and fires a create event.
        /// </summary>
        public void CreateNew()
        {
            var newClassifier = _classifiers.CreateNewClassWithBestName();
            _messageSystem.PublishCreated(_classifiers, newClassifier);
        }
    }
}
