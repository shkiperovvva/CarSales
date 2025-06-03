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
    /// <summary>
    /// Логика взаимодействия для AddEmployeeWindow.xaml
    /// </summary>
    public partial class AddEmployeeWindow : Window
    {
        private readonly string ConnectionString = "Host=172.20.7.53;Port=5432;Database=db2992_23;Username=st2992;Password=pwd2992";
        public AddEmployeeWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text;
            string lastname = LastnameTextBox.Text;
            string patronymic = PatronymicTextBox.Text;
            string email = EmailTextBox.Text;
            string password = PasswordTextBox.Password;

            try
            {
                using (var connection = new NpgsqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (var cmd = new NpgsqlCommand("SELECT car.add_employee(@name, @lastname, @patronymic, @email, @password)", connection))
                    {
                        cmd.Parameters.AddWithValue("name", name);
                        cmd.Parameters.AddWithValue("lastname", lastname);
                        cmd.Parameters.AddWithValue("patronymic", patronymic);
                        cmd.Parameters.AddWithValue("email", email);
                        cmd.Parameters.AddWithValue("password", password);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show($"Сотрудник {name} {lastname} успешно добавлен");
                this.Close();
            }
            catch (Exception ex) { 
                MessageBox.Show($"Ошибка добавления сотрудника: {ex.Message}");
            }
        }
    }
}
