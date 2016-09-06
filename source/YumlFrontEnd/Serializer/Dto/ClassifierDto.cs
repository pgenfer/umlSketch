using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Serializer.Dto
{
    /// <summary>
    /// classifier object used to store information
    /// about a classifier business object
    /// </summary>
    internal class ClassifierDto
    {
        public string Name { get; set; }
        public List<PropertyDto> Properties { get; set; }
        public List<MethodDto> Methods { get; set; }
        public bool IsVisible { get; set; }
        public ClassifierDto BaseClass { get; set; }

        public override bool Equals(object obj) => 
            obj is ClassifierDto && Name == ((ClassifierDto)obj).Name;
        public override int GetHashCode() => Name.GetHashCode();
    }
}
