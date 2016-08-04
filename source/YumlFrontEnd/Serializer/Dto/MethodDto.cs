using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Serializer.Dto
{
    /// <summary>
    /// Dto object to store method information
    /// </summary>
    internal class MethodDto
    {
        public string Name { get; set; }
        public ClassifierDto ReturnType { get; set; }
        public List<ParameterDto> Parameters { get; set; }
        public bool IsVisible { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is MethodDto))
                return false;

            var otherMethod = (MethodDto)obj;
            var isValid =
                Name == otherMethod.Name &&
                ReturnType.Equals(otherMethod.ReturnType);
            for (var i = 0; i < Parameters.Count; i++)
                if (!Parameters[i].Equals(otherMethod.Parameters[i]))
                    return false;
            return isValid;
        }

        public override int GetHashCode()
        {
            var hash = Name.GetHashCode() ^ ReturnType.GetHashCode();
            foreach (var parameter in Parameters)
                hash ^= parameter.GetHashCode();
            return hash;
            
        }
    }
}
