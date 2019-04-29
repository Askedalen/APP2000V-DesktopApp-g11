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
    /// Interaction logic for ProjectReport.xaml
    /// </summary>
    public partial class ProjectReport : AnimatedUserControl
    {
        ProjectController Pc = new ProjectController();
        Persistence Db = new Persistence();
        Report CurrentReport;
        DesktopGUI AppWindow;
        public ProjectReport(int pid) : base()
        {
            InitializeComponent();

            AppWindow = App.Current.MainWindow as DesktopGUI;
            CurrentReport = Db.GetReport(pid);

            ReportGrid.DataContext = CurrentReport;
            CommentBlock.Text = CurrentReport.Comment;
        }

        private void ApproveProjectBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Pc.ApproveProject(CurrentReport.ProjectId.Value) == 0)
            {
                SwitchContent(new Dashboard());
            }
        }

        private void DisapproveProjectBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Pc.DisapproveProject(CurrentReport.ProjectId.Value) == 0)
            {
                SwitchContent(new Dashboard());
            }
        }

        private void ViewProjectBtn_Click(object sender, RoutedEventArgs e)
        {
            SwitchContent(new ProjectPage(CurrentReport.Project.ProjectId));
        }
    }
}
