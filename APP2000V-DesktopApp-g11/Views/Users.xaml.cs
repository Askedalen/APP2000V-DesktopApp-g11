using APP2000V_DesktopApp_g11.Assets;
using APP2000V_DesktopApp_g11.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace APP2000V_DesktopApp_g11.Views
{
    /// <summary>
    /// Interaction logic for Employees.xaml
    /// </summary>
    public partial class Employees : AnimatedUserControl
    {
        private Persistence Db = new Persistence();
        private DesktopGUI AppWindow;
        public Employees(DesktopGUI gui) : base(gui)
        {
            InitializeComponent();
            InitializeComponent();
            AppWindow = gui;
           
        }

        private void NewEmpBtn_Click(object sender, RoutedEventArgs e)
        {
            SwitchContent(new CreateUser(AppWindow));
        }
    }
}
