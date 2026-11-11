using System.Windows;
using System.Windows.Input;

namespace _2026_WpfApp1
{
    /// <summary>
    /// MYdocvier.xaml 的互動邏輯
    /// </summary>
    public partial class MYdocvier : Window
    {
        public MYdocvier()
        {
            InitializeComponent();
        }

        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MYdocvier newWindow = new MYdocvier ();
            newWindow.Show();
        }

        private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
    }
}