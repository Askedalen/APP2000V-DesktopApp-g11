using APP2000V_DesktopApp_g11.Assets;
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
    /// Interaction logic for Archive.xaml
    /// </summary>
    public partial class Archive : AnimatedUserControl
    {
        DesktopGUI AppWindow;
        Persistence Db = new Persistence();
        public Archive() : base()
        {
            InitializeComponent();
            AppWindow = App.Current.MainWindow as DesktopGUI;
            PrintArchive();
        }

        private void PrintArchive()
        {
            ArchivePanel.Children.Clear();
            List<Project> projects = Db.GetArchive();
            projects.ForEach(p =>
            {
                TextBlock projectName = new TextBlock
                {
                    Text = p.ProjectName,
                    Style = AppWindow.FindResource("ArchivedProjectName") as Style
                };
                TextBlock projectDesc = new TextBlock
                {
                    Text = p.ProjectDescription,
                    Style = AppWindow.FindResource("ArchivedProjectDescription") as Style
                };
                ProjectButton viewProjectBtn = new ProjectButton(p)
                {
                    Content = "View project",
                    Style = AppWindow.FindResource("ArchivedProjectViewBtn") as Style
                };
                viewProjectBtn.Click += new RoutedEventHandler(ViewProjectBtn_Click);
                StackPanel projectPanel = new StackPanel
                {
                    Style = AppWindow.FindResource("ArchivedProjectPanel") as Style
                };
                projectPanel.Children.Add(projectName);
                projectPanel.Children.Add(projectDesc);
                projectPanel.Children.Add(viewProjectBtn);
                ArchivePanel.Children.Add(projectPanel);
            });
        }

        private void ViewProjectBtn_Click(object sender, RoutedEventArgs e)
        {
            ProjectButton btn = sender as ProjectButton;
            SwitchContent(AppWindow.Archive = new ProjectPage(btn.ProjectId));
        }
    }
}
