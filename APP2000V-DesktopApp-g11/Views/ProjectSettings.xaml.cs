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
    /// Interaction logic for ProjectSettings.xaml
    /// </summary>
    public partial class ProjectSettings : AnimatedUserControl
    {
        Persistence Db = new Persistence();
        ProjectController Pc = new ProjectController();
        Project CurrentProject;
        DesktopGUI AppWindow;

        public ProjectSettings(Project project) : base()
        {
            InitializeComponent();
            CurrentProject = project;
            AppWindow = App.Current.MainWindow as DesktopGUI;
            ProjectSettingsGrid.DataContext = project;
            List<User> projectMembers = Db.GetAllProjectMembers(project.ProjectId);
            ChooseProjectManager.ItemsSource = projectMembers;
            for (int i = 0; i < projectMembers.Count; i++)
            {
                if (project.ProjectManager == projectMembers.ElementAt(i).UserId)
                {
                    ChooseProjectManager.SelectedIndex = i;
                }
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            SwitchContent(new ProjectPage(CurrentProject.ProjectId));
        }

        private void UpdateProjectBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateProjectInformation();
        }

        private void UpdateProjectInformation()
        {
            Project projectUpdate = new Project
            {
                ProjectName = ProjectNameInput.Text,
                ProjectDescription = ProjectDescInput.Text,
                ProjectStart = ProjectStartPicker.SelectedDate,
                ProjectDeadline = ProjectDeadlinePicker.SelectedDate,
            };
            if (ChooseProjectManager.SelectedIndex >= 0)
            {
                User chosenManager = ChooseProjectManager.SelectedItem as User;
                projectUpdate.ProjectManager = chosenManager.UserId;
            }

            if (Pc.UpdateProject(projectUpdate, CurrentProject.ProjectId) == 0)
            {
                SwitchContent(new ProjectPage(CurrentProject.ProjectId));
            }
        }

        private void DeleteProjectBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult answer = MessageBox.Show("Are you sure you want to delete this project?", "Delete project", MessageBoxButton.YesNo);
            if (answer == MessageBoxResult.Yes)
            {
                if (Pc.DeleteProject(CurrentProject.ProjectId) == 0)
                {
                    SwitchContent(new Projects());
                }
            }
        }
    }
}
