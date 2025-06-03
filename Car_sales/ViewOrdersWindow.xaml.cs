using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;
using Npgsql;

namespace Car_sales
{
    public partial class ViewOrdersWindow : Window
    {
        private readonly string ConnectionString = "Host=172.20.7.53;Port=5432;Database=db2992_23;Username=st2992;Password=pwd2992";
        public ViewOrdersWindow()
        {
            InitializeComponent();
            LoadOrder();
        }
        private void LoadOrder()
        {
            try
            {
                using (var connection = new NpgsqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (var cmd = new NpgsqlCommand("SELECT * FROM car.orders_view", connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            var dt = new DataTable();
                            dt.Load(reader);
                            OrderDataGrid.ItemsSource = dt.DefaultView;
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show($"Ошибка: {ex.Message}"); }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = OrderDataGrid.SelectedItem as System.Data.DataRowView;
            int idOrder = Convert.ToInt32(selectedRow["id_order"]);
            var result = MessageBox.Show("Вы действительно хотите удалить выбранный заказ?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) { return; }

            try
            {
                using (var connection = new NpgsqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (var cmd = new NpgsqlCommand("SELECT car.delete_order(@p_id_order)", connection))
                    {
                        cmd.Parameters.AddWithValue("p_id_order", idOrder);
                        var deleteResult = cmd.ExecuteScalar();
                        if (deleteResult != null) { 
                            LoadOrder();
                            MessageBox.Show("Заказ успешно удален!");
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show($"Ошибка: {ex.Message}"); }
        }
    }
}
