using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml
{
    public class ParameterWriter
    {
        DiagramContentMixin _content;

        public ParameterWriter(DiagramContentMixin content = null)
        {
            _content = content;
        }

        public ParameterWriter AddParameter(string type, string name)
        {
            AppendIdentifier(type);
            AppendIdentifier(name);

            return this;
        }

        public bool IsEmpty => _content.IsEmpty;

        private void AppendIdentifier(string identifier) => _content.AppendIdentifier(identifier);
        public void AppendToken(string token) => _content.AppendToken(token);
        public override string ToString() => _content.ToString();
    }
}
