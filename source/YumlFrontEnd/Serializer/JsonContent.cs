using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Serializer
{
    /// <summary>
    /// class used to identify a 
    /// string as a json content
    /// </summary>
    public class JsonContent
    {
        private readonly string _content;

        public JsonContent(string content)
        {
            _content = content;
        }

        public string Value => _content;

        public override string ToString() => Value;
    }
}
