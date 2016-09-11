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
    /// Interaction logic for RendererView.xaml
    /// </summary>
    public partial class RendererView
    {
        public RendererView()
        {
            InitializeComponent();
        }

        public void NavigateTo(string uri) => WebBrowser.Navigate(new Uri(uri));
    }


    // http://stackoverflow.com/questions/4202961/can-i-bind-html-to-a-wpf-web-browser-control
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
