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
using System.Windows.Shapes;

namespace APP2000V_DesktopApp_g11.Views
{
    /// <summary>
    /// Interaction logic for DesktopGUI.xaml
    /// </summary>
    public partial class DesktopGUI : Window
    {
        public DesktopGUI()
        {
            InitializeComponent();
            ContentArea.Content = new Dashboard();
        }
        private void ProjectsBtn_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new Projects(ContentArea);
        }

        private void EmployeesBtn_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new Employees();
        }
    }
}
