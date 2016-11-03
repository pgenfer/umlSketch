using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Diagnostics.Contracts.Contract;

namespace Yuml
{
    /// <summary>
    /// list where all interfaces are stored.
    /// </summary>
    public class InterfaceList : BaseList<Classifier>
    {
        /// <summary>
        /// adds a new entry to the interface list.
        /// The interface that will be added will be chosen by the method,
        /// the user can change it after the new entry was created.
        /// </summary>
        /// <param name="self">object itself that owns the interface list"></param>
        /// <param name="classifiers"></param>
        public Classifier AddNewInterfaceEntryToList(Classifier self, ClassifierDictionary classifiers)
        {
            // all interfaces except
            // 1. the ones which are already in the interface list
            // 2. the owner of the interfaces itself
            var availableInterfaces = classifiers
                .Where(x => x.IsInterface && x != self)
                .Except(_list);
            var firstInterface = availableInterfaces.FirstOrDefault();
            // TODO: throw error if there is no interface anymore that we can add?
            if(firstInterface != null)
                AddExistingMember(firstInterface);
            return firstInterface;
        }

        public void ReplaceInterface(Classifier oldInterface, Classifier newInterface)
        {
            RemoveInterfaceFromList(oldInterface);
            AddInterfaceToList(newInterface);
        }

        public void RemoveInterfaceFromList(Classifier interfaceToRemove)
        {
            Requires(interfaceToRemove != null);
            Requires(interfaceToRemove.IsInterface);
            Requires(this.Contains(interfaceToRemove));

            _list.Remove(interfaceToRemove);
        }

        public void AddInterfaceToList(Classifier interfaceToAdd)
        {
            Requires(interfaceToAdd != null);
            Requires(interfaceToAdd.IsInterface);
            Requires(!this.Contains(interfaceToAdd));

            AddExistingMember(interfaceToAdd);
        }
    }
}
