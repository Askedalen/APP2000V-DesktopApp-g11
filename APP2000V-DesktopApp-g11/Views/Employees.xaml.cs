using APP2000V_DesktopApp_g11.Assets;
using APP2000V_DesktopApp_g11.Models;
using APP2000V_DesktopApp_g11.Models.Database;
using System;
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
            SwitchContent(new CreateEmployee(AppWindow));
        }
    }
}
