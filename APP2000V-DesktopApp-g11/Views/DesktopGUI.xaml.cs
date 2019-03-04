using APP2000V_DesktopApp_g11.Assets;
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
            ContentArea.Content = new Dashboard(this);
            DashboardBtn.Background = Brushes.Gray;
        }
        private void NavBtn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            ClearBtnColor();
            btn.Background = Brushes.Gray;
            AnimatedUserControl currentContent = ContentArea.Content as AnimatedUserControl;
            if (sender.Equals(DashboardBtn)) currentContent.SwitchContent(new Dashboard(this));
            else if (sender.Equals(ProjectsBtn)) currentContent.SwitchContent(new Projects(this));
            else if (sender.Equals(EmployeesBtn)) currentContent.SwitchContent(new Employees(this));
            //else if (sender.Equals(ArchiveBtn)) ContentArea.Content = new Archive();
            
        }

        private void ClearBtnColor()
        {
            DashboardBtn.Background = Brushes.LightGray;
            ProjectsBtn.Background = Brushes.LightGray;
            EmployeesBtn.Background = Brushes.LightGray;
            ArchiveBtn.Background = Brushes.LightGray;
        }
    }
}
