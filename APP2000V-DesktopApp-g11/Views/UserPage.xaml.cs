using APP2000V_DesktopApp_g11.Assets;
using APP2000V_DesktopApp_g11.Controllers;
using APP2000V_DesktopApp_g11.Models;
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
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : AnimatedUserControl
    {
        DesktopGUI AppWindow;
        Persistence Db = new Persistence();
        User CurrentUser;
        public UserPage(int uid)
        {
            InitializeComponent();
            AppWindow = App.Current.MainWindow as DesktopGUI;
            CurrentUser = Db.GetSingleUser(uid);
            UserInfoGrid.DataContext = CurrentUser;
        }

        private void EditUserBtn_Click(object sender, RoutedEventArgs e)
        {
            EditUsers editPage = new EditUsers(CurrentUser);
            SwitchContent(AppWindow.Employees = editPage);
        }
    }
}
