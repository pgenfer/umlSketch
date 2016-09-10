using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Service
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
            // TODO: handle parameters here
            // TODO: handle relations here

            foreach (var properties in dependentProperties)
                properties.DeleteSelection(_messageSystem);
            foreach (var methods in dependentMethods)
                methods.DeleteSelection(_messageSystem);
            
            foreach (var derivedClass in derivedClasses)
            {
                derivedClass.BaseClass = null;
                _messageSystem.Publish(derivedClass,new ClearBaseClassEvent());
            }

            _classifiers.RemoveClassifier(classifier);
            _messageSystem.Publish(classifier,new DomainObjectDeletedEvent<Classifier>(classifier));
            _messageSystem.RemoveSource(classifier);
        }
    }
}
