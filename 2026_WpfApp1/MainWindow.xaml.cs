using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace _2026_WpfApp1
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<OrderItem> _orderItems = new();
        private ObservableCollection<Drink> _menu = new();
        private Drink? _selectedDrink;

        public MainWindow()
        {
            InitializeComponent();

            // 建立菜單（可擴充）
            _menu.Add(new Drink("紅茶", 30));
            _menu.Add(new Drink("珍珠奶茶", 60));
            _menu.Add(new Drink("綠茶", 25));
            _menu.Add(new Drink("咖啡", 50));

            // 使用 Label 顯示菜單，並綁定事件（不使用 ComboBox）
            foreach (var d in _menu)
            {
                var lbl = new Label
                {
                    Content = $"{d.Name} ({d.Price} 元)",
                    Tag = d,
                    Margin = new Thickness(2),
                    Padding = new Thickness(4),
                    Background = Brushes.Transparent
                };

                // 點擊時選取（MouseLeftButtonUp）
                lbl.MouseLeftButtonUp += MenuLabel_MouseLeftButtonUp;
                MenuPanel.Children.Add(lbl);
            }

            OrderListBox.ItemsSource = _orderItems;
            OrderListBox.DisplayMemberPath = "Display";
            UpdateTotal();
        }

        private void MenuLabel_MouseLeftButtonUp(object? sender, MouseButtonEventArgs e)
        {
            var lbl = sender as Label;
            if (lbl == null) return;

            // 取得對應 Drink（存在 Tag）
            _selectedDrink = lbl.Tag as Drink;

            // 視覺上標示選取（簡單處理：清除其他背景並標示此項）
            foreach (var child in MenuPanel.Children.OfType<Label>())
            {
                child.Background = Brushes.Transparent;
                child.Foreground = Brushes.Black;
            }

            lbl.Background = Brushes.Orange;
            lbl.Foreground = Brushes.White;
        }

        private void QtyTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var targetTextBox = sender as TextBox;
            if (targetTextBox == null) return;

            if (!int.TryParse(targetTextBox.Text, out int qty) || qty <= 0)
            {
                targetTextBox.Text = "1";
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // 改為使用 _selectedDrink（由 Label 選取）
            if (_selectedDrink == null)
            {
                MessageBox.Show("請先選擇一項飲料。", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(QtyTextBox.Text, out int qty) || qty <= 0)
            {
                qty = 1;
            }

            var existing = _orderItems.FirstOrDefault(i => i.Drink.Name == _selectedDrink.Name);
            if (existing != null)
            {
                existing.Quantity += qty;
            }
            else
            {
                _orderItems.Add(new OrderItem(_selectedDrink, qty));
            }

            UpdateTotal();
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = OrderListBox.SelectedItem as OrderItem;
            if (selected != null)
            {
                _orderItems.Remove(selected);
                UpdateTotal();
            }
        }

        private void CheckoutButton_Click(object sender, RoutedEventArgs e)
        {
            int total = _orderItems.Sum(i => i.Subtotal);
            MessageBox.Show($"結帳金額：{total} 元", "結帳", MessageBoxButton.OK, MessageBoxImage.Information);
            _orderItems.Clear();
            UpdateTotal();
        }

        private void UpdateTotal()
        {
            int total = _orderItems.Sum(i => i.Subtotal);
            TotalTextBlock.Text = $"{total} 元";
        }

        // 保留：示範程式動態新增控制項的用法
        private void AddTextBlockProgrammatically()
        {
            var tb = new TextBlock { Text = "程式新增的文字", Margin = new Thickness(6) };
            Grid.SetRow(tb, 1);
            Grid.SetColumn(tb, 1);
            // MainGrid 若在 XAML 有命名才可用；此方法示範用途
        }
    }

    public class Drink
    {
        public string Name { get; }
        public int Price { get; } // 單位：元

        public Drink(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public override string ToString() => $"{Name} ({Price} 元)";
    }

    public class OrderItem : INotifyPropertyChanged
    {
        public Drink Drink { get; }
        private int _quantity;

        public int Quantity
        {
            get => _quantity;
            set
            {
                if (_quantity == value) return;
                _quantity = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Quantity)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Subtotal)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Display)));
            }
        }

        public int Subtotal => Drink.Price * Quantity;

        public string Display => $"{Drink.Name} x{Quantity} = {Subtotal} 元";

        public OrderItem(Drink drink, int quantity)
        {
            Drink = drink;
            _quantity = quantity;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
