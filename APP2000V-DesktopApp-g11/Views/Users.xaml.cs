using APP2000V_DesktopApp_g11.Assets;
using APP2000V_DesktopApp_g11.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace APP2000V_DesktopApp_g11.Views
{
    /// <summary>
    /// Interaction logic for Employees.xaml
    /// </summary>
    public partial class Employees : AnimatedUserControl
    {
        private Persistence Db = new Persistence();
        private DesktopGUI AppWindow;
        public Employees() : base()
        {
            InitializeComponent();
            AppWindow = App.Current.MainWindow as DesktopGUI;
            PrintUsers();
        }

        private void PrintUsers()
        {
            EmployeesDisplay.Children.Clear();
            List<User> users = Db.GetAllEmployees();
            users.ForEach(u =>
            {
                TextBlock name = new TextBlock
                {
                    Text = u.FirstName + " " + u.LastName,
                    Style = AppWindow.FindResource("UserListFullName") as Style,
                };
                UserButton userButton = new UserButton(u)
                {
                    Style = AppWindow.FindResource("UserListButton") as Style,
                    Content = name
                };
                userButton.Click += new RoutedEventHandler(UserButton_Click);
                EmployeesDisplay.Children.Add(userButton);
            });
        }

        private void UserButton_Click(object sender, RoutedEventArgs e)
        {
            UserButton userButton = sender as UserButton;
            SwitchContent(AppWindow.Employees = new UserPage(userButton.UserId));
        }

        private void NewEmpBtn_Click(object sender, RoutedEventArgs e)
        {
            SwitchContent(AppWindow.Employees = new EditUsers());
        }
    }
}
