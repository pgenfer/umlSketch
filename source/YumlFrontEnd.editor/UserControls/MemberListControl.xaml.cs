using System.Windows;

namespace UmlSketch.Editor
{
    /// <summary>
    /// controls used to visualize a list of members
    /// with an additional header area.
    /// The header contains expand functionality
    /// and a toolbar with additional commands
    /// </summary>
    public partial class MemberListControl
    {
        public MemberListControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title", typeof(string), typeof(MemberListControl), new PropertyMetadata(default(string)));

        public string Title
        {
            get { return (string) GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty MemberItemStyleProperty = DependencyProperty.Register(
            "MemberItemStyle", typeof(Style), typeof(MemberListControl), new PropertyMetadata(default(Style)));

        public Style MemberItemStyle
        {
            get { return (Style) GetValue(MemberItemStyleProperty); }
            set { SetValue(MemberItemStyleProperty, value); }
        }
    }
}
