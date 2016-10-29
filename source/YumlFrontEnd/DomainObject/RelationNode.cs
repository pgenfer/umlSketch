using Common;
using static System.Diagnostics.Contracts.Contract;

namespace Yuml
{
    /// <summary>
    /// a single node of a relation
    /// </summary>
    public class RelationNode : INamed
    {
        private readonly INamed _named = new NameMixin();
        private Classifier _classifier;

        /// <summary>
        /// the classifier that is represented by this node
        /// </summary>
        public Classifier Classifier
        {
            get { return _classifier; }
            set
            {
                Requires(value != null);
                _classifier = value;
            }
        }

        /// <summary>
        /// optional name of the relation's start node.
        /// </summary>
        public string Name
        {
            get { return _named.Name; }
            set { _named.Name = value; }
        }

        /// <summary>
        /// flag if the relation can be traversed to this node.
        /// </summary>
        public bool IsNavigatable { get; set; }

        public RelationNode(Classifier classifier, string name = "", bool isNavigable = false)
        {
            Classifier = classifier;
            Name = name;
            IsNavigatable = isNavigable;
        }
    }
}