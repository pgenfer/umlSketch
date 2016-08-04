using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Serializer
{
    /// <summary>
    /// class used to identifiy a string as a file name
    /// </summary>
    public class FileName
    {
        private readonly string _fileName;

        public FileName(string fileName)
        {
            _fileName = fileName;
        }

        public string Value => _fileName;

        public override string ToString() => Value;
    }
}
