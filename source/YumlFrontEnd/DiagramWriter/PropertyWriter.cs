namespace Yuml
{
    /// <summary>
    /// writes a single property 
    /// to a class. After the property is written,
    /// call Finish() to return back to the
    /// owning ClassWriter.
    /// </summary>
    public class PropertyWriter
    {
        private readonly DiagramContentMixin _content;

        public PropertyWriter WithType(string type)
        {
            AppendIdentifier(type);
            return this;
        }
        public PropertyWriter WithName(string name)
        {
            AppendIdentifier(name);
            return this;
        }
        public PropertyWriter WithNewProperty()
        {
            AppendToken(";");
            return new PropertyWriter(_content);
        }

        public ClassWriter Finish()
        {
            AppendToken(";");
            // this class must have at least one property,
            // because we are just inside the property writer
            return new ClassWriter(true, _content);
        }
        protected void AppendIdentifier(string identifier) => _content.AppendIdentifier(identifier);
        protected void AppendToken(string token) => _content.AppendToken(token);
      
        public PropertyWriter(DiagramContentMixin content = null)
        {
            _content = content;
        }
    }
}