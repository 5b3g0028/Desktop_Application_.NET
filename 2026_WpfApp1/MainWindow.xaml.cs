using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

namespace _2026_WpfApp1
{
    public partial class MainWindow : Window
    {
        private Color strokeColor = Colors.Black;
        private Color fillColor = Colors.Transparent;
        private int strokeThickness = 1;
        private string shapeType = "Line";
        private string actionType = "Draw";
        private Point startPoint;
        private Point endPoint;
        private Shape? currentPreviewShape;

        public MainWindow()
        {
            InitializeComponent();

            if (StrokeColorPicker != null)
                StrokeColorPicker.SelectedColor = strokeColor;

            if (FillColorPicker != null)
                FillColorPicker.SelectedColor = fillColor;

            DisplayStatus();
        }

        private void ShapeButton_Checked(object sender, RoutedEventArgs e)
        {
            var radio = sender as RadioButton;
            actionType = "Draw";
            shapeType = radio?.Tag?.ToString() ?? "Line";
            DisplayStatus();
        }

        private void StrokeColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (StrokeColorPicker.SelectedColor.HasValue)
            {
                strokeColor = StrokeColorPicker.SelectedColor.Value;
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

        private void MyCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (actionType == "Eraser")
            {
                var pos = e.GetPosition(MyCanvas);
                var element = VisualTreeHelper.HitTest(MyCanvas, pos)?.VisualHit;
                if (element is FrameworkElement fe && fe != MyCanvas)
                {
                    if (fe is Shape shape)
                    {
                        MyCanvas.Children.Remove(shape);
                    }
                    else if (fe.Parent is Shape parentShape)
                    {
                        MyCanvas.Children.Remove(parentShape);
                    }
                }
                return;
            }

            startPoint = e.GetPosition(MyCanvas);
            endPoint = startPoint;
            MyCanvas.Cursor = Cursors.Cross;

            switch (shapeType)
            {
                case "Line":
                    currentPreviewShape = new Line
                    {
                        X1 = startPoint.X,
                        Y1 = startPoint.Y,
                        X2 = endPoint.X,
                        Y2 = endPoint.Y,
                        Stroke = new SolidColorBrush(strokeColor),
                        StrokeThickness = strokeThickness
                    };
                    MyCanvas.Children.Add(currentPreviewShape);
                    break;

                case "Rectangle":
                    currentPreviewShape = new Rectangle
                    {
                        Stroke = new SolidColorBrush(strokeColor),
                        Fill = new SolidColorBrush(fillColor),
                        StrokeThickness = strokeThickness
                    };
                    MyCanvas.Children.Add(currentPreviewShape);
                    break;

                case "Ellipse":
                    currentPreviewShape = new Ellipse
                    {
                        Stroke = new SolidColorBrush(strokeColor),
                        Fill = new SolidColorBrush(fillColor),
                        StrokeThickness = strokeThickness
                    };
                    MyCanvas.Children.Add(currentPreviewShape);
                    break;

                case "Polyline":
                    currentPreviewShape = new Polyline
                    {
                        Stroke = new SolidColorBrush(strokeColor),
                        Fill = new SolidColorBrush(fillColor),
                        StrokeThickness = strokeThickness
                    };
                    ((Polyline)currentPreviewShape).Points.Add(startPoint);
                    MyCanvas.Children.Add(currentPreviewShape);
                    break;
            }
        }

        private void MyCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (actionType == "Eraser")
                return;

            if (e.LeftButton != MouseButtonState.Pressed || currentPreviewShape == null)
                return;

            endPoint = e.GetPosition(MyCanvas);

            switch (shapeType)
            {
                case "Line":
                    var line = (Line)currentPreviewShape;
                    line.X2 = endPoint.X;
                    line.Y2 = endPoint.Y;
                    break;

                case "Rectangle":
                    var rect = (Rectangle)currentPreviewShape;
                    double left = Math.Min(startPoint.X, endPoint.X);
                    double top = Math.Min(startPoint.Y, endPoint.Y);
                    double width = Math.Abs(endPoint.X - startPoint.X);
                    double height = Math.Abs(endPoint.Y - startPoint.Y);

                    Canvas.SetLeft(rect, left);
                    Canvas.SetTop(rect, top);
                    rect.Width = width;
                    rect.Height = height;
                    break;

                case "Ellipse":
                    var ellipse = (Ellipse)currentPreviewShape;
                    var eLeft = Math.Min(startPoint.X, endPoint.X);
                    var eTop = Math.Min(startPoint.Y, endPoint.Y);
                    var eWidth = Math.Abs(endPoint.X - startPoint.X);
                    var eHeight = Math.Abs(endPoint.Y - startPoint.Y);

                    Canvas.SetLeft(ellipse, eLeft);
                    Canvas.SetTop(ellipse, eTop);
                    ellipse.Width = eWidth;
                    ellipse.Height = eHeight;
                    break;

                case "Polyline":
                    var poly = (Polyline)currentPreviewShape;
                    poly.Points.Add(endPoint);
                    break;
            }
        }

        private void MyCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (actionType == "Eraser")
                return;

            endPoint = e.GetPosition(MyCanvas);

            if (currentPreviewShape is Line line)
            {
                line.Stroke = new SolidColorBrush(strokeColor);
                line.StrokeThickness = strokeThickness;
            }
            else if (currentPreviewShape is Rectangle rect)
            {
                rect.Stroke = new SolidColorBrush(strokeColor);
                rect.Fill = new SolidColorBrush(fillColor);
                rect.StrokeThickness = strokeThickness;
            }
            else if (currentPreviewShape is Ellipse ellipse)
            {
                ellipse.Stroke = new SolidColorBrush(strokeColor);
                ellipse.Fill = new SolidColorBrush(fillColor);
                ellipse.StrokeThickness = strokeThickness;
            }
            else if (currentPreviewShape is Polyline poly)
            {
                poly.Stroke = new SolidColorBrush(strokeColor);
                poly.Fill = new SolidColorBrush(fillColor);
                poly.StrokeThickness = strokeThickness;
            }

            currentPreviewShape = null;
        }

        private void EraserButton_Click(object sender, RoutedEventArgs e)
        {
            actionType = "Eraser";
            MyCanvas.Cursor = Cursors.Arrow;
            DisplayStatus();
        }

        private void MYcanvas_MouseEnter(object sender, MouseEventArgs e)
        {
            MyCanvas.Cursor = Cursors.Cross;
        }

        private void DisplayStatus()
        {
            if (StatusLabel != null)
                StatusLabel.Content = $"模式:{actionType} / 形狀:{shapeType}";

            if (ColorLabel != null)
                ColorLabel.Content = $"筆刷色彩:{strokeColor}     填充色彩:{fillColor}     筆刷粗細:{strokeThickness}";

            if (ShapeLabel != null)
                ShapeLabel.Content = $"形狀:{shapeType}";
        }
    }
}