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
    /// Interaction logic for ButtonBar.xaml
    /// </summary>
    public partial class ButtonBar
    {
        public ButtonBar()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty HasNewButtonProperty = DependencyProperty.Register(
            "HasNewButton", typeof(bool), typeof(ButtonBar), new PropertyMetadata(true));

        public bool HasNewButton
        {
            get { return (bool) GetValue(HasNewButtonProperty); }
            set { SetValue(HasNewButtonProperty, value); }
        }

        public static readonly DependencyProperty HasDeleteButtonProperty = DependencyProperty.Register(
            "HasDeleteButton", typeof(bool), typeof(ButtonBar), new PropertyMetadata(true));

        public bool HasDeleteButton
        {
            get { return (bool) GetValue(HasDeleteButtonProperty); }
            set { SetValue(HasDeleteButtonProperty, value); }
        }

        public static readonly DependencyProperty HasColorPickerProperty = DependencyProperty.Register(
            "HasColorPicker", typeof(bool), typeof(ButtonBar), new PropertyMetadata(default(bool)));

        public bool HasColorPicker
        {
            get { return (bool) GetValue(HasColorPickerProperty); }
            set { SetValue(HasColorPickerProperty, value); }
        }
    }
}
