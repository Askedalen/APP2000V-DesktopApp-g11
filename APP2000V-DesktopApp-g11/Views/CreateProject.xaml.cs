using APP2000V_DesktopApp_g11.Assets;
using APP2000V_DesktopApp_g11.Controllers;
using APP2000V_DesktopApp_g11.Models;
using System;
using System.Collections.Generic;
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
        public CreateProject() : base()
        {
            InitializeComponent();

            List<User> users = Db.GetAllEmployees();
            users.Insert(0, new User
            {
                UserId = -1,
                FirstName = "No manager",
                LastName = "",
            });
            ChooseProjectManager.ItemsSource = users;
        }

        private void RegisterProjectBtn_Click(object sender, RoutedEventArgs e)
        {
            Project newProject = new Project
            {
                ProjectName = ProjectNameInput.Text,
                ProjectDescription = ProjectDescInput.Text,
            };

            if (ProjectStartPicker.SelectedDate != null)
            {
                newProject.ProjectStart = ProjectStartPicker.SelectedDate;
            }
            if (ProjectDeadlinePicker.SelectedDate != null)
            {
                newProject.ProjectDeadline = ProjectDeadlinePicker.SelectedDate;
            }
            if (ChooseProjectManager.SelectedItem != null)
            {
                User chosenManager = ChooseProjectManager.SelectedItem as User;
                if (chosenManager.UserId >= 0)
                {
                    newProject.ProjectManager = chosenManager.UserId;
                }
               
            }

            int pid = Pc.CreateProject(newProject);
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
            DesktopGUI gui = App.Current.MainWindow as DesktopGUI;
            SwitchContent(gui.Projects = new ProjectPage(ProjectId));
        }
    }
}
