using UmlSketch.Settings;

namespace UmlSketch.DomainObject
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

        public void Reset()
        {
            Note.Clear();
            Classifiers.Clear();
            Classifiers.AddMissingSystemTypes();
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

        public void WriteTo(DiagramWriter.DiagramWriter writer,DiagramDirection direction)
        {
            Classifiers.WriteTo(writer,direction);
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
