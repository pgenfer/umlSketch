namespace UmlSketch.DiagramWriter
{
    /// <summary>
    /// used to write the content of a class
    /// </summary>
    public class ClassWriter
    {
        private readonly bool _hasMembers = false;
        private readonly DiagramContentMixin _content;

        public ClassWriter(bool hasMembers, DiagramContentMixin content = null)
        {
            _hasMembers = hasMembers;
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

        public ClassWriter WithColor(string color)
        {
            if(!string.IsNullOrEmpty(color))
               AppendToken($"{{bg:{color}}}");
            return this;
        }

        public PropertyWriter WithNewProperty()
        {
            // first property that was added,
            // so add a separator before
            if (!_hasMembers)
                AppendToken("|");
            return new PropertyWriter(_content);
        }

        public MethodWriter WithNewMethod()
        {
            // first method was added,
            // so add a separator before
            if (!_hasMembers)
                AppendToken("|");
            return new MethodWriter(_content);
        }
        protected void AppendIdentifier(string identifier) => _content.AppendIdentifier(identifier);
        protected void AppendToken(string token) => _content.AppendToken(token);
    }
}