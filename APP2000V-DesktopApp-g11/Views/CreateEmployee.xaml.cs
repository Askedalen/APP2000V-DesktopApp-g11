using System;
using APP2000V_DesktopApp_g11.Assets;
using APP2000V_DesktopApp_g11.Models;
using APP2000V_DesktopApp_g11.Models.Database;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace APP2000V_DesktopApp_g11.Views
{
    /// <summary>
    /// Interaction logic for CreateEmployee.xaml
    /// </summary>
    public partial class CreateEmployee : AnimatedUserControl
    {
        Persistence Db = new Persistence();
        public CreateEmployee(DesktopGUI gui) : base(gui)
        {
            InitializeComponent();
        }

        private void RegisterUserBtn_Click(object sender, RoutedEventArgs e)
        {
            // Parsing year, month and day for dates
            // string[] startParts = ProjectStartInput.Text.Split('-');
            //  int sy = Int32.Parse(startParts[0]);
            //  int sm = Int32.Parse(startParts[1]);
            //   int sd = Int32.Parse(startParts[2]);
            //   string[] deadlineParts = ProjectStartInput.Text.Split('-');
            //   int dy = Int32.Parse(deadlineParts[0]);
            //   int dm = Int32.Parse(deadlineParts[1]);
            //  int dd = Int32.Parse(deadlineParts[2]);

            // Uses Persistence object to insert the project into the database
            // Returns 0 if operation succeeds
            int result = Db.CreateUser(new Employee
            {
                Username = EmployeeUsernameInput.Text,
                FirstName = EmployeeFNameInput.Text,
                LastName = EmployeeLNameInput.Text,
                PhoneNumber = EmployeePhoneInput.Text,
                Email = EmployeeEmailInput.Text,

            });

            if (result == 0)
            {
                ConfirmationBox.Text = "Project is registered!";
            }
            else
            {
                ConfirmationBox.Text = "Something went wrong!";
            }

        }

        private void CreateUserBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateUserBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteUserBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
