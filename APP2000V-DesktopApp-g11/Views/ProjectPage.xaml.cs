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
    /// Interaction logic for ProjectPage.xaml
    /// </summary>
    public partial class ProjectPage : UserControl
    {
        Persistence Db = new Persistence();
        Project CurrentProject;
        public ProjectPage(int projectID)
        {
            InitializeComponent();
            Console.WriteLine("Project: " + projectID);
            CurrentProject = Db.GetSingleProject(projectID);
            DisplayProjectName.Text = CurrentProject.ProjectName;
        }

        private void CreateTaskBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CreateTaskTb_GotFocus(object sender, RoutedEventArgs e)
        {
            CreateTaskBtn.Visibility = Visibility.Visible;
            CreateTaskTb.Text = "";
            CreateTaskTb.Foreground = new SolidColorBrush(Colors.Black);
        }
    }
}
