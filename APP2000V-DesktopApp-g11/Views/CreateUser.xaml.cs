using System;
using APP2000V_DesktopApp_g11.Assets;
using APP2000V_DesktopApp_g11.Models;
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
using MySql.Data.MySqlClient;

namespace APP2000V_DesktopApp_g11.Views
{
    /// <summary>
    /// Interaction logic for CreateUser.xaml
    /// </summary>
    public partial class CreateUser : AnimatedUserControl
    {
        Persistence Db = new Persistence();
        public CreateUser() : base()
        {
            InitializeComponent();

            // string connectionString = "server=localhost;port=3306;database=app2000v;uid=root;";

            //MySqlConnection connection = new MySqlConnection(connectionString);

            //MySqlCommand cmd = new MySqlCommand("select * from app2000v", connection);

        }


        private void CreateUserBtn_Click(object sender, RoutedEventArgs e)
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
            int result = Db.CreateUser(new User
            {
                Username = UserUsernameInput.Text,
                Password = UserPasswordInput.Password,
                FirstName = UserFNameInput.Text,
                LastName = UserLNameInput.Text,
                PhoneNumber = UserPhoneInput.Text,
                Email = UserEmailInput.Text,

            });

            if (result == 0)
            {
                ConfirmationBox.Text = "User is registered!";
            }
            else
            {
                ConfirmationBox.Text = "Something went wrong!";
            }

        }



        private void UpdateUserBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteUserBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
