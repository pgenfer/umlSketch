namespace Yuml
{
    /// <summary>
    /// renders the end node information into the relation 
    /// </summary>
    public class EndNode : RelationNode
    {
        public EndNodeWriter WriteTo(RelationEndWriter writer)
        {
            if (!string.IsNullOrEmpty(Name))
                writer = writer.WithName(Name);
            if (IsNavigatable)
                writer = writer.WithNavigation();
            return writer.ToClassifier(Classifier.Name);
        }

        public EndNode(Classifier classifier, string name = "", bool isNavigable = false) : 
            base(classifier, name, isNavigable)
        {
        }
    }
}