using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml
{
    /// <summary>
    /// represents a node within the diagram.
    /// Notes can either be attached to a class or to 
    /// the entire diagram.
    /// </summary>
    public class Note
    {
        /// <summary>
        /// text content of the diagram
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// background color of the note
        /// </summary>
        public string Color { get; set; }

        public bool HasColor => !string.IsNullOrEmpty(Color);
        public bool HasText => !string.IsNullOrEmpty(Text);

        public void Clear()
        {
            Text = string.Empty;
            Color = string.Empty;
        }

        public Note()
        {
            
        }
    }
}
