namespace UmlSketch.Serializer
{
    /// <summary>
    /// class used to identify a 
    /// string as a json content
    /// </summary>
    public class JsonContent
    {
        public JsonContent(string content)
        {
            Value = content;
        }

        public string Value { get; }

        public override string ToString() => Value;
    }
}
