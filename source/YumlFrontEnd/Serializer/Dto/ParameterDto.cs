using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Serializer.Dto
{
    /// <summary>
    /// Dto object to store information
    /// of a parameter
    /// </summary>
    internal class ParameterDto
    {
        public string Name { get; set; }
        public ClassifierDto Type { get; set; }

        public override bool Equals(object obj) => 
            obj is ParameterDto && 
            Name == ((ParameterDto)obj).Name && 
            Type.Equals(((ParameterDto)obj).Type);

        public override int GetHashCode() => Name.GetHashCode() ^ Type.GetHashCode();
    }
}
