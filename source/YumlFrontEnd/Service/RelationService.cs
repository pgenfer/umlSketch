using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Service
{
    /// <summary>
    /// the relation service handles the relation between classifiers
    /// if any of their relation changes.
    /// Example: A classifier is changed from a class to an interface,
    /// now all derived class must change their relation from Inheritance to Implementation.
    /// Currently, the following rules apply:
    /// 1. Classifier changed to interface 
    ///     => class cannot have base class any more
    ///     => change base classes to interfaces
    ///     => class cannot be used as base class for other classes any more
    /// 2. Interface changed to classifier 
    ///     => change interface to base class (if no other base class)
    ///     => class cannot be used as interface for other classes any more
    /// </summary>
    public class RelationService : IRelationService
    {
        private readonly ClassifierDictionary _classifiers;
        private readonly MessageSystem _messageSystem;

        public RelationService(
            ClassifierDictionary classifiers,
            MessageSystem messageSystem)
        {
            _classifiers = classifiers;
            _messageSystem = messageSystem;
        }

        public void ChangeFromInterfaceToClass(Classifier @interface)
        {
            @interface.IsInterface = false;
            // fire the event first, otherwise the interface item sources
            // do not contain have the new interface in their list yet
            _messageSystem.Publish(@interface, new InterfaceToClassEvent(@interface.Name));

            var implementers = _classifiers.FindAllImplementers(@interface);
            foreach (var implementer in implementers)
            {
                implementer.RemoveImplementationForInterface(@interface,_messageSystem);
                // if implementer has no base class, set class as base
                if (implementer.BaseClass == null)
                    implementer.SetBaseClass(@interface, _messageSystem);
            }

            
        }

        public void ChangeFromClassToInterface(Classifier @class)
        {
            @class.IsInterface = true;
            // the class itself cannot have a base class any more
            @class.ClearBaseClass(_messageSystem);
            // find all derived classes with this base class
            var derivedClasses = _classifiers.FindAllDerivedClassifiers(@class);
            foreach (var derivedClass in derivedClasses)
            {
                derivedClass.ClearBaseClass(_messageSystem);
                derivedClass.AddInterfaceImplementation(@class, _messageSystem);
            }

            // fire the notification at the end, because this will change all
            // existing classifier list items
            
            _messageSystem.Publish(@class, new ClassToInterfaceEvent(@class.Name));
        }
    }
}
