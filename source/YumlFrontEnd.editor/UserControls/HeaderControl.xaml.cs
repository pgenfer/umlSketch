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
    /// Interaction logic for HeaderControl.xaml
    /// </summary>
    public partial class HeaderControl
    {
        public HeaderControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title", typeof(string), typeof(HeaderControl), new PropertyMetadata(default(string)));

        public string Title
        {
            get { return (string) GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty IsExpandedProperty = DependencyProperty.Register(
            "IsExpanded", typeof(bool), typeof(HeaderControl), new PropertyMetadata(default(bool)));

        public bool IsExpanded
        {
            get { return (bool) GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        public static readonly DependencyProperty HasExpandButtonProperty = DependencyProperty.Register(
            "HasExpandButton", typeof(bool), typeof(HeaderControl), new PropertyMetadata(default(bool)));

        public bool HasExpandButton
        {
            get { return (bool) GetValue(HasExpandButtonProperty); }
            set { SetValue(HasExpandButtonProperty, value); }
        }

        public static readonly DependencyProperty HasColorPickerProperty = DependencyProperty.Register(
            "HasColorPicker", typeof(bool), typeof(HeaderControl), new PropertyMetadata(false));

        public static readonly DependencyProperty HasNewButtonProperty = DependencyProperty.Register(
            "HasNewButton", typeof(bool), typeof(HeaderControl), new PropertyMetadata(true));

        public bool HasNewButton
        {
            get { return (bool) GetValue(HasNewButtonProperty); }
            set { SetValue(HasNewButtonProperty, value); }
        }

        public static readonly DependencyProperty HasDeleteButtonProperty = DependencyProperty.Register(
            "HasDeleteButton", typeof(bool), typeof(HeaderControl), new PropertyMetadata(false));

        public bool HasDeleteButton
        {
            get { return (bool) GetValue(HasDeleteButtonProperty); }
            set { SetValue(HasDeleteButtonProperty, value); }
        }

        public bool HasColorPicker
        {
            get { return (bool) GetValue(HasColorPickerProperty); }
            set { SetValue(HasColorPickerProperty, value); }
        }
    }
}
