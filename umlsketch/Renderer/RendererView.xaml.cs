using System;

namespace UmlSketch.Editor
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


    
}
