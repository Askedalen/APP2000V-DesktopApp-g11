using System;
using APP2000V_DesktopApp_g11.Assets;
using APP2000V_DesktopApp_g11.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using APP2000V_DesktopApp_g11.Controllers;

namespace APP2000V_DesktopApp_g11.Views
{
    /// <summary>
    /// Interaction logic for EditUsers.xaml
    /// </summary>
    public partial class EditUsers : AnimatedUserControl
    {
        EmployeeController Ec = new EmployeeController();
        Persistence Db = new Persistence();

        public EditUsers() : base()
        {
            InitializeComponent();
            UpdateTable();
        }

        public EditUsers(User user) : this()
        {
            SetInput(user);
        }

        private void UpdateTable()
        {
            UserDataGrid.ItemsSource = Db.GetAllEmployees();
        }

        private User GetInput()
        {
            return new User
            {
                Username = UserUsernameInput.Text,
                Password = UserPasswordInput.Password,
                FirstName = UserFNameInput.Text,
                LastName = UserLNameInput.Text,
                PhoneNumber = UserPhoneInput.Text,
                Email = UserEmailInput.Text,
                About = UserAboutInput.Text
            };
        }

        private void SetInput(User user)
        {
            InputPanel.DataContext = user;
        }

        private void ClearInput()
        {
            UserUsernameInput.Text = "";
            UserPasswordInput.Password = "";
            UserFNameInput.Text = "";
            UserLNameInput.Text = "";
            UserEmailInput.Text = "";
            UserPhoneInput.Text = "";
            UserAboutInput.Text = "";
        }

        private void CreateUserBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Ec.CreateUser(GetInput()))
            {
                UpdateTable();
            }
        }

        private void UpdateUserBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Ec.UpdateUser(GetInput()))
            {
                UpdateTable();
            }
        }

        private void DeleteUserBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult answer = MessageBox.Show("Are you sure you want to delete this user?", "Delete user", MessageBoxButton.YesNo);
            if (answer == MessageBoxResult.Yes)
            {
                if (Ec.DeleteUser(GetInput()))
                {
                    UpdateTable();
                }
            }
        }

        private void ClearTextBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearInput();
        }

        private void UserDataGridRow_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            User user = row.Item as User;
            SetInput(user);
        }

        
    }
}
