using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuml.metadata;

namespace yuml.Serializer
{
    public class DiagramSerializer
    {
        public void Save(string fileName,Diagram diagram) {/* should save json format */ }
        public Diagram Load(string fileName) { /* load from json file */return default(Diagram); }
    }
}
