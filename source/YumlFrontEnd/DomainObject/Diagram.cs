using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuml
{
    /// <summary>
    /// representation of a diagram
    /// </summary>
    public class Diagram
    {
        /// <summary>
        /// a single note that can be attached to the diagram
        /// </summary>
        public Note Note { get; set; } = new Note();
        public ClassifierDictionary Classifiers { get; }  = new ClassifierDictionary();

        public void Clear()
        {
            Note.Clear();
            Classifiers.Clear();
        }

        public Diagram() { }

        /// <summary>
        /// constructor used for testing only 
        /// </summary>
        /// <param name="classifiers"></param>
        internal Diagram(ClassifierDictionary classifiers)
        {
            Classifiers = classifiers;
        }

        public void WriteTo(DiagramWriter writer)
        {
            Classifiers.WriteTo(writer);
            if (Note.HasText)
            {
                // we need a separator between the
                // classes and the diagram note
                if (Classifiers.Count > 0)
                    writer.AddSeparator();
                writer.WithNote(Note);
            }
        }
    }
}
