using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml;
using Yuml.Command;

namespace YumlFrontEnd.editor
{
    /// <summary>
    /// item source for implementations of interfaces.
    /// only the following classifiers may be available here:
    /// 1. classifiers must be interfaces
    /// 2. they may not be in the implementation list of this classifier already
    /// 3. they may not be the owning classifier itself
    /// Best way would be to put this logic into its own query object
    /// and provide it during initialization of the items source.
    /// </summary>
    public class InterfaceSelectionItemsSource : ClassifierSelectionItemsSource
    {
        private readonly ClassifierDictionary _classifiers;

        public InterfaceSelectionItemsSource(
            ClassifierDictionary classifiers,
            IQuery<Classifier> availableClassifiers,
            MessageSystem messageSystem):base(classifiers,availableClassifiers.Get,messageSystem)
        {
            _classifiers = classifiers;
        }

        protected override bool ShouldItemBeVisible(Classifier classifier) => classifier.IsInterface;

        protected override void RegisterForClassifierEvent(MessageSystem messageSystem, Classifier classifier)
        {
            messageSystem.Subscribe<ClassToInterfaceEvent>(classifier, OnClassToInterfaceChanged);
            messageSystem.Subscribe<InterfaceToClassEvent>(classifier, OnInterfaceToClassChanged);
        }

        private void OnClassToInterfaceChanged(ClassToInterfaceEvent domainEvent)
        {
            // item could already be added because it was a base class of this classifier
            // so do not add it again when this event appears
            var item = ByName(domainEvent.ClassName);
            if (item != null)
                return;
            // add the new interface to the list
            var @class = _classifiers.FindByName(domainEvent.ClassName);
            Add(new ClassifierItemViewModel(@class.Name));
        }

        private void OnInterfaceToClassChanged(InterfaceToClassEvent domainEvent)
        {
            // remove the interface from the available interface list
            var item = ByName(domainEvent.InterfaceName);
            if (item != null)
                Remove(item);
        }
    }
}
