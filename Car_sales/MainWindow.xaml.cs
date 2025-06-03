using System;
using System.Data;
using System.Windows;
using Npgsql;

namespace Car_sales
{
    public partial class MainWindow : Window
    {
        private readonly string ConnectionString = "Host=172.20.7.53;Port=5432;Database=db2992_23;Username=st2992;Password=pwd2992";

        private int currentUserId = -1;

        public MainWindow()
        {
            InitializeComponent();
            LoadEmails();
            LoadCars();
        }

        private void LoadEmails()
        {
            try
            {
                using var connection = new NpgsqlConnection(ConnectionString);
                connection.Open();

                using var cmd = new NpgsqlCommand("SELECT id_user, email FROM car.users ORDER BY email", connection);
                using var reader = cmd.ExecuteReader();

                var dt = new DataTable();
                dt.Load(reader);

                EmailComboBox.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки email: {ex.Message}");
            }
        }

        private void LoadCars()
        {
            try
            {
                using var connection = new NpgsqlConnection(ConnectionString);
                connection.Open();

                using var cmd = new NpgsqlCommand("SELECT c.id_car, c.id_brand, b.brand, b.model FROM car.car c JOIN car.brand b on c.id_brand = b.id_brand", connection);
                using var reader = cmd.ExecuteReader();

                var dt = new DataTable();
                dt.Load(reader);

                CarComboBox.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки автомобилей: {ex.Message}");
            }
        }

        private void EmailComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (EmailComboBox.SelectedValue != null)
            {
                currentUserId = (int)EmailComboBox.SelectedValue;
            }
            else
            {
                currentUserId = -1;
            }
        }

        private void PlaceOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentUserId == -1)
            {
                MessageBox.Show("Пожалуйста, выберите вашу электронную почту.");
                return;
            }

            if (CarComboBox.SelectedValue == null)
            {
                MessageBox.Show("Выберите автомобиль.");
                return;
            }

            if (!int.TryParse(QuantityTextBox.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Введите корректное количество (целое число больше 0).");
                return;
            }

            int carId = (int)CarComboBox.SelectedValue;

            try
            {
                using var connection = new NpgsqlConnection(ConnectionString);
                connection.Open();

                using var cmd = new NpgsqlCommand("SELECT car.create_order(@p_id_user, @p_id_car, @p_quantity)", connection);
                cmd.Parameters.AddWithValue("p_id_user", currentUserId);
                cmd.Parameters.AddWithValue("p_id_car", carId);
                cmd.Parameters.AddWithValue("p_quantity", quantity);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Заказ успешно оформлен!");
                QuantityTextBox.Clear();
                CarComboBox.SelectedIndex = -1;
                EmailComboBox.SelectedIndex = -1;
                currentUserId = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при оформлении заказа: {ex.Message}");
            }
        }
    }
}
