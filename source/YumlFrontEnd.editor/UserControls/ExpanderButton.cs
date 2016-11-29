using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UmlSketch.Editor
{
    /// <summary>
    /// there is a problem with WPF when changing the content of a button within a style:
    /// https://stackoverflow.com/questions/2898195/wpf-button-image-only-showing-in-last-control/2899697#2899697
    /// So when a button style is used for several buttons, only the last button get affected by the style
    /// trigger.
    /// Solution would be to completly rewrite the content template of the button in the style's trigger,
    /// which is too much effort. So instead the content of the button will be set here in code behind.
    /// </summary>
    public class ExpanderButton : ActionButton
    {
        private readonly TextBlock _textBlock = new TextBlock();

        public ExpanderButton()
        {
            Content = _textBlock;
            _textBlock.Text = ">";
            SetCollapsed();
        }

        private void SetExpanded()
        {
            _textBlock.LayoutTransform = new RotateTransform(-90);
            _textBlock.Margin = new Thickness(-5, 0, 0, 0);
            ToolTip = Tooltips.ClickToCollapse;
        }

        private void SetCollapsed()
        {
            _textBlock.LayoutTransform = new RotateTransform(90);
            _textBlock.Margin = new Thickness(-7, 0, 0, 0);
            ToolTip = Tooltips.ClickToExpand;
        }

        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsExpanded.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register("IsExpanded", typeof(bool), typeof(ExpanderButton),
                new PropertyMetadata(false,OnIsExpandedChanged));

        private static void OnIsExpandedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var expanderButton = (ExpanderButton)d;
            var isExpanded = (bool)e.NewValue;
            if (isExpanded)
                expanderButton.SetExpanded();
            else
                expanderButton.SetCollapsed();
        }
    }
}
