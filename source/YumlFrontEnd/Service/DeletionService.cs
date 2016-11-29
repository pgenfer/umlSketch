using System.Linq;
using UmlSketch.DomainObject;
using UmlSketch.Event;

namespace UmlSketch.Service
{
    /// <summary>
    /// deleting a domain object is a complex task
    /// as it can affect many other domain objects,
    /// that's why this task is separated out into a single service.
    /// This service is able to handle all delete operations on any entities.
    /// </summary>
    public class DeletionService
    {
        private readonly ClassifierDictionary _classifiers;
        private readonly MessageSystem _messageSystem;
    
        public DeletionService(
            ClassifierDictionary classifiers,
            MessageSystem messageSystem)
        {
            _classifiers = classifiers;
            _messageSystem = messageSystem;
        }

        public void DeleteClassifier(Classifier classifier)
        {
            var derivedClasses = _classifiers.FindAllDerivedClassifiers(classifier);

            var dependentProperties =
                _classifiers.Select(x => x.Properties.FindPropertiesThatDependOnClassifier(classifier));
            var dependentMethods =
                _classifiers.Select(x => x.Methods.FindMethodsThatDependOnClassifier(classifier));
            var dependentAssociations =
                _classifiers.Select(x => x.Associations.FindAssociationsThatDependOnClassifier(classifier));
            var dependentImplementations =
                _classifiers.Select(x => x.InterfaceImplementations.FindImplementationsOfInterface(classifier));
            var dependentParameters = 
                _classifiers.SelectMany(x => x.Methods)
                    .Select(m => m.Parameters.FindMethodsThatDependOnClassifier(classifier));

            foreach (var properties in dependentProperties)
                properties.DeleteSelection(_messageSystem);
            foreach (var methods in dependentMethods)
                methods.DeleteSelection(_messageSystem);
            foreach(var associations in dependentAssociations)
                associations.DeleteSelection(_messageSystem);
            foreach (var implementation in dependentImplementations)
                implementation.DeleteSelection(_messageSystem);
            foreach (var parameter in dependentParameters)
                parameter.DeleteSelection(_messageSystem);

            // remove the deleted class as base class
            foreach (var derivedClass in derivedClasses)
                derivedClass.ClearBaseClass(_messageSystem);

            _classifiers.RemoveClassifier(classifier);
            _messageSystem.PublishDeleted(_classifiers,classifier);
        }
    }
}
