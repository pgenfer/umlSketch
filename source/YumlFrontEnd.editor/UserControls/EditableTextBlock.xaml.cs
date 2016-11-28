using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Threading;

namespace YumlFrontEnd.editor
{
    /// <summary>
    /// Interaction logic for EditableTextBlock.xaml
    /// </summary>
    public partial class EditableTextBlock
    {
        public EditableTextBlock()
        {
            InitializeComponent();
            EditNameTextBox.TextChanged += EditNameTextBox_TextChanged;
            // automatically set focus if edit text is visible
            var editTextVisibilityProperty = DependencyPropertyDescriptor.FromProperty(
                VisibilityProperty, typeof(TextBox));
            editTextVisibilityProperty?.AddValueChanged(EditNameTextBox, (s, o) =>
            {
                var textBox = (TextBox) s;
                var isVisible = textBox.Visibility == Visibility.Visible;
                if (isVisible)
                {
                    Dispatcher.BeginInvoke(
                        DispatcherPriority.Input,
                        new Action(() =>
                        {
                            textBox.Focus();
                            Keyboard.Focus(textBox);
                            textBox.SelectAll();
                        }));
                }
            });
        }

        private bool IsWatermarkVisible() => 
            !string.IsNullOrEmpty(Watermark) && 
            string.IsNullOrEmpty(EditNameTextBox.Text);

        private void EditNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ShowWatermark = IsWatermarkVisible();
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == WatermarkProperty)
                ShowWatermark = IsWatermarkVisible();
            if (e.Property == ShowWatermarkProperty &&
                (bool) e.OldValue && !(bool) e.NewValue) // if watermark is deactivated, set focus
                EditNameTextBox.Focus();
            base.OnPropertyChanged(e);
        }

        public static readonly DependencyProperty WatermarkProperty = DependencyProperty.Register(
            "Watermark", typeof(string), typeof(EditableTextBlock), new PropertyMetadata(string.Empty));

        public string Watermark
        {
            get { return (string) GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        public static readonly DependencyProperty ShowWatermarkProperty = DependencyProperty.Register(
            "ShowWatermark", typeof(bool), typeof(EditableTextBlock), new PropertyMetadata(default(bool)));

        public bool ShowWatermark
        {
            get { return (bool) GetValue(ShowWatermarkProperty); }
            set { SetValue(ShowWatermarkProperty, value); }
        }

        public static readonly DependencyProperty ForegroundTextBrushProperty = DependencyProperty.Register(
            "ForegroundTextBrush", typeof(Brush), typeof(EditableTextBlock), 
            new PropertyMetadata(Brushes.Black));

        public Brush ForegroundTextBrush
        {
            get { return (Brush) GetValue(ForegroundTextBrushProperty); }
            set { SetValue(ForegroundTextBrushProperty, value); }
        }
    }
}
