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
                    FontSize = 28,
                    Margin = new Thickness(15, 30, 15, 30),
                };
                StackPanel userPanel = new StackPanel
                {
                    Background = new SolidColorBrush(Colors.White),
                };
                userPanel.Children.Add(name);
                UserButton userButton = new UserButton(u)
                {
                    Width = 300,
                    Height = 300,
                    Margin = new Thickness(30, 30, 30, 0),
                    Content = userPanel
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
            SwitchContent(AppWindow.Employees = new CreateUser());
        }
    }
}
