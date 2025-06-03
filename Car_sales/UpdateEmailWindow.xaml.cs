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

namespace Car_sales
{
    public partial class UpdateEmailWindow : Window
    {
        public string NewEmail { get; set; }
        public UpdateEmailWindow(string currentEmail)
        {
            InitializeComponent();
            UpdateEmailTextBox.Focus();
            UpdateEmailTextBox.Text = currentEmail;
            UpdateEmailTextBox.SelectAll();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            NewEmail = UpdateEmailTextBox.Text;
            DialogResult = true;
        }
    }
}
