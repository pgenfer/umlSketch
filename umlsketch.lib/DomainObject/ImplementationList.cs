using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using UmlSketch.Event;

namespace UmlSketch.DomainObject
{
    /// <summary>
    /// list of interfaces that are implemented by a classifier
    /// </summary>
    public class ImplementationList : BaseList<Implementation>
    {
        public virtual Classifier Root { get; set; }

        public int Count => _list.Count;

        /// <summary>
        /// returns all interfaces that are implemented.
        /// </summary>
        public IEnumerable<Classifier> ImplementedInterfaces => _list.Select(x => x.End.Classifier);


        /// <summary>
        /// adds a new implementation entry to the list. This entry will be a prototype
        /// where the end node can be changed by the user later.
        /// </summary>
        /// <param name="classifiers">classifier list used to obtain available interfaces</param>
        /// <returns></returns>
        public override Implementation CreateNew(ClassifierDictionary classifiers)
        {
            Implementation newImplementation = null;
            // all interfaces except
            // 1. the ones which are already in the interface list
            // 2. the owner of the interfaces itself
            var availableInterfaces = classifiers
                .Where(x => x.IsInterface && x != Root)
                .Except(ImplementedInterfaces);
            var firstInterface = availableInterfaces.FirstOrDefault();
            // TODO: throw error if there is no interface anymore that we can add?
            if (firstInterface != null)
            {
                newImplementation = new Implementation(Root, firstInterface);
                AddExistingMember(newImplementation);
            }
            return newImplementation;
        }

        public void ReplaceInterface(Implementation implementation, Classifier newInterface)
        {
            implementation.End.Classifier = newInterface;
        }

        public void AddInterfaceToList(Implementation implementation)
        {
            Contract.Requires(implementation != null);
            Contract.Requires(!this.Contains(implementation));

            AddExistingMember(implementation);
        }

        public SubSet FindImplementationsOfInterface(Classifier @interface)
            => Filter(x => x.End.Classifier == @interface);

        /// <summary>
        /// deletes the implementation for the given interface from the implementation list
        /// and fires a notification that the implementation was removed.
        /// </summary>
        /// <param name="interface"></param>
        /// <param name="messageSystem"></param>
        internal void RemoveImplementationForInterface(
            Classifier @interface, 
            MessageSystem messageSystem = null)
        {
            Contract.Requires(@interface != null);

            var implementations = Filter(x => x.End.Classifier == @interface);
            implementations.DeleteSelection(messageSystem);
        }
    }
}
