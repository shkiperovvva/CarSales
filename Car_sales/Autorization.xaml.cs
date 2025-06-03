using System;
using System.Collections.Generic;
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
    public partial class Autorization : Window
    {
        private readonly string ConnectionString = "Host=172.20.7.53;Port=5432;Database=db2992_23;Username=st2992;Password=pwd2992";
        public Autorization()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string password = PasswordTextBox.Password;

            try
            {
                using (var connection = new NpgsqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (var cmd = new NpgsqlCommand("SELECT car.autorization_user(@email, @password)", connection))
                    {
                        cmd.Parameters.AddWithValue("email", email);
                        cmd.Parameters.AddWithValue("password", password);

                        var result = cmd.ExecuteScalar();

                        if (result != DBNull.Value && result != null) {
                            int userRole = Convert.ToInt32(result);

                            switch (userRole) 
                            {
                                case 1: new MainWindow().Show();
                                    break;
                                case 2: new MainWindowEmployee().Show();
                                    break;
                                case 3: new MainWindowAdmin().Show();
                                    break;
                                default:
                                    MessageBox.Show("Неизвестная роль пользователя");
                                    break;
                            }
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Неверный email или пароль");
                        }
                    }
                }
            }
            catch (Exception ex) {
                MessageBox.Show($"Ошибка при авторизации: {ex.Message}");
            }
        }
    }
}
