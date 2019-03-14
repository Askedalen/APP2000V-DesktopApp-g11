using APP2000V_DesktopApp_g11.Assets;
using APP2000V_DesktopApp_g11.Models;
using APP2000V_DesktopApp_g11.Models.Database;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace APP2000V_DesktopApp_g11.Views
{
    /// <summary>
    /// Interaction logic for ProjectPage.xaml
    /// </summary>
    public partial class ProjectPage : AnimatedUserControl
    {
        Persistence Db = new Persistence();
        Project CurrentProject;
        public ProjectPage(int projectID, DesktopGUI gui) : base(gui)
        {
            InitializeComponent();
            Console.WriteLine("Project: " + projectID);
            CurrentProject = Db.GetSingleProject(projectID);
            DisplayProjectName.Text = CurrentProject.ProjectName;
            PrintBacklog();
            PrintLists();
        }

        private void CreateTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            PTask newTask = new PTask
            {
                TaskName = CreateTaskTb.Text,
                TaskProjectID = CurrentProject.ProjectID,
                TaskCreationDate = DateTime.Now
            };

            if (Db.CreateTask(newTask) == 0)
            {
                CreateTaskBtn.Visibility = Visibility.Collapsed;
                CreateTaskTb.Text = "Create new task...";
                CreateTaskTb.Foreground = new SolidColorBrush(Colors.Gray);
                PrintBacklog();
                Console.WriteLine("PTask is created");
            }
            else
            {
                Console.WriteLine("PTask was not created");
            }

        }

        private void CreateListBtn_Click(object sender, RoutedEventArgs e)
        {
            TaskList newTaskList = new TaskList
            {
                ListName = CreateListTb.Text,
                ProjectID = CurrentProject.ProjectID
            };

            if (Db.CreateTaskList(newTaskList) == 0)
            {
                CreateTaskBtn.Visibility = Visibility.Collapsed;
                CreateTaskTb.Text = "Create new task...";
                CreateTaskTb.Foreground = new SolidColorBrush(Colors.Gray);
                PrintBacklog();
                Console.WriteLine("TaskList is created");
            }
            else
            {
                Console.WriteLine("TaskList was not created");
            }

        }

        private void PrintBacklog()
        {
            BacklogPanel.Children.Clear();
            List<PTask> taskList = Db.GetBacklog(CurrentProject.ProjectID);
            taskList.ForEach(t =>
            {
                TextBlock taskBlock = new TextBlock();
                taskBlock.Text = t.TaskName;
                taskBlock.Background = new SolidColorBrush(Colors.White);
                taskBlock.FontSize = 28;
                taskBlock.Padding = new Thickness(30, 20, 0, 25);
                TaskButton taskButton = new TaskButton(t);
                taskButton.Content = taskBlock;
                taskButton.Margin = new Thickness(0, 0, 0, 30);
                taskButton.Padding = new Thickness(0);
                taskButton.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                taskButton.VerticalContentAlignment = VerticalAlignment.Stretch;
                taskButton.Click += new RoutedEventHandler(TaskButton_Click);
                BacklogPanel.Children.Add(taskButton);
            });
        }

        private void PrintLists()
        {
            ListPanel.Children.Clear();
            List<TaskList> lists = Db.GetLists(CurrentProject.ProjectID);
            lists.ForEach(l =>
            {
                TextBlock listName = new TextBlock();
                listName.Text = l.ListName;
                listName.FontSize = 28;
                listName.FontWeight = FontWeights.Bold;
                listName.Margin = new Thickness(15);

                Border nameBorder = new Border();
                nameBorder.BorderBrush = new SolidColorBrush(Colors.Gray);
                nameBorder.BorderThickness = new Thickness(0, 0, 0, 1);
                nameBorder.Child = listName;

                StackPanel panelInScroll = new StackPanel();
                List<TaskButton> taskList = CreateListTasks(l);
                taskList.ForEach(t =>
                {
                    panelInScroll.Children.Add(t);
                });

                ScrollViewer taskScroll = new ScrollViewer();
                taskScroll.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                taskScroll.Content = panelInScroll;

                StackPanel currentList = new StackPanel();
                currentList.Width = 350;
                currentList.Margin = new Thickness(30, 30, 0, 30);
                currentList.Background = new SolidColorBrush(Colors.AliceBlue);
                currentList.Children.Add(nameBorder);
                currentList.Children.Add(taskScroll);
                ListPanel.Children.Add(currentList);
            });
        }

        private List<TaskButton> CreateListTasks(TaskList l)
        {
            List<TaskButton> taskButtons = new List<TaskButton>();
            List<PTask> listTasks = Db.GetListTasks(l);
            listTasks.ForEach(t =>
            {
                TextBlock taskBlock = new TextBlock();
                taskBlock.Text = t.TaskName;
                taskBlock.Background = new SolidColorBrush(Colors.White);
                taskBlock.FontSize = 28;
                taskBlock.Padding = new Thickness(30, 20, 0, 25);
                TaskButton taskButton = new TaskButton(t);
                taskButton.Content = taskBlock;
                taskButton.Margin = new Thickness(0, 0, 0, 30);
                taskButton.Padding = new Thickness(0);
                taskButton.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                taskButton.VerticalContentAlignment = VerticalAlignment.Stretch;
                taskButton.Click += new RoutedEventHandler(TaskButton_Click);
                taskButtons.Add(taskButton);
            });
            return taskButtons;
        }

        private void TaskButton_Click(object sender, RoutedEventArgs e)
        {
            TestPopup.IsOpen = true;
        }

        private void CreateTaskTb_GotFocus(object sender, RoutedEventArgs e)
        {
            CreateTaskBtn.Visibility = Visibility.Visible;
            CreateTaskTb.Text = "";
            CreateTaskTb.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void CreateListTb_GotFocus(object sender, RoutedEventArgs e)
        {
            CreateListBtn.Visibility = Visibility.Visible;
            CreateListTb.Text = "";
            CreateListTb.Foreground = new SolidColorBrush(Colors.Black);
        }

    }
}
