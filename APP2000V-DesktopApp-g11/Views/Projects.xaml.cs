﻿using APP2000V_DesktopApp_g11.Assets;
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
        public Projects(DesktopGUI gui) : base(gui)
        {
            InitializeComponent();
            AppWindow = gui;
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
            StackPanel projectPanel = new StackPanel();

            TextBlock headline = new TextBlock();
            headline.Text = obj.ProjectName;
            headline.Style = AppWindow.FindResource("ProjectHeadline") as Style;
            projectPanel.Children.Add(headline);

            TextBlock participantCount = new TextBlock();
            participantCount.Text = "5" + " participants"; // Midlertidig antall deltakere
            participantCount.Style = AppWindow.FindResource("ProjectParticipantsCount") as Style;
            projectPanel.Children.Add(participantCount);

            TextBlock description = new TextBlock();
            description.Text = obj.ProjectDescription;
            description.Style = AppWindow.FindResource("ProjectShortDescription") as Style;
            projectPanel.Children.Add(description);

            StackPanel tasksPanel = new StackPanel();

            List<PTask> taskList = new List<PTask>();
            for (int i = 0; i < 10; i++) taskList.Add(new PTask() { TaskName = "Dette er test nummer"+(i+1) }); // Midlertidig liste over tasks
            taskList.ForEach(t =>
            {
                TextBlock taskInScroll = new TextBlock();
                taskInScroll.Text = t.TaskName;
                taskInScroll.Style = AppWindow.FindResource("TaskInScrollViewer") as Style;
                tasksPanel.Children.Add(taskInScroll);
            });

            ScrollViewer tasksScroll = new ScrollViewer();
            tasksScroll.Content = tasksPanel;
            tasksScroll.Style = AppWindow.FindResource("TasksScrollViewer") as Style;

            projectPanel.Children.Add(tasksScroll);
            projectPanel.Style = AppWindow.FindResource("ProjectPanel") as Style;

            ProjectButton projectButton = new ProjectButton(obj);
            projectButton.Style = AppWindow.FindResource("ProjectButton") as Style;
            projectButton.Content = projectPanel;
            projectButton.Click += new RoutedEventHandler(ProjectButton_Click);
            ProjectsDisplay.Children.Add(projectButton);
        }

        private void ProjectButton_Click(object sender, RoutedEventArgs e)
        {
            ProjectButton btn = sender as ProjectButton;
            SwitchContent(new ProjectPage(btn.ProjectId, AppWindow));
        }

        private void NewProjectBtn_Click(object sender, RoutedEventArgs e)
        {
            SwitchContent(new CreateProject(AppWindow));
        }
    }

}
