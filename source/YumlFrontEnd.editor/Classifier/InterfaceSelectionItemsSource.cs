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
        public InterfaceSelectionItemsSource(
            ClassifierDictionary classifiers,
            IQuery<Classifier> availableClassifiers,
            MessageSystem messageSystem):base(classifiers,availableClassifiers.Get,messageSystem)
        {
        }
    }
}
