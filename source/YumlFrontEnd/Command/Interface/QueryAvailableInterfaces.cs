using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Command
{
    /// <summary>
    /// provides a list of all interfaces that can be implemented by this
    /// classifer
    /// </summary>
    public class QueryAvailableInterfaces : Query<Classifier>
    {
        public QueryAvailableInterfaces(Implementation implementation, ClassifierDictionary classifiers)
            :base(() => classifiers
                        // show only interfaces            
                        .Where(x => x.IsInterface)
                        // a class cannot implement itself
                        .Where(x => x != implementation.Start.Classifier) 
                        // skip interfaces which are already implemented
                        // TODO: the chaining to access the classifier is a bit awful
                        .Except(implementation.Start.Classifier.InterfaceImplementations.ImplementedInterfaces)) 
            
        { }
    }
}
