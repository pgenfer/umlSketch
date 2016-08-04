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
        public ClassifierDto ParameterType { get; set; }

        public override bool Equals(object obj) => 
            obj is ParameterDto && 
            Name == ((ParameterDto)obj).Name && 
            ParameterType.Equals(((ParameterDto)obj).ParameterType);

        public override int GetHashCode() => Name.GetHashCode() ^ ParameterType.GetHashCode();
    }
}
