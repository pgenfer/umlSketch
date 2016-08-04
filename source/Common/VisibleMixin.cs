using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// defines whether an object is visible on the diagram or not.
    /// </summary>
    public class VisibleMixin : IVisible
    {
        /// <summary>
        /// if set to true, object will be rendered,
        /// otherwise object is not visible on the diagram
        /// </summary>
        public bool IsVisible { get; set; }
    }
}
