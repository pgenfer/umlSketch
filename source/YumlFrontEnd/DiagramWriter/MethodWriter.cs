namespace UmlSketch.DiagramWriter
{
    /// <summary>
    /// writes a single method 
    /// to a class. After the method is written,
    /// call Finish() to return back to the
    /// owning ClassWriter.
    /// Interestingly, it currently seems like that yumle
    /// does not support more than one method parameter, so
    /// we will skip the generation of them here also
    /// </summary>
    public class MethodWriter
    {
        private readonly DiagramContentMixin _content;

        public MethodWriter WithReturnType(string type)
        {
            AppendIdentifier(type);
            return this;
        }
        public MethodWriter WithName(string name)
        {
            AppendIdentifier(name);
            AppendToken("(");
            return this;
        }
        public MethodWriter WithNewMethod()
        {
            AppendToken(");");
            return new MethodWriter(_content);
        }

        public ParameterWriter WithParameter()
        {
            return new ParameterWriter(_content);
        }

        public ClassWriter Finish()
        {
            AppendToken(");");
            // this class must have at least one method,
            // because we are just inside the property writer
            return new ClassWriter(true, _content);
        }
        protected void AppendIdentifier(string identifier) => _content.AppendIdentifier(identifier);
        protected void AppendToken(string token) => _content.AppendToken(token);

        public MethodWriter(DiagramContentMixin content = null)
        {
            _content = content;
        }
    }
}