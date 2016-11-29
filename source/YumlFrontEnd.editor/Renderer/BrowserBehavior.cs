using System.Windows;
using System.Windows.Controls;

namespace UmlSketch.Editor
{
    /// <summary>
    /// Behavior is used to enable binding of the URL of the browser. See here for more details:
    /// http://stackoverflow.com/questions/4202961/can-i-bind-html-to-a-wpf-web-browser-control
    /// </summary>
    public class BrowserBehavior
    {
        public static readonly DependencyProperty UrlProperty = DependencyProperty.RegisterAttached(
            "Url",
            typeof(string),
            typeof(BrowserBehavior),
            new FrameworkPropertyMetadata(OnUrlChanged));

        [AttachedPropertyBrowsableForType(typeof(WebBrowser))]
        public static string GetUrl(WebBrowser d) => (string)d.GetValue(UrlProperty);
        public static void SetUrl(WebBrowser d, string value) => d.SetValue(UrlProperty, value);

        private static void OnUrlChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var renderView = dependencyObject as RendererView;
            renderView?.NavigateTo(e.NewValue.ToString());
        }
    }
}