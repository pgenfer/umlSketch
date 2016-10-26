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
        public JsonContent(string content)
        {
            Value = content;
        }

        public string Value { get; }

        public override string ToString() => Value;
    }
}
