using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Common;
using UmlSketch.Mixin;
using UmlSketch.Settings;

namespace UmlSketch.DomainObject
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
        [Pure]
        public virtual Classifier FindByName(string className)
        {
            Contract.Requires(!string.IsNullOrEmpty(className));
            // check that we never search for non existing classifiers
            Contract.Ensures(Contract.Result<Classifier>() != null);

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
            _dictionary.Values.Where(x => x.BaseClass == baseClass).ToList();

        /// <summary>
        /// returns all classifiers that implement the given interface
        /// </summary>
        /// <param name="interface"></param>
        /// <returns></returns>
        public virtual IEnumerable<Classifier> FindAllImplementers(Classifier @interface) =>
            _dictionary.Values.Where(x => x.FindImplementationsOfInterface(@interface).IsNotEmpty());

        [Pure]
        public int Count => _dictionary.Count;
        
        /// <summary>
        /// creates a new class with the given name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Classifier CreateNewClass(string name)
        {
            Contract.Requires(IsClassNameFree(name));
            Contract.Requires(!string.IsNullOrEmpty(name));
            Contract.Ensures(_dictionary.Count == Contract.OldValue(_dictionary.Count)+1);

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
            Contract.Requires(!string.IsNullOrEmpty(classifier.Name));
            Contract.Requires(IsClassNameFree(classifier.Name));

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
            Contract.Requires(classifiers != null);

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
            Contract.Requires(classifierToRemove != null);
            Contract.Requires(!string.IsNullOrEmpty(classifierToRemove.Name));
            // the classifer should always be in the list,
            // it does not make much sense to have "phantom classifiers"
            Contract.Ensures(Count == Contract.OldValue(Count) - 1);

            _dictionary.Remove(classifierToRemove.Name);
        }

        public void WriteTo(DiagramWriter.DiagramWriter writer,DiagramDirection direction)
        {
            Contract.Requires(writer != null);

            var relations = new RelationList();
            var classifiers = NoSystemTypes.Where(x => x.IsVisible).ToArray();
            for (var i = 0; i < classifiers.Length; i++)
            {
                var classifier = classifiers[i];
                var classWriter = writer.StartClass();
                classifier.WriteTo(classWriter);
                writer = classWriter.Finish();
                var lastClassifier = i == classifiers.Length - 1;
                var addSeparator = !lastClassifier || classifier.Note.HasText;
                if (addSeparator)
                    writer.AddSeparator();
                if (classifier.Note.HasText)
                {
                    writer = writer.WithClassifierNote(classifier.Name, classifier.Note);
                    if (!lastClassifier) // add separator also after note if there are still more classifiers
                        writer.AddSeparator();
                }
                
                // store relations for later writing
                relations.AddRelations(classifier.FindAllRelationStartingFromClass());
               
            }
           
            relations.WriteTo(writer,direction);
        }

        public virtual void RenameClassifier(Classifier classifier,string newName)
        {
            Contract.Requires(classifier != null);
            Contract.Requires(!string.IsNullOrEmpty(newName));
            Contract.Ensures(IsClassNameFree(Contract.OldValue(classifier.Name)));
            Contract.Ensures(classifier.Name != Contract.OldValue(classifier.Name));

            _dictionary.Remove(classifier.Name);
            classifier.Name = newName;
            _dictionary.Add(newName, classifier);
        }

        /// <summary>
        /// returns string system type
        /// </summary>
        internal virtual Classifier String => FindByName(SystemTypes.String);
        internal virtual Classifier Void => FindByName(SystemTypes.Void);

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
