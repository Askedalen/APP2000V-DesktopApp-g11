using APP2000V_DesktopApp_g11.Models;
using APP2000V_DesktopApp_g11.Models.Database;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace APP2000V_DesktopApp_g11.Views
{
    /// <summary>
    /// Interaction logic for Projects.xaml
    /// </summary>
    public partial class Projects : UserControl
    {
        private Persistence Db = new Persistence();
        private ContentControl ContentArea;
        private DesktopGUI AppWindow;
        public Projects(DesktopGUI gui)
        {
            InitializeComponent();
            AppWindow = gui;
            ContentArea = gui.ContentArea;
            DisplayAllProjects();
        }

        private void DisplayAllProjects()
        {
            List<Project> allProjects = Db.GetAllProjects();
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

        private void DisplaySingleProject(Project obj)
        {
            ProjectButton projectButton = new ProjectButton(obj);
            StackPanel projectPanel = new StackPanel();
            TextBlock headline = new TextBlock();
            TextBlock participantCount = new TextBlock();
            TextBlock description = new TextBlock();
            ScrollViewer tasksScroll = new ScrollViewer();
            StackPanel tasksPanel = new StackPanel();

            headline.Text = obj.ProjectName;
            headline.Style = AppWindow.FindResource("ProjectHeadline") as Style;
            projectPanel.Children.Add(headline);

            participantCount.Text = "5" + " participants"; // Midlertidig antall deltakere
            participantCount.Style = AppWindow.FindResource("ProjectParticipantsCount") as Style;
            projectPanel.Children.Add(participantCount);

            description.Text = obj.ProjectDescription;
            description.Style = AppWindow.FindResource("ProjectShortDescription") as Style;
            projectPanel.Children.Add(description);

            List<Task> taskList = new List<Task>();
            for (int i = 0; i < 10; i++) taskList.Add(new Task() { TaskName = "Dette er test nummer"+(i+1) }); // Midlertidig liste over tasks
            taskList.ForEach(t =>
            {
                TextBlock taskInScroll = new TextBlock();
                taskInScroll.Text = t.TaskName;
                taskInScroll.Style = AppWindow.FindResource("TaskInScrollViewer") as Style;
                tasksPanel.Children.Add(taskInScroll);
            });

            tasksScroll.Content = tasksPanel;
            tasksScroll.Style = AppWindow.FindResource("TasksScrollViewer") as Style;

            projectPanel.Children.Add(tasksScroll);
            projectPanel.Style = AppWindow.FindResource("ProjectPanel") as Style;

            projectButton.Style = AppWindow.FindResource("ProjectButton") as Style;
            projectButton.Content = projectPanel;
            projectButton.Click += new RoutedEventHandler(ProjectButton_Click);
            ProjectsDisplay.Children.Add(projectButton);
        }

        private void ProjectButton_Click(object sender, RoutedEventArgs e)
        {
            ProjectButton btn = sender as ProjectButton;
            this.ContentArea.Content = new ProjectPage(btn.ProjectID);
        }

        private void NewProjectBtn_Click(object sender, RoutedEventArgs e)
        {
            this.ContentArea.Content = new CreateProject();
        }
    }

    public class ProjectButton : Button
    {
        public int ProjectID { get; set; }
        public ProjectButton(Project p) : base()
        {
            ProjectID = p.ProjectID;
        }
    }
}
