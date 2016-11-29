using System.Linq;
using UmlSketch.DomainObject;
using UmlSketch.Event;

namespace UmlSketch.Editor
{
    /// <summary>
    /// shows the available classes that can be used as base classes.
    /// Contains special handling in case a class is changed from or to a base class
    /// </summary>
    public class BaseClassSelectionItemSource : ClassifierSelectionItemsSource
    {
        private readonly ClassifierDictionary _classifiers;

        public BaseClassSelectionItemSource(
           ClassifierDictionary classifiers,
           string derivedClassName,
           MessageSystem messageSystem):
            base(
                classifiers, 
                () => classifiers.Where(x => 
                    x.Name != derivedClassName && 
                    !x.IsSystemType),
                messageSystem,true)
        {
            _classifiers = classifiers;
        }

        /// <summary>
        /// hide all interfaces from the class list
        /// </summary>
        /// <param name="classifier"></param>
        /// <returns></returns>
        protected override bool ShouldItemBeVisible(Classifier classifier) => !classifier.IsInterface;

        protected override void RegisterForClassifierEvent(MessageSystem messageSystem, Classifier classifier)
        {
            messageSystem.Subscribe<ClassToInterfaceEvent>(classifier,OnClassToInterfaceChanged);
            messageSystem.Subscribe<InterfaceToClassEvent>(classifier, OnInterfaceToClassChanged);
        }

        private void OnClassToInterfaceChanged(ClassToInterfaceEvent domainEvent)
        {
            var item = ByName(domainEvent.ClassName);
            if (item != null)
                Remove(item);
        }

        private void OnInterfaceToClassChanged(InterfaceToClassEvent domainEvent)
        {
            // a new class was added that cannot be part of our list,
            // so add it explicitly
            var @class = _classifiers.FindByName(domainEvent.InterfaceName);
            Add(new ClassifierItemViewModel(@class.Name));
        }
    }
}
