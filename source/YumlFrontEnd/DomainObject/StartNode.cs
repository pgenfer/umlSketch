namespace Yuml
{
    /// <summary>
    /// calls the correct rendering for the start node of a relation
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
    }
}