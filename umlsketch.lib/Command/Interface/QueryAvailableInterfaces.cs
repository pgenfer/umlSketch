using System.Linq;
using UmlSketch.DomainObject;

namespace UmlSketch.Command
{
    /// <summary>
    /// provides a list of all interfaces that can be implemented by this
    /// classifer
    /// </summary>
    public class QueryAvailableInterfaces : Query<Classifier>
    {
        public QueryAvailableInterfaces(
            Implementation implementation, 
            ClassifierDictionary classifiers)
            : base(() =>
                // the list contains all interfaces that are not already implemented
                // but the interface itself (otherwise, it could not be selected in the list)
                classifiers
                    .Where(x => x.IsInterface) 
                    .Where(x => x != implementation.Start.Classifier) // no implementation recursion allowed
                    .Except(implementation
                        .Start
                        .Classifier
                        .InterfaceImplementations
                        .ImplementedInterfaces
                        .Except(new [] {implementation.End.Classifier})))
        {
        }
    }
}
