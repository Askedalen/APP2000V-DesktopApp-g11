using APP2000V_DesktopApp_g11.Assets;
using APP2000V_DesktopApp_g11.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace APP2000V_DesktopApp_g11.Views
{
    public partial class Projects : AnimatedUserControl
    {
        private Persistence Db = new Persistence();
        private DesktopGUI AppWindow;
        public Projects() : base()
        {
            InitializeComponent();
            AppWindow = App.Current.MainWindow as DesktopGUI;
            DisplayAllProjects();
        }

        private void DisplayAllProjects()
        {
            List<Project> allProjects = Db.GetAllCurrentProjects();
            if (allProjects != null && allProjects.Count != 0)
            {
                allProjects.ForEach(DisplaySingleProject);
            }
            else
            {
                TextBlock noProjectsMessage = new TextBlock();
                noProjectsMessage.Text = "No projects yet!";
                noProjectsMessage.Style = AppWindow.FindResource("NoProjectsMessage") as Style;
                ProjectsDisplay.Orientation = Orientation.Vertical;
                ProjectsDisplay.Children.Add(noProjectsMessage);
            }
        }

        private void DisplaySingleProject(Project p)
        {
            StackPanel projectPanel = new StackPanel();

            TextBlock headline = new TextBlock
            {
                Text = p.ProjectName,
                Style = AppWindow.FindResource("ProjectHeadline") as Style
            };
            projectPanel.Children.Add(headline);

            TextBlock participantCount = new TextBlock
            {
                Text = p.ProjectParticipants.Count + " participants", // Midlertidig antall deltakere
                Style = AppWindow.FindResource("ProjectParticipantsCount") as Style
            };
            projectPanel.Children.Add(participantCount);

            TextBlock description = new TextBlock
            {
                Text = p.ProjectDescription,
                Style = AppWindow.FindResource("ProjectShortDescription") as Style
            };
            projectPanel.Children.Add(description);

            StackPanel tasksPanel = new StackPanel();

            List<PTask> taskList = Db.GetBacklog(p.ProjectId);
            taskList.ForEach(t =>
            {
                TextBlock taskInScroll = new TextBlock();
                taskInScroll.Text = t.TaskName;
                taskInScroll.Style = AppWindow.FindResource("TaskInScrollViewer") as Style;
                tasksPanel.Children.Add(taskInScroll);
            });

            ScrollViewer tasksScroll = new ScrollViewer
            {
                Content = tasksPanel,
                Style = AppWindow.FindResource("TasksScrollViewer") as Style
            };

            projectPanel.Children.Add(tasksScroll);
            projectPanel.Style = AppWindow.FindResource("ProjectPanel") as Style;

            ProjectButton projectButton = new ProjectButton(p)
            {
                Style = AppWindow.FindResource("ProjectButton") as Style,
                Content = projectPanel
            };
            projectButton.Click += new RoutedEventHandler(ProjectButton_Click);
            ProjectsDisplay.Children.Add(projectButton);
        }

        private void ProjectButton_Click(object sender, RoutedEventArgs e)
        {
            ProjectButton btn = sender as ProjectButton;
            SwitchContent(AppWindow.Projects = new ProjectPage(btn.ProjectId));
        }

        private void NewProjectBtn_Click(object sender, RoutedEventArgs e)
        {
            SwitchContent(AppWindow.Projects = new CreateProject());
        }
    }

}
