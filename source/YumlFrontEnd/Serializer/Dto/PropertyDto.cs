using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml.Serializer.Dto
{
    /// <summary>
    /// Dto object to store property information
    /// </summary>
    internal class PropertyDto
    {
        public string Name { get; set; }
        public ClassifierDto Type {get;set;}
        public bool IsVisible { get; set; }
    }
}
