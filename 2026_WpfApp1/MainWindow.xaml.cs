using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace _2026_WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ResultTextBox is not null && string.IsNullOrEmpty(InputTextBox?.Text))
            {
                ResultTextBox.Text = "";
            }
        }

        private void RunBotton_Click(object sender, RoutedEventArgs e)
        {
            if (ResultTextBox == null || InputTextBox == null) return;

            string input = InputTextBox.Text;

            if (string.IsNullOrEmpty(input))
            {
                ResultTextBox.Text = "";
                return;
            }

            if (int.TryParse(input, out int maxNum) && maxNum >= 1 && maxNum <= 9)
            {
                var sb = new StringBuilder();

                for (int i = 1; i <= maxNum; i++)
                {
                    for (int j = 1; j <= maxNum; j++)
                    {
                        sb.Append($" {i} x {j} = {i * j}\t");
                    }
                    sb.AppendLine();
                }

                ResultTextBox.Text = sb.ToString();
            }
            else
            {
                ResultTextBox.Text = "輸入1~9之間的數字";
            }
        }
    }
}
