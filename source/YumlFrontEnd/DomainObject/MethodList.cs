using System.Diagnostics.Contracts;
using System.Linq;
using UmlSketch.DiagramWriter;

namespace UmlSketch.DomainObject
{
    public class MethodList : NamedBaseList<Method>
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
            Contract.Requires(!string.IsNullOrEmpty(name));
            Contract.Requires(type != null);
            Contract.Ensures(_list.Count == Contract.OldValue(_list.Count) + 1);

            return AddNewMember(new Method(name, type,isVisible));
        }

        public void WriteTo(ClassWriter classWriter)
        {
            Contract.Requires(classWriter != null);

            foreach (var method in this.Where(x => x.IsVisible && x.ReturnType.IsVisible))
            {
                var methodWriter = classWriter.WithNewMethod();
                methodWriter = method.WriteTo(methodWriter);
                classWriter = methodWriter.Finish();
            }
        }

        public SubSet FindMethodsThatDependOnClassifier(Classifier classifier) => Filter(x => x.ReturnType == classifier);

        /// <summary>
        /// checks if this method list already contains a method with the given name
        /// and the parameter list.
        /// </summary>
        /// <param name="newMethodName">name the method would have</param>
        /// <param name="parameters">list of parameters the method would have</param>
        /// <returns>true if there is already a method with the same name and signature</returns>
        public bool ContainsMethodWithSignature(string newMethodName, ParameterList parameters)
        {
            var methodsWithSameName = this.Where(x => x.Name == newMethodName).ToArray();
            // if there is no method with the same, we do not have to check for parameters
            return 
                methodsWithSameName.Length != 0 && 
                methodsWithSameName.Any(method => method.Parameters.IsSame(parameters));
        }

        /// <summary>
        /// creates a property with a useful name (e.g. New Property 1, New Property 2 etc...)
        /// and a useful data type (e.g. the data type that was not used before)
        /// </summary>
        /// <returns></returns>
        public override Method CreateNew(ClassifierDictionary systemClassifiers)
        {
            var bestDefaultName = FindBestName(Strings.NewMethod);
            var newMethod = CreateMethod(bestDefaultName, systemClassifiers.Void);

            return newMethod;
        }
    }
}