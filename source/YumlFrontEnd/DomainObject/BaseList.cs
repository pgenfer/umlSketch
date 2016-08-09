using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Diagnostics.Contracts.Contract;

namespace Yuml
{
    /// <summary>
    /// base class for list structures that 
    /// handle classifier members or parameters
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseList<T> : IEnumerable<T>
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
    }
}
