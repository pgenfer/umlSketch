using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UmlSketch.Editor
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
