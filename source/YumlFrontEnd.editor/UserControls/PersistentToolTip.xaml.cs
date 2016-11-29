using System.Windows;

namespace UmlSketch.Editor
{
    /// <summary>
    /// Interaction logic for PersistentToolTip.xaml
    /// </summary>
    public partial class PersistentToolTip
    {
        public PersistentToolTip()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(PersistentToolTip), new PropertyMetadata(default(string)));

        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
    }
}
