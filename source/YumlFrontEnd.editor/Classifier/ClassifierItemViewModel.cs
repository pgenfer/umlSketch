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
    }
}