using APP2000V_DesktopApp_g11.Models;
using APP2000V_DesktopApp_g11.Views;
using MySql.Data.MySqlClient;
using System;
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

        //Views.SplashScreen DesktopGUI = new Views.SplashScreen();

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
                                                && u.Password == password
                                                && u.Role == 0)
                                       .FirstOrDefault();
                    if (user != null)
                    {
                        Views.SplashScreen splashScreen = new Views.SplashScreen();
                        splashScreen.Show();
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
