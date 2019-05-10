using APP2000V_DesktopApp_g11.Models;
using APP2000V_DesktopApp_g11.Views;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq;
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

        DesktopGUI DesktopGUI = new DesktopGUI();

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
                        DesktopGUI.OpenWindow();
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
