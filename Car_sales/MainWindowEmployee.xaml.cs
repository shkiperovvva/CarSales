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
    public partial class MainWindowEmployee : Window
    {
        private readonly string ConnectionString = "Host=172.20.7.53;Port=5432;Database=db2992_23;Username=st2992;Password=pwd2992";
        public MainWindowEmployee()
        {
            InitializeComponent();
        }
        private void LoadCar()
        {
            try
            {
                using (var connection = new NpgsqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (var cmd = new NpgsqlCommand("SELECT * FROM car.car_view", connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            var dt = new DataTable();
                            dt.Load(reader);
                            DataGrid.ItemsSource = dt.DefaultView;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void LoadUsers()
        {
            try
            {
                using (var connection = new NpgsqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (var cmd = new NpgsqlCommand("SELECT * FROM car.users_view", connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            var dt = new DataTable();
                            dt.Load(reader);
                            DataGrid.ItemsSource = dt.DefaultView;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void LoadBrand()
        {
            try
            {
                using (var connection = new NpgsqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (var cmd = new NpgsqlCommand("SELECT * FROM car.brand_view", connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            var dt = new DataTable();
                            dt.Load(reader);
                            DataGrid.ItemsSource = dt.DefaultView;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void ViewCarButton_Click(object sender, RoutedEventArgs e)
        {
            LoadCar();
        }

        private void ViewBrandButton_Click(object sender, RoutedEventArgs e)
        {
            LoadBrand();
        }

        private void ViewUsersButton_Click(object sender, RoutedEventArgs e)
        {
            LoadUsers();
        }

        private void ViewOrdersButton_Click(object sender, RoutedEventArgs e)
        {
            var viewOrdersWindow = new ViewOrdersWindow();
            viewOrdersWindow.Show();
        }
    }
}
