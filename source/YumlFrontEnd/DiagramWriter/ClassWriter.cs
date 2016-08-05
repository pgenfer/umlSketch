namespace Yuml
{
    /// <summary>
    /// used to write the content of a class
    /// </summary>
    public class ClassWriter
    {
        private bool _hasProperties = false;
        private DiagramContentMixin _content;

        public ClassWriter(bool hasProperties, DiagramContentMixin content = null)
        {
            _hasProperties = hasProperties;
            _content = content;
        }
        public DiagramWriter Finish()
        {
            AppendToken("]");
            return new DiagramWriter(_content);
        }
        public ClassWriter WithName(string name)
        {
            AppendIdentifier(name);
            return this;
        }

        public PropertyWriter WithNewProperty()
        {
            // first property that was added,
            // so add a separator before
            if (!_hasProperties)
                AppendToken("|");
            return new PropertyWriter(_content);
        }
        protected void AppendIdentifier(string identifier) => _content.AppendIdentifier(identifier);
        protected void AppendToken(string token) => _content.AppendToken(token);
    }
}