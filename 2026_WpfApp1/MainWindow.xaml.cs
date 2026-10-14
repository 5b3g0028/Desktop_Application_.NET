using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;

namespace _2026_WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Color strokeColor = Colors.Black;
        Color fillColor = Colors.Transparent;
        int strokeThickness = 1;
        string shapeType = "Line";

        public MainWindow()
        {
            InitializeComponent();
            if (FillColorPicker != null)
            {
                FillColorPicker.SelectedColor = fillColor;
            }
            DisplayStatus();
        }

        private void ShapeButton_Checked(object sender, RoutedEventArgs e)
        {
            var targetShapeButton = sender as RadioButton;
            shapeType = targetShapeButton?.Tag?.ToString() ?? "Line";
            DisplayStatus();
        }

        private void BrushColorCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BrushColorCombo.SelectedItem is ComboBoxItem item && item.Tag is string colorStr)
            {
                strokeColor = (Color)ColorConverter.ConvertFromString(colorStr);
                DisplayStatus();
            }
        }

        private void FillColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (FillColorPicker.SelectedColor.HasValue)
            {
                fillColor = FillColorPicker.SelectedColor.Value;
                DisplayStatus();
            }
        }

        private void ThicknessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            strokeThickness = (int)e.NewValue;
            DisplayStatus();
        }

        private void DisplayStatus()
        {
            if (ColorLabel != null)
                ColorLabel.Content = $"筆刷色彩: {strokeColor}     填充色彩: {fillColor}     筆刷粗細: {strokeThickness}";
            if (ShapeLabel != null)
                ShapeLabel.Content = $"形狀: {shapeType}";
        }
    }
}