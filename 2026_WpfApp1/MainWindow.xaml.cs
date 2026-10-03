using System.Windows;
using System.Windows.Controls;

namespace _2026_WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<string, int> drinks = new Dictionary<string, int>()
        {
            {"紅茶大杯", 60 },
            {"紅茶小杯", 40 },
            {"綠茶大杯", 60 },
            {"綠茶小杯", 40 },
            {"可樂大杯", 50 },
            {"可樂小杯", 30 }
        };

        Dictionary<string, int> orders = new Dictionary<string, int>();
        string resultMessage = "";
        string typeMessage = "內用";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            orders.Clear();
            resultMessage = "";

            double total = 0.0;
            string discountMessage = "沒有折扣";
            int index = 1;
            double sellPrice = 0.0;

            // 檢視飲料選單內，把正確的飲料訂單品項加入orders內
            for (int i = 0; i < DrinkMenuStackPanel.Children.Count; i++)
            {
                var sp = DrinkMenuStackPanel.Children[i] as StackPanel;
                var cb = sp.Children[0] as CheckBox;
                var sl = sp.Children[2] as Slider;

                int quantity = (int)sl.Value;
                if (cb.IsChecked == true && quantity > 0)
                {
                    string drinkName = cb.Content.ToString();
                    int price = drinks[drinkName];
                    orders.Add(drinkName, quantity);
                }
            }

            // 檢視orders，把所有訂單細項內容計算出細項總和
            resultMessage += $"訂購方式：{typeMessage}，訂購清單如下：\n";
            foreach (var item in orders)
            {
                string drinkName = item.Key;
                int price = drinks[drinkName];
                int quantity = item.Value;

                int subTotal = price * quantity;
                total += subTotal;
                resultMessage += $"{index}. {drinkName}：{price}元 X {quantity}杯 = {subTotal}元\n";
                index++;
            }

            if (total >= 500)
            {
                discountMessage = "打8折";
                sellPrice = total * 0.8;
            }
            else if (total >= 300)
            {
                discountMessage = "打85折";
                sellPrice = total * 0.85;
            }
            else if (total >= 200)
            {
                discountMessage = "打9折";
                sellPrice = total * 0.9;
            }
            else
            {
                sellPrice = total;
            }
            resultMessage += $"總價{total}元，{discountMessage}，售價為：{sellPrice}元\n";
            ResultTextBlock.Text = resultMessage;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var rb = sender as RadioButton;
            typeMessage = rb.Content.ToString();
        }
    }
}