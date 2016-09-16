namespace Yuml
{
    /// <summary>
    /// calls the correct rendering for the start node of a relation.
    /// Currently, navigating to the start node is not supported.
    /// </summary>
    public class StartNode : RelationNode
    {
        public StartNodeWriter WriteTo(RelationWriter writer)
        {
            var start = writer.WithStartNode(Classifier.Name);
            if (!string.IsNullOrEmpty(Name))
                start = start.WithName(Name);
            return start;
        }

        public StartNode(Classifier classifier, string name = "", bool isNavigable = false) : 
            base(classifier, name, isNavigable)
        {
        }
    }
}