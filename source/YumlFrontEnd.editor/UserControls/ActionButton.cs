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
    /// Interaction logic for ActionButton.xaml
    /// </summary>
    public class ActionButton : Button
    {
        public static readonly DependencyProperty MouseOverBrushProperty = DependencyProperty.Register(
            "MouseOverBrush", typeof(Brush), typeof(ActionButton), new PropertyMetadata(default(Brush)));

        public Brush MouseOverBrush
        {
            get { return (Brush)GetValue(MouseOverBrushProperty); }
            set { SetValue(MouseOverBrushProperty, value); }
        }

        public static readonly DependencyProperty MouseDownBrushProperty = DependencyProperty.Register(
            "MouseDownBrush", typeof(Brush), typeof(ActionButton), new PropertyMetadata(default(Brush)));

        public Brush MouseDownBrush
        {
            get { return (Brush)GetValue(MouseDownBrushProperty); }
            set { SetValue(MouseDownBrushProperty, value); }
        }


    }


}
