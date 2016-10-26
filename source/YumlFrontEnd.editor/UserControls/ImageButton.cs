using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace YumlFrontEnd.editor
{
    public class ImageButton : Button
    {
        public static readonly DependencyProperty MouseOverImageProperty = DependencyProperty.Register(
            "MouseOverImage", typeof(ImageSource), typeof(ImageButton), new PropertyMetadata(default(ImageSource)));

        public ImageSource MouseOverImage
        {
            get { return (ImageSource) GetValue(MouseOverImageProperty); }
            set { SetValue(MouseOverImageProperty, value); }
        }

        public static readonly DependencyProperty DefaultImageProperty = DependencyProperty.Register(
            "DefaultImage", typeof(ImageSource), typeof(ImageButton), new PropertyMetadata(default(ImageSource)));

        public ImageSource DefaultImage
        {
            get { return (ImageSource) GetValue(DefaultImageProperty); }
            set { SetValue(DefaultImageProperty, value); }
        }
    }
}
