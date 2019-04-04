using APP2000V_DesktopApp_g11.Assets;
using APP2000V_DesktopApp_g11.Controllers;
using APP2000V_DesktopApp_g11.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace APP2000V_DesktopApp_g11.Views
{
    /// <summary>
    /// Interaction logic for CreateProject.xaml
    /// </summary>
    public partial class CreateProject : AnimatedUserControl
    {
        Persistence Db = new Persistence();
        ProjectController Pc = new ProjectController();
        int ProjectId;
        DesktopGUI AppWindow;
        public CreateProject(DesktopGUI gui) : base(gui)
        {
            AppWindow = gui;
            InitializeComponent();
        }

        private void RegisterProjectBtn_Click(object sender, RoutedEventArgs e)
        {
            Project projectUpdate = new Project
            {
                ProjectName = ProjectNameInput.Text,
                ProjectDescription = ProjectDescInput.Text,
            };

            if (ProjectStartPicker.SelectedDate != null)
            {
                projectUpdate.ProjectStart = ProjectStartPicker.SelectedDate;
            }
            if (ProjectDeadlinePicker.SelectedDate != null)
            {
                projectUpdate.ProjectDeadline = ProjectDeadlinePicker.SelectedDate;
            }

            int pid = Pc.CreateProject(projectUpdate);
            if (pid != -1)
            {
                ProjectId = pid;
                Console.WriteLine("Project created!");
                ConfirmationBox.Text = "Project was created!";
                GoToProjectBtn.Visibility = Visibility.Visible;
                GoToProjectBtn.Focus();

            }
        }

        private void GoToProjectBtn_Click(object sender, RoutedEventArgs e)
        {
            SwitchContent(new ProjectPage(ProjectId, AppWindow));
        }
    }
}
