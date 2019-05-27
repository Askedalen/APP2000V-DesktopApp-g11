using APP2000V_DesktopApp_g11.Models;
using APP2000V_DesktopApp_g11.Views;
using MySql.Data.MySqlClient;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;

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

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameInput.Text;
            string password = PasswordInput.Password;

            using (WorkflowContext context = new WorkflowContext())
            {
                try
                {
                    User user = context.Users
                                       .Where(u => u.Username == username
                                                && u.Role == 0)
                                       .FirstOrDefault();
                    if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
                    {
                        DesktopGUI gui = App.Current.MainWindow as DesktopGUI;
                        gui.OpenWindow();
                        this.Close();
                    }
                    else
                    {
                        ErrorMessage.Text = "Wrong username/password";
                    }

                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                }
            }
        }
    }
}
