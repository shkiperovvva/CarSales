using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
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
    /// <summary>
    /// Логика взаимодействия для MainWindowAdmin.xaml
    /// </summary>
    public partial class MainWindowAdmin : Window
    {
        private readonly string ConnectionString = "Host=172.20.7.53;Port=5432;Database=db2992_23;Username=st2992;Password=pwd2992";
        public MainWindowAdmin()
        {
            InitializeComponent();
            LoadBrands();
        }
        private void LoadBrands()
        {
            try
            {
                using (var connection = new NpgsqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (var cmd = new NpgsqlCommand("SELECT DISTINCT brand FROM car.brand ORDER BY brand", connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            var dt = new DataTable();
                            dt.Load(reader);
                            BrandComboBox.ItemsSource = dt.DefaultView;
                            BrandComboBox.DisplayMemberPath = "brand";
                            BrandComboBox.SelectedValuePath = "brand";
                        }
                    }
                }
            }
            catch (Exception ex) {
                MessageBox.Show($"Ошибка загрузки марок автомобилей: {ex.Message}");
            }
        }

        private void ViewEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            ViewEmployeeList viewEmployeeList = new ViewEmployeeList();
            viewEmployeeList.Show();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string brand = BrandComboBox.SelectedValue.ToString();
            DateTime startDate = StartDatePicker.SelectedDate.Value;
            DateTime endDate = EndDatePicker.SelectedDate.Value;
            try
            {
                using (var connection = new NpgsqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (var transaction  = connection.BeginTransaction())
                    {
                        string cursorName = "car_cursor";
                        using (var cmd = new NpgsqlCommand("CALL car.get_sold_cars_by_brand_proc(@brand_name, @startDate, @endDate, @ref)", connection))
                        {
                            cmd.Parameters.AddWithValue("brand_name", brand);
                            cmd.Parameters.AddWithValue("startDate", startDate);
                            cmd.Parameters.AddWithValue("endDate", endDate);
                            cmd.Parameters.Add(new NpgsqlParameter("ref", NpgsqlTypes.NpgsqlDbType.Refcursor)
                            {
                                Value = cursorName,
                            });
                            cmd.ExecuteNonQuery();
                            using (var fetchCmd = new NpgsqlCommand($"FETCH ALL FROM {cursorName};", connection, transaction))
                            {
                                using (var reader = fetchCmd.ExecuteReader())
                                {
                                    var dt = new DataTable();
                                    dt.Load(reader);
                                    CarsDataGrid.ItemsSource = dt.DefaultView;
                                }
                            }
                            transaction.Commit();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка выполнения запроса: {ex.Message}");
            }

            
        }
    }
}
