using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YumlFrontEnd.editor
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
