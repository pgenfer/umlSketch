using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using static System.Diagnostics.Contracts.Contract;

namespace Yuml
{
    /// <summary>
    /// base class for list structures that 
    /// handle classifier members or parameters
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseList<T> : IEnumerable<T>, IVisibleObjectList where T: INamed, IVisible
    {
        protected readonly List<T> _list = new List<T>();

        internal void AddExistingMember(T newMember)
        {
            Requires(newMember != null);

            _list.Add(newMember);
        }

        protected T AddNewMember(T newMember)
        {
            Requires(newMember != null);

            _list.Add(newMember);
            return newMember;
        }

        public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _list.GetEnumerator();

        protected SubSet Filter(Func<T,bool> filter ) => new SubSet(this,_list.Where(filter));

        /// <summary>
        /// represents a subset of an existing property list.
        /// The subset contains selected elements of the original list
        /// </summary>
        public class SubSet
        {
            private readonly BaseList<T> _parent;
            private readonly IEnumerable<T> _chosenSelection;

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
                    messageSystem?.PublisDeleted(selected);
                }
            }
        }


        public bool IsVisible
        {
            get { return VisibleObjects.All(x => x.IsVisible); }
            set{foreach (var visibleObject in VisibleObjects) visibleObject.IsVisible = value;}
        }

        public IEnumerable<IVisible> VisibleObjects => _list.Cast<IVisible>();
    }
}
