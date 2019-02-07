using APP2000V_DesktopApp_g11.Models.Database;
using APP2000V_DesktopApp_g11.Views;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace APP2000V_DesktopApp_g11
{
    /// <summary>
    /// Interaction logic for AdminLogin.xaml
    /// </summary>
    public partial class AdminLogin : Window
    {
        public AdminLogin()
        {
            InitializeComponent();
        }

        DesktopGUI desktopGUI = new DesktopGUI();

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameInput.Text;
            string password = PasswordInput.Text;
            User userInput = new User() { Username = username, Password = password };
            string connectionString = "server=localhost;port=3306;database=app2000v;uid=root;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                try
                {
                    using (MySqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM users WHERE Username=@username AND Password=@password AND UserType=0";
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var ii = reader.FieldCount;
                                for (int i = 0; i < ii; i++)
                                {
                                    if (reader[i] is DBNull)
                                        ErrorMessage.Text = "Username and password do not match.";
                                    else
                                    {
                                        desktopGUI.Show();
                                        this.Hide();
                                    }
                                }
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    ErrorMessage.Text = "Username and password do not match.";
                    Console.WriteLine(ex.Message);
                }

                connection.Close();
            }
        }
    }
}
