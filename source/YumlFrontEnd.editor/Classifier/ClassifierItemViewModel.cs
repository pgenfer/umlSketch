using System.Diagnostics.Tracing;
using Caliburn.Micro;
using Common;

namespace YumlFrontEnd.editor
{
    /// <summary>
    /// a single classifier item in the list of
    /// selected classifiers
    /// </summary>
    public class ClassifierItemViewModel : PropertyChangedBase, INamed
    {
        private readonly NameMixin _name = new NameMixin();

        public ClassifierItemViewModel(string name)
        {
            Name = name;
        }

        public string Name
        {
            get { return _name.Name; }
            set { _name.Name = value;NotifyOfPropertyChange(); }
        }

        public override string ToString() => _name.ToString();

        /// <summary>
        /// implementation of the NullObject pattern. This 
        /// instance should be used instead of a null reference
        /// </summary>
        public static readonly ClassifierItemViewModel None = new ClassifierItemViewModel(EditorStrings.None);
    }
}