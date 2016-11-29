namespace UmlSketch.Serializer.Dto
{
    /// <summary>
    /// Dto object to store property information
    /// </summary>
    internal class PropertyDto
    {
        public string Name { get; set; }
        public ClassifierDto Type {get;set;}
        public bool IsVisible { get; set; }
    }
}
