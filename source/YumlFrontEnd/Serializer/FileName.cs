using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.IO.File;
using static System.IO.Path;


namespace Yuml.Serializer
{
    /// <summary>
    /// class used to identifiy a string as a file name
    /// </summary>
    public class FileName
    {
        public FileName(string fileName)
        {
            Value = fileName;
        }

        public bool IsValid => Exists(Value);
        public string Path => !string.IsNullOrEmpty(Value) ? GetDirectoryName(Value) : null;
        public string Value { get; }
        public override string ToString() => Value;
    }
}
