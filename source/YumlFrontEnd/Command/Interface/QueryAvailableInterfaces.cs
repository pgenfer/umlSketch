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
        public QueryAvailableInterfaces(Classifier owner,ClassifierDictionary classifiers)
            :base(() => classifiers
                        .Where(x => x.IsInterface) // show only interfaces
                        .Where(x => x != owner) // a class cannot implement itself
                        .Except(owner.InterfaceImplementations.ImplementedInterfaces)) // skip interfaces which are already implemented
        { }
    }
}
