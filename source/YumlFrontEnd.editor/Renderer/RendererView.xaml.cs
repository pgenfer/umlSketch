using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


    
}
