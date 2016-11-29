using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Common;
using UmlSketch.Event;

namespace UmlSketch.DomainObject
{
    /// <summary>
    /// base class for list structures that 
    /// handle classifier members or parameters
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseList<T> : IEnumerable<T>, IVisibleObjectList where T: class, IVisible
    {
        protected readonly List<T> _list = new List<T>();
   
        internal void AddExistingMember(T newMember)
        {
            Contract.Requires(newMember != null);

            _list.Add(newMember);
        }

        protected T AddNewMember(T newMember)
        {
            Contract.Requires(newMember != null);

            _list.Add(newMember);
            return newMember;
        }

        public abstract T CreateNew(ClassifierDictionary classifiers);

        /// <summary>
        /// deletes the given member and fires a notification
        /// if a message system is provided.
        /// </summary>
        /// <param name="member">member that should be deleted</param>
        /// <param name="messageSystem">optional message system that is used to publish a domain event
        /// after the member was deleted.</param>
        public void DeleteMember(T member, MessageSystem messageSystem = null)
        {
            var memberToDelete = Filter(x => x == member);
            memberToDelete.DeleteSelection(messageSystem);
        }

        public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _list.GetEnumerator();

        public SubSet Filter(Func<T,bool> filter ) => new SubSet(this,_list.Where(filter));

        /// <summary>
        /// represents a subset of an existing property list.
        /// The subset contains selected elements of the original list
        /// </summary>
        public class SubSet
        {
            private readonly BaseList<T> _parent;
            private readonly IEnumerable<T> _chosenSelection;

            internal static SubSet Empty { get; } = new SubSet(null,Enumerable.Empty<T>());

            /// <summary>
            /// should only be used for testing purposes
            /// </summary>
            public SubSet()
            {
                _chosenSelection = Enumerable.Empty<T>();
            }

            internal SubSet(BaseList<T> parent, IEnumerable<T> chosenSelection)
            {
                _parent = parent;
                // keep a copy of the selection because
                // the original list will be changed during deletion
                _chosenSelection = new List<T>(chosenSelection);
            }

            /// <summary>
            /// removes all items in this subset from their parent list.
            /// </summary>
            /// <param name="messageSystem">If provided, an event will 
            /// be fired for every removed item</param>
            public void DeleteSelection(MessageSystem messageSystem = null)
            {
                foreach (var selected in _chosenSelection)
                {
                    _parent._list.Remove(selected);
                    messageSystem?.PublishDeleted(_parent,selected);
                }
            }

            public virtual bool IsNotEmpty() => _chosenSelection.Any();
        }


        public bool IsVisible
        {
            // if the list is empty or at least one item is visible
            // mark the list as visible
            get { return !VisibleObjects.Any() ||  VisibleObjects.Any(x => x.IsVisible); }
            set{foreach (var visibleObject in VisibleObjects) visibleObject.IsVisible = value;}
        }

        public IEnumerable<IVisible> VisibleObjects => _list.Cast<IVisible>();
       
    }
}
