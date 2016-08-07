using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using static System.Diagnostics.Contracts.Contract;

namespace Yuml
{
    public class MethodList : BaseList<Method>
    {
        /// <summary>
        /// adds a new property to the property list.
        /// Currently there is no restriction about duplicate properties
        /// </summary>
        /// <param name="name">name of property</param>
        /// <param name="type">classifier of the property</param>
        /// <returns>the newly added property</returns>
        public Method CreateMethod(string name, Classifier type)
        {
            Requires(!string.IsNullOrEmpty(name));
            Requires(type != null);
            Ensures(_list.Count == OldValue(_list.Count) + 1);

            return AddNewMember(new Method(name, type));
        }
    }
}