using System.Diagnostics.Tracing;
using Common;

namespace YumlFrontEnd.editor
{
    /// <summary>
    /// a single classifier item in the list of
    /// selected classifiers
    /// </summary>
    public class ClassifierItemViewModel : AutoPropertyChange, INamed
    {
        private readonly NameMixin _name = new NameMixin();

        public ClassifierItemViewModel(string name)
        {
            Name = name;
        }

        public string Name
        {
            get { return _name.Name; }
            set { _name.Name = value;RaisePropertyChanged(); }
        }

        public override string ToString() => _name.ToString();

        /// <summary>
        /// implementation of the NullObject pattern. This 
        /// instance should be used instead of a null reference
        /// </summary>
        public static readonly ClassifierItemViewModel None = new ClassifierItemViewModel(EditorStrings.None);
    }
}