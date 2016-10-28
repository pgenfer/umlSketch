using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using Common;
using static System.Diagnostics.Contracts.Contract;

namespace Yuml
{
    /// <summary>
    /// storage for classifiers. A classifier is always identified
    /// by its name, so the name must be unique.
    /// </summary>
    public class ClassifierDictionary : IEnumerable<Classifier>, IVisibleObjectList
    {
        private readonly Dictionary<string, Classifier> _dictionary = 
            new Dictionary<string, Classifier>();
        private readonly FindBestNameMixin _findBestName;

        /// <summary>
        /// returns true if the given class name can be used
        /// for a new class (a class with the same name does not exist
        /// already
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        [Pure]
        public virtual bool IsClassNameFree(string className) => !_dictionary.ContainsKey(className);

        /// <summary>
        /// searches a class with the given name.
        /// </summary>
        /// <param name="className"></param>
        /// <returns>The classifier or null if no classifier was found</returns>
        public virtual Classifier FindByName(string className)
        {
            Requires(!string.IsNullOrEmpty(className));
            Classifier result;
            _dictionary.TryGetValue(className, out result);
            return result;
        }

        /// <summary>
        /// returns all classifiers that use the given type as base type
        /// </summary>
        /// <param name="baseClass"></param>
        /// <returns></returns>
        public virtual IEnumerable<Classifier> FindAllDerivedClassifiers(Classifier baseClass) =>
            _dictionary.Values.Where(x => x.BaseClass == baseClass);

        [Pure]
        public int Count => _dictionary.Count;
        
        /// <summary>
        /// creates a new class with the given name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Classifier CreateNewClass(string name)
        {
            Requires(IsClassNameFree(name));
            Requires(!string.IsNullOrEmpty(name));
            Ensures(_dictionary.Count == OldValue(_dictionary.Count)+1);

            var newClass = new Classifier(name);
            AddNewClassifier(newClass);

            return newClass;
        }

        public Classifier CreateNewClassWithBestName() => CreateNewClass(FindBestName(Strings.NewClassifier));

        /// <summary>
        /// method is used by serializer to store newly
        /// created classifiers in the dictionary, should
        /// not be used from other external clients
        /// </summary>
        /// <param name="classifier"></param>
        internal void AddNewClassifier(Classifier classifier)
        {
            Requires(!string.IsNullOrEmpty(classifier.Name));
            Requires(IsClassNameFree(classifier.Name));

            // check if the classifier must also be added to system types
            _dictionary.Add(classifier.Name, classifier);
        }

        public IEnumerator<Classifier> GetEnumerator()  => _dictionary.Values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _dictionary.Values.GetEnumerator();

        /// <summary>
        /// creates an empty dictionary with classifiers
        /// </summary>
        public ClassifierDictionary(bool hasSystemTypes = true)
        {
            if (!hasSystemTypes)
                return;

            // make system types available
            foreach (var systemType in new SystemTypes())
                AddNewClassifier(systemType);

            _findBestName = new FindBestNameMixin(_dictionary.Values);
        }

        /// <summary>
        /// used to initialize a dictionary with the given list of classifiers.
        /// Used only in test cases and does not load system types
        /// </summary>
        /// <param name="classifiers"></param>
        internal ClassifierDictionary(params Classifier[] classifiers):this(false)
        {
            Requires(classifiers != null);

            foreach (var classifier in classifiers)
                AddNewClassifier(classifier);
        }

        /// <summary>
        /// also only for internal use (testing, serialization etc...)
        /// </summary>
        /// <param name="classifiers"></param>
        internal ClassifierDictionary(IEnumerable<Classifier> classifiers)
            :this(classifiers.ToArray())
        {           
        }

        public void RemoveClassifier(Classifier classifierToRemove)
        {
            Requires(classifierToRemove != null);
            Requires(!string.IsNullOrEmpty(classifierToRemove.Name));
            // the classifer should always be in the list,
            // it does not make much sense to have "phantom classifiers"
            Ensures(Count == OldValue(Count) - 1);

            _dictionary.Remove(classifierToRemove.Name);
        }

        public void WriteTo(DiagramWriter writer)
        {
            Requires(writer != null);

            var index = 0;
            var relations = new RelationList();
            foreach (var classifier in NoSystemTypes.Where(x => x.IsVisible))
            {
                var classWriter = writer.StartClass();
                classWriter = classifier.WriteTo(classWriter);
                classWriter.Finish(++index == _dictionary.Count);

                relations.AddRelations(classifier.FindAllRelationStartingFromClass());
            }
            relations.WriteTo(writer);
        }

        public virtual void RenameClassifier(Classifier classifier,string newName)
        {
            Requires(classifier != null);
            Requires(!string.IsNullOrEmpty(newName));
            Ensures(IsClassNameFree(OldValue(classifier.Name)));
            Ensures(classifier.Name != OldValue(classifier.Name));

            _dictionary.Remove(classifier.Name);
            classifier.Name = newName;
            _dictionary.Add(newName, classifier);
        }

        /// <summary>
        /// returns string system type
        /// </summary>
        internal virtual Classifier String => FindByName(Strings.String);
        internal virtual Classifier Void => FindByName(Strings.Void);

        /// <summary>
        /// adds all system types to the dictionary which were not already added before
        /// // (used during serialization when reading classifiers from a storage)
        /// </summary>
        internal virtual void AddMissingSystemTypes()
        {
            foreach (var systemType in new SystemTypes())
                if(IsClassNameFree(systemType.Name))
                   AddNewClassifier(systemType);
        }

        internal IEnumerable<Classifier> NoSystemTypes => this.Where(x => !x.IsSystemType);

        public bool IsVisible
        {
            get { return this.All(x => x.IsVisible); }
            set{foreach (var classifier in this) classifier.IsVisible = value;}
        }

        public IEnumerable<IVisible> VisibleObjects => this;
        private string FindBestName(string defaultName) => _findBestName.FindBestName(defaultName);

        /// <summary>
        /// removes all classifiers from this dictionary
        /// </summary>
        public void Clear()
        {
            if(_dictionary.Count > 0)
               _dictionary.Clear();
        }
    }
}
