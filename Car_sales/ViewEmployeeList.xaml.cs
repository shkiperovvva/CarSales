using System;
using System.Data;
using System.Windows;
using Npgsql;

namespace Car_sales
{
    public partial class ViewEmployeeList : Window
    {
        private readonly string ConnectionString = "Host=172.20.7.53;Port=5432;Database=db2992_23;Username=st2992;Password=pwd2992";

        public ViewEmployeeList()
        {
            InitializeComponent();
            LoadEmployee();
        }

        private void LoadEmployee()
        {
            try
            {
                using (var connection = new NpgsqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (var cmd = new NpgsqlCommand("SELECT * FROM car.users_view", connection)) {
                        using (var reader = cmd.ExecuteReader())
                        {
                            var dt = new DataTable();
                            dt.Load(reader);
                            EmployeeDataGrid.ItemsSource = dt.DefaultView;
                        }
                    }  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeeWindow addEmployeeWindow = new AddEmployeeWindow();
            addEmployeeWindow.ShowDialog();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = EmployeeDataGrid.SelectedItem as System.Data.DataRowView;
            int userId = Convert.ToInt32(selectedRow["id_user"]);

            var result = MessageBox.Show("Вы действительно хотите удалить пользователя?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) {
                return;
            }
            try
            {
                using (var connection = new NpgsqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (var cmd = new NpgsqlCommand("SELECT car.delete_user(@user_id)", connection))
                    {
                        cmd.Parameters.AddWithValue("user_id", userId);
                        var deleteResult = cmd.ExecuteScalar();
                        if (deleteResult != null)
                        {
                            LoadEmployee();
                            MessageBox.Show("Сотрудник успешно удален!", "Успех");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = EmployeeDataGrid.SelectedItem as System.Data.DataRowView;
            int userId = Convert.ToInt32(selectedRow["id_user"]);
            string currentEmail = selectedRow["email"].ToString();

            var updateEmailWindow = new UpdateEmailWindow(currentEmail)
            {
                Owner = this
            };
            if (updateEmailWindow.ShowDialog() != true)
            {
                return;
            }

            try
            {
                using (var connection = new NpgsqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (var cmd = new NpgsqlCommand("SELECT car.update_email_user(@id_user, @email)", connection))
                    {
                        cmd.Parameters.AddWithValue("id_user", userId);
                        cmd.Parameters.AddWithValue("email", updateEmailWindow.NewEmail);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Email успешно изменён");
                        LoadEmployee();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
    }
}
