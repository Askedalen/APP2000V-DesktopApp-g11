using APP2000V_DesktopApp_g11.Assets;
using APP2000V_DesktopApp_g11.Controllers;
using APP2000V_DesktopApp_g11.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace APP2000V_DesktopApp_g11.Views
{
    /// <summary>
    /// Interaction logic for ProjectPage.xaml
    /// </summary>
    public partial class ProjectPage : AnimatedUserControl
    {
        Persistence Db = new Persistence();
        ProjectController Pc = new ProjectController();
        Project CurrentProject;
        DesktopGUI AppWindow;

        public ProjectPage(int projectID, DesktopGUI gui) : base(gui)
        {
            InitializeComponent();
            AppWindow = gui;
            Console.WriteLine("Project: " + projectID);
            CurrentProject = Db.GetSingleProject(projectID);
            DisplayProjectName.Text = CurrentProject.ProjectName;
            PrintBacklog();
            PrintLists();
            PrintParticipants();
        }

        // EVENTS

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

        private void CreateTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            CreateTask();
        }

        private void CreateListBtn_Click(object sender, RoutedEventArgs e)
        {
            CreateList();
        }

        private void TaskButton_Click(object sender, RoutedEventArgs e)
        {
            TaskButton currentTask = sender as TaskButton;
            PrintTaskInfo(currentTask);
        }

        private void CloseTaskPopupBtn_Click(object sender, RoutedEventArgs e)
        {
            TaskPopup.IsOpen = false;
        }

        private void AddTaskAssignmentBtn_Click(object sender, RoutedEventArgs e)
        {
            UserTaskButton emp = sender as UserTaskButton;
            CreateTaskAssignment(emp);
        }

        private void RemoveTaskAssignmentBtn_Click(object sender, RoutedEventArgs e)
        {
            UserTaskButton emp = sender as UserTaskButton;
            DeleteTaskAssignment(emp);
        }

        private void PopupSaveTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            PTask currentTask = PopupTaskName.DataContext as PTask;
            UpdateTask(currentTask);
        }



        private void OpenEmployeePopupBtn_Click(object sender, RoutedEventArgs e)
        {
            EmployeePopup.IsOpen = true;
            PrintAvaliableEmployees();
        }

        private void CloseEmployeePopupBtn_Click(object sender, RoutedEventArgs e)
        {
            EmployeePopup.IsOpen = false;
        }

        private void AddEmpBtn_Click(object sender, RoutedEventArgs e)
        {
            UserButton emp = sender as UserButton;
            AddProjectMember(emp);
        }

        private void SearchEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DropParticipantBtn_Click(object sender, RoutedEventArgs e)
        {
            UserButton emp = sender as UserButton;
            DropProjectMember(emp);
        }



        private void EmpBtn_Click(object sender, RoutedEventArgs e)
        {
            UserButton emp = sender as UserButton;
            //SwitchContent(new EmployeePage(emp.UserId));
        }

        private void ProjectSettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            SwitchContent(new ProjectSettings(CurrentProject, AppWindow));
        }

        private void ToggleBacklogColBtn_Click(object sender, RoutedEventArgs e)
        {
            AnimateColWidth(sender as Button);
        }

        private void ToggleEmployeeColBtn_Click(object sender, RoutedEventArgs e)
        {
            AnimateColWidth(sender as Button);
        }

        // PRINT ELEMENTS
        private void PrintBacklog()
        {
            BacklogPanel.Children.Clear();
            List<PTask> taskList = Db.GetBacklog(CurrentProject.ProjectId);
            taskList.ForEach(t =>
            {
                TextBlock taskBlock = new TextBlock
                {
                    Text = t.TaskName,
                    Style = AppWindow.FindResource("NormalTaskText") as Style
                };
                TaskButton taskButton = new TaskButton(t)
                {
                    Content = taskBlock,
                    Style = AppWindow.FindResource("NormalTaskButton") as Style
                };
                taskButton.Click += new RoutedEventHandler(TaskButton_Click);
                BacklogPanel.Children.Add(taskButton);
            });
        }

        private void PrintParticipants()
        {
            ParticipantsPanel.Children.Clear();
            List<User> employees = Db.GetAllProjectMembers(CurrentProject.ProjectId);
            employees.ForEach(e =>
            {
                TextBlock empNameBlock = new TextBlock
                {
                    Text = e.FirstName + " " + e.LastName,
                    Style = AppWindow.FindResource("PmemberText") as Style
                };
                UserButton deleteBtn = new UserButton(e)
                {
                    Style = AppWindow.FindResource("PmemberDropBtn") as Style
                };
                StackPanel empPanel = new StackPanel
                {
                    Style = AppWindow.FindResource("PmemberPanel") as Style
                };
                empPanel.Children.Add(empNameBlock);
                if (e.UserId != CurrentProject.ProjectManager)
                {
                    empPanel.Children.Add(deleteBtn);
                }
                deleteBtn.Click += new RoutedEventHandler(DropParticipantBtn_Click);
                UserButton empBtn = new UserButton(e)
                {
                    Style = AppWindow.FindResource("PmemberBtn") as Style,
                    Content = empPanel,
                };
                empBtn.Click += new RoutedEventHandler(EmpBtn_Click);
                ParticipantsPanel.Children.Add(empBtn);
            });
        }

        private void PrintLists()
        {
            ListPanel.Children.Clear();
            List<TaskList> lists = Db.GetLists(CurrentProject.ProjectId);
            lists.ForEach(l =>
            {
                TextBlock listName = new TextBlock
                {
                    Text = l.ListName,
                    Style = AppWindow.FindResource("ListNameText") as Style
                };

                Border nameBorder = new Border
                {
                    Style = AppWindow.FindResource("ListNameBorder") as Style,
                    Child = listName
                };

                StackPanel panelInScroll = new StackPanel();
                List<TaskButton> taskList = CreateListTasks(l);
                taskList.ForEach(t =>
                {
                    panelInScroll.Children.Add(t);
                });

                ScrollViewer taskScroll = new ScrollViewer
                {
                    Style = AppWindow.FindResource("ListTaskScrollbar") as Style,
                    Content = panelInScroll
                };

                StackPanel currentList = new StackPanel
                {
                    Style = AppWindow.FindResource("ListPanel") as Style
                };
                currentList.Children.Add(nameBorder);
                currentList.Children.Add(taskScroll);
                ListPanel.Children.Add(currentList);
            });
        }

        private void PrintTaskInfo(TaskButton currentTask)
        {
            PTask taskInfo = Db.GetSingleTask(currentTask.TaskId);
            TaskBindingGrid.DataContext = taskInfo;

            List<TaskList> taskLists = Db.GetLists(CurrentProject.ProjectId);
            ChooseTaskList.ItemsSource = taskLists;

            ChooseTaskList.SelectedIndex = taskInfo.TaskListId.HasValue ? taskInfo.TaskListId.Value - 1 : -1;

            switch (taskInfo.Priority)
            {
                case "low":
                    ChoosePriorityList.SelectedIndex = 0;
                    break;
                case "normal":
                    ChoosePriorityList.SelectedIndex = 1;
                    break;
                case "high":
                    ChoosePriorityList.SelectedIndex = 2;
                    break;
                default:
                    ChoosePriorityList.SelectedIndex = -1;
                    break;
            }

            PrintTaskAssignment(currentTask.TaskId);
            PrintTaskNotAssigned(currentTask.TaskId);

            TaskPopup.IsOpen = true;
        }

        private void PrintTaskNotAssigned(int tid)
        {
            TaskNotAssignedView.Children.Clear();
            List<User> emps = Db.GetTaskNotAssigned(tid, CurrentProject.ProjectId);
            emps.ForEach(emp =>
            {
                TextBlock empNameText = new TextBlock
                {
                    Text = emp.FirstName + " " + emp.LastName,
                    FontSize = 24
                };
                UserTaskButton removeBtn = new UserTaskButton(emp, new PTask { TaskId = tid })
                {
                    FontSize = 18,
                    Content = "Add"
                };
                removeBtn.Click += new RoutedEventHandler(AddTaskAssignmentBtn_Click);
                StackPanel empPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal
                };
                empPanel.Children.Add(empNameText);
                empPanel.Children.Add(removeBtn);
                TaskNotAssignedView.Children.Add(empPanel);
            });
        }

        private void PrintTaskAssignment(int tid)
        {
            TaskAssignmentView.Children.Clear();
            List<User> emps = Db.GetTaskAssignment(tid);
            emps.ForEach(emp =>
            {
                TextBlock empNameText = new TextBlock
                {
                    Text = emp.FirstName + " " + emp.LastName,
                    FontSize = 24
                };
                UserTaskButton removeBtn = new UserTaskButton(emp, new PTask { TaskId = tid })
                {
                    FontSize = 18,
                    Content = "X"
                };
                removeBtn.Click += new RoutedEventHandler(RemoveTaskAssignmentBtn_Click);
                StackPanel empPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal
                };
                empPanel.Children.Add(empNameText);
                empPanel.Children.Add(removeBtn);
                TaskAssignmentView.Children.Add(empPanel);
            });
        }

        private void PrintAvaliableEmployees()
        {
            EmployeePopupList.Children.Clear();
            List<User> avaliableEmps = Db.GetAllEmployeesNotInProject(CurrentProject.ProjectId);
            avaliableEmps.ForEach(emp =>
            {
                Rectangle imagePlaceholder = new Rectangle
                {
                    Height = 100,
                    Width = 100,
                    Fill = new SolidColorBrush(Colors.Gray)
                };
                TextBlock empNameTb = new TextBlock
                {
                    Text = emp.FirstName + " " + emp.LastName,
                    FontSize = 24,
                    TextWrapping = TextWrapping.Wrap,
                    TextAlignment = TextAlignment.Center,
                    Foreground = new SolidColorBrush(Colors.Black)
                };
                StackPanel empInfoPanel = new StackPanel();
                empInfoPanel.Children.Add(imagePlaceholder);
                empInfoPanel.Children.Add(empNameTb);
                UserButton empBtn = new UserButton(emp)
                {
                    Height = 200,
                    Background = new SolidColorBrush(Colors.White),
                    BorderBrush = new SolidColorBrush(Colors.White),
                    Content = empInfoPanel
                };
                empBtn.Click += new RoutedEventHandler(EmpBtn_Click);
                UserButton addEmpBtn = new UserButton(emp)
                {
                    Content = "Add",
                    FontSize = 24,
                    Height = 50
                };
                addEmpBtn.Click += new RoutedEventHandler(AddEmpBtn_Click);
                StackPanel empPanel = new StackPanel
                {
                    Margin = new Thickness(15, 15, 15, 0),
                    Width = 230,
                    Background = new SolidColorBrush(Colors.White)
                };
                empPanel.Children.Add(empBtn);
                empPanel.Children.Add(addEmpBtn);
                EmployeePopupList.Children.Add(empPanel);
            });
        }

        private List<TaskButton> CreateListTasks(TaskList l)
        {
            List<TaskButton> taskButtons = new List<TaskButton>();
            List<PTask> listTasks = Db.GetListTasks(l);
            listTasks.ForEach(t =>
            {
                TextBlock taskBlock = new TextBlock
                {
                    Text = t.TaskName,
                    Style = AppWindow.FindResource("NormalTaskText") as Style
                };
                TaskButton taskButton = new TaskButton(t)
                {
                    Content = taskBlock,
                    Style = AppWindow.FindResource("NormalTaskButton") as Style
                };
                taskButton.Click += new RoutedEventHandler(TaskButton_Click);
                taskButtons.Add(taskButton);
            });
            return taskButtons;
        }

        // UPDATE/CREATE DB-ENTRIES

        private void CreateTask()
        {
            PTask newTask = new PTask
            {
                TaskName = CreateTaskTb.Text,
                TaskProjectId = CurrentProject.ProjectId,
                TaskCreationDate = DateTime.Now
            };

            if (Pc.CreateTask(newTask) == 0)
            {
                CreateTaskBtn.Visibility = Visibility.Collapsed;
                CreateTaskTb.Text = "Create new task...";
                CreateTaskTb.Foreground = new SolidColorBrush(Colors.Gray);
                PrintBacklog();
            }
        }

        private void CreateList()
        {
            TaskList newTaskList = new TaskList
            {
                ListName = CreateListTb.Text,
                ProjectId = CurrentProject.ProjectId
            };

            if (Pc.CreateTaskList(newTaskList) == 0)
            {
                CreateListBtn.Visibility = Visibility.Collapsed;
                CreateListTb.Text = "Create new list...";
                CreateListTb.Foreground = new SolidColorBrush(Colors.Gray);
                PrintLists();
            }
        }

        private void UpdateTask(PTask currentTask)
        {
            PTask taskUpdate = new PTask
            {
                TaskId = currentTask.TaskId,
                TaskName = PopupTaskName.Text,
                Description = TaskDescription.Text
            };

            if (TaskDeadlinePicker.SelectedDate != null)
            {
                taskUpdate.TaskDeadline = TaskDeadlinePicker.SelectedDate;
            }

            if (ChooseTaskList.SelectedValue != null)
            {
                TaskList chosenList = ChooseTaskList.SelectedValue as TaskList;
                taskUpdate.TaskListId = chosenList.TaskListId;
            }

            int priority = ChoosePriorityList.SelectedIndex;
            switch (priority)
            {
                case 0:
                    taskUpdate.Priority = "low";
                    break;
                case 1:
                    taskUpdate.Priority = "normal";
                    break;
                case 2:
                    taskUpdate.Priority = "high";
                    break;
                default:
                    break;
            }

            if (Pc.UpdateTask(taskUpdate) == 0)
            {
                TaskPopup.IsOpen = false;
                PrintBacklog();
                PrintLists();
            }
        }

        private void CreateTaskAssignment(UserTaskButton emp)
        {
            if (Pc.AddTaskAssignment(emp.UserId, CurrentProject.ProjectId, emp.TaskId) == 0)
            {
                PrintTaskAssignment(emp.TaskId);
                PrintTaskNotAssigned(emp.TaskId);
            }
        }

        private void DeleteTaskAssignment(UserTaskButton emp)
        {
            if (Pc.RemoveTaskAssignment(emp.UserId, emp.TaskId) == 0)
            {
                PrintTaskAssignment(emp.TaskId);
                PrintTaskNotAssigned(emp.TaskId);
            }
        }

        private void AddProjectMember(UserButton emp)
        {
            if (Pc.AddProjectParticipant(emp.UserId, CurrentProject.ProjectId) == 0)
            {
                PrintAvaliableEmployees();
                PrintParticipants();
            }
        }

        private void DropProjectMember(UserButton emp)
        {
            if (Pc.DropProjectParticipant(emp.UserId, CurrentProject.ProjectId) == 0)
            {
                PrintParticipants();
            }
        }

        // COLUMN ANIMATION

        private void AnimateColWidth(Button button)
        {
            if (button.Equals(ToggleBacklogColBtn))
            {
                Storyboard toggleBacklogStory = new Storyboard();
                DoubleAnimation toggleBacklogAnim = new DoubleAnimation();
                toggleBacklogAnim.EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut };
                toggleBacklogAnim.Duration = new Duration(TimeSpan.FromMilliseconds(500));
                toggleBacklogStory.Children.Add(toggleBacklogAnim);
                if (BacklogCol.MaxWidth == 400)
                {
                    toggleBacklogAnim.From = 400;
                    toggleBacklogAnim.To = 0;
                }
                else
                {
                    toggleBacklogAnim.From = 0;
                    toggleBacklogAnim.To = 400;
                }
                Storyboard.SetTarget(toggleBacklogAnim, BacklogCol);
                Storyboard.SetTargetProperty(toggleBacklogAnim, new PropertyPath("(ColumnDefinition.MaxWidth)"));

                toggleBacklogStory.Begin();
            }
            else if (button.Equals(ToggleEmployeeColBtn))
            {
                Storyboard toggleEmpStory = new Storyboard();
                DoubleAnimation toggleEmpAnim = new DoubleAnimation();
                toggleEmpAnim.EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut };
                toggleEmpAnim.Duration = new Duration(TimeSpan.FromMilliseconds(500));
                toggleEmpStory.Children.Add(toggleEmpAnim);
                if (EmployeesCol.MaxWidth == 400)
                {
                    toggleEmpAnim.From = 400;
                    toggleEmpAnim.To = 0;
                }
                else
                {
                    toggleEmpAnim.From = 0;
                    toggleEmpAnim.To = 400;
                }
                Storyboard.SetTarget(toggleEmpAnim, EmployeesCol);
                Storyboard.SetTargetProperty(toggleEmpAnim, new PropertyPath("(ColumnDefinition.MaxWidth)"));

                toggleEmpStory.Begin();
            }
        }

    }
}
