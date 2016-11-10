using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml.DomainObject;
using static System.Diagnostics.Contracts.Contract;

namespace Yuml
{
    /// <summary>
    /// Generally, you can think of a classifier as a class, 
    /// but technically a classifier is a more general term that 
    /// refers to the other three types above as well.
    /// See documentation here:
    /// https://www.ibm.com/developerworks/rational/library/content/RationalEdge/sep04/bell/
    /// </summary>
    public class Classifier : IVisible, INamed
    {
        private readonly NameMixin _name = new NameMixin();
        private readonly IVisible _visible = new VisibleMixin();
        private Classifier _baseClass;
        private ImplementationList _interfaceImplementations = new ImplementationList();
        private ClassifierAssociationList _associations = new ClassifierAssociationList();

        /// <summary>
        /// color is stored in hex format as AARRGGBB.
        /// The classifier stores the color only for serializing / deserializing
        /// and is not used in the domain object.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Specifies whether this classifier is a system type.
        /// A system type can not be edited in the classifier view
        /// but can be assigned to properties and method return values.
        /// </summary>
        public bool IsSystemType { get; internal set; }

        /// <summary>
        /// only used for test stubs, do not delete and do not use in production code
        /// </summary>
        public Classifier() { }

        public Classifier(string name, bool isSystemType=false)
        {
            Name = name;
            IsVisible = true;
            IsSystemType = isSystemType;
        }
       
        public bool IsVisible
        {
            get { return _visible.IsVisible; }

            set { _visible.IsVisible = value; }
        }
        public string Name
        {
            get { return _name.Name; }
            set { _name.Name = value; }
        }

        /// <summary>
        /// internal setter is only used by serialization, should not be used by other production code
        /// </summary>
        public PropertyList Properties { get; internal set; } = new PropertyList();
        /// <summary>
        /// internal setter only used by serialization, should not be used by other production code
        /// </summary>
        public MethodList Methods { get; internal set; } = new MethodList();

        public ImplementationList InterfaceImplementations
        {
            get { return _interfaceImplementations; }
            internal set { _interfaceImplementations = value; }
        }

        public override string ToString() => _name.ToString();

        /// <summary>
        /// true if the class is an interface, otherwise false
        /// </summary>
        public bool IsInterface { get; set; }

        public Property CreateProperty(string name, Classifier type,bool isVisible = true) => 
            Properties.CreateProperty(name, type,isVisible);
        public IEnumerator<Property> GetEnumerator() => Properties.GetEnumerator();

        public ClassWriter WriteTo(ClassWriter classWriter)
        {
            Requires(classWriter != null);

            classWriter.WithName(Name);
            Properties.WriteTo(classWriter);
            Methods.WriteTo(classWriter);
            classWriter.WithColor(Color);
            return classWriter;
        }

        public Method CreateMethod(string name, Classifier type,bool isVisible = true) => 
            Methods.CreateMethod(name, type,isVisible);
        public Property CreateNewPropertyWithBestInitialValues(ClassifierDictionary systemClassifiers) => 
            Properties.CreateNewPropertyWithBestInitialValues(systemClassifiers);

        /// <summary>
        /// optional base class of this classifier
        /// </summary>
        public Classifier BaseClass
        {
            get { return _baseClass; }
            set
            {
                // if a base class is set, it must not be an interface
                Requires(value == null || !value.IsInterface);
                _baseClass = value; 
            }
        }

        public Relation AddNewRelation(
            Classifier target,
            RelationType associationType,
            string startName="", 
            string endName="") => 
            Associations.AddNewRelation(target, associationType, startName, endName);

        /// <summary>
        /// property must have setter, otherwise we cannot set it via AutoMapper
        /// </summary>
        public ClassifierAssociationList Associations
        {
            get { return _associations; }
            internal set
            {
                if (value == null)
                    return;
                //Requires(value != null);

                _associations = value;
                _associations.Root = this;
            }
        }


        public Relation CreateNewAssociationWithBestInitialValues(ClassifierDictionary classifiers) => 
            Associations.CreateNewAssociationWithBestInitialValues(classifiers);

        public Method CreateNewMethodWithBestInitialValues(ClassifierDictionary classifiers) =>
            Methods.CreateNewMethodWithBestInitialValues(classifiers);

        /// <summary>
        /// collects all relations from this classifier to adjacent classifiers.
        /// These can either be associations or implementations/derivation
        /// </summary>
        /// <returns>
        /// list of relations to other classifiers 
        /// starting from this classifier.
        /// Empty list if no relations are available for this type.
        /// </returns>
        public RelationList FindAllRelationStartingFromClass()
        {
            var relationList = new RelationList(Associations);
            if (BaseClass != null)
                relationList.AddRelation(new Relation(this, BaseClass, RelationType.Inheritance));
            relationList.AddRelations(InterfaceImplementations);
            return relationList;
        }

        /// <summary>
        /// note that is attached to this classifier
        /// </summary>
        public Note Note { get; set; } = new Note();

        /// <summary>
        /// clears the base class of this class and fires
        /// the required event
        /// </summary>
        /// <param name="messageSystem">optional, if set, a domain event
        /// will be fired that the base class was removed.</param>
        public void ClearBaseClass(MessageSystem messageSystem = null)
        {
            BaseClass = null;
            messageSystem?.Publish(this,new ClearBaseClassEvent());
        }

        public void SetBaseClass(Classifier @interface, MessageSystem messageSystem)
        {
            var oldBaseClass = BaseClass;
            BaseClass = @interface;
            if (oldBaseClass == null)
                messageSystem.Publish(this, new BaseClassSetEvent(@interface.Name));
            else
                messageSystem.Publish(this, new BaseClassChangedEvent(oldBaseClass.Name,@interface.Name));
        }

        public void AddInterfaceImplementation(Classifier newInterface,MessageSystem messageSystem = null)
        {
            var newImplementation = new Implementation(this, newInterface);
            InterfaceImplementations.AddInterfaceToList(new Implementation(this, newInterface));
            messageSystem?.PublishCreated(InterfaceImplementations, newImplementation);
        }

        public IEnumerable<Classifier> ImplementedInterfaces => 
            _interfaceImplementations.ImplementedInterfaces;
        public void ReplaceInterface(Implementation implementation, Classifier newInterface) => 
            _interfaceImplementations.ReplaceInterface(implementation, newInterface);
        public void RemoveInterfaceFromList(Implementation implementation) => 
            _interfaceImplementations.RemoveInterfaceFromList(implementation);
        public virtual BaseList<Implementation>.SubSet FindImplementationsOfInterface(Classifier @interface) => 
            _interfaceImplementations.FindImplementationsOfInterface(@interface);

        public void RemoveImplementationForInterface(Classifier @interface,MessageSystem messageSystem = null) =>
            _interfaceImplementations.RemoveImplementationForInterface(@interface,messageSystem);
    }
}
