using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Common;
using static System.Diagnostics.Contracts.Contract;

namespace Yuml
{
    public class MethodList : BaseList<Method>, IVisible
    {
        /// <summary>
        /// adds a new property to the property list.
        /// Currently there is no restriction about duplicate properties
        /// </summary>
        /// <param name="name">name of property</param>
        /// <param name="type">classifier of the property</param>
        /// <param name="isVisible"></param>
        /// <returns>the newly added property</returns>
        public Method CreateMethod(string name, Classifier type, bool isVisible=true)
        {
            Requires(!string.IsNullOrEmpty(name));
            Requires(type != null);
            Ensures(_list.Count == OldValue(_list.Count) + 1);

            return AddNewMember(new Method(name, type,isVisible));
        }

        public void WriteTo(ClassWriter classWriter)
        {
            Requires(classWriter != null);

            foreach (var method in this.Where(x => x.IsVisible))
            {
                var methodWriter = classWriter.WithNewMethod();
                methodWriter = method.WriteTo(methodWriter);
                classWriter = methodWriter.Finish();
            }
        }

        public SubSet FindMethodsThatDependOnClassifier(Classifier classifier) => Filter(x => x.ReturnType == classifier);

        public bool IsVisible
        {
            get { return _list.All(x => x.IsVisible); }
            set { _list.ForEach(x => x.IsVisible = value);}
        }
    }
}