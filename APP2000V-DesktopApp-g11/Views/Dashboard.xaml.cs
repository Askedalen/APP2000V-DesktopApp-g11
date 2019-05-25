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
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : AnimatedUserControl
    {
        Persistence Db = new Persistence();
        ProjectController Pc = new ProjectController();
        DesktopGUI AppWindow;
        public Dashboard() : base()
        {
            InitializeComponent();
            AppWindow = App.Current.MainWindow as DesktopGUI;

            PrintTopProjects();
            PrintProjectDeadlines();
            PrintTaskDeadlines();
            PrintFinishedProjects();
            PrintStats();
        }

        private void PrintStats()
        {
            dynamic statsObject = Pc.GetStats();
            StatsGrid.DataContext = statsObject;
        }

        private void PrintFinishedProjects()
        {
            FinishedProjectsPanel.Children.Clear();
            Grid finishedProjectsGrid = new Grid
            {
                Style = AppWindow.FindResource("DeadlineViewGrid") as Style
            };
            for (int i = 0; i < 2; i++)
            {
                finishedProjectsGrid.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = GridLength.Auto
                });
            }
            int currentRow = 0;

            List<Project> finishedProjects = Db.GetProjectsMarkedAsFinished();
            if (finishedProjects.Count > 0)
            {
                MarkedAsFinishedContainer.Visibility = Visibility.Visible;
            }
            finishedProjects.ForEach(p =>
            {
                for (int i = 0; i < 2; i++)
                {
                    finishedProjectsGrid.RowDefinitions.Add(new RowDefinition
                    {
                        Height = GridLength.Auto
                    });
                }
                TextBlock projectName = new TextBlock
                {
                    Text = p.ProjectName,
                    Style = AppWindow.FindResource("DashboardDisplayName") as Style
                };
                Grid.SetColumn(projectName, 0);
                Grid.SetRow(projectName, currentRow);
                finishedProjectsGrid.Children.Add(projectName);

                ProjectButton viewReportBtn = new ProjectButton(p)
                {
                    Content = "View report",
                    Style = AppWindow.FindResource("ViewReportBtn") as Style
                };
                Grid.SetColumn(viewReportBtn, 1);
                Grid.SetRow(viewReportBtn, currentRow++);
                viewReportBtn.Click += new RoutedEventHandler(ViewReportBtn_Click);
                finishedProjectsGrid.Children.Add(viewReportBtn);

                TextBlock markedFinishedBy = new TextBlock
                {
                    Text = "Marked as finished by " + p.User.FirstName + " " + p.User.LastName,
                    Style = AppWindow.FindResource("DashboardDisplaySubtext") as Style
                };
                Grid.SetColumn(markedFinishedBy, 0);
                Grid.SetRow(markedFinishedBy, currentRow);
                finishedProjectsGrid.Children.Add(markedFinishedBy);

                ProjectButton viewProjectBtn = new ProjectButton(p)
                {
                    Content = "View project",
                    Style = AppWindow.FindResource("DashboardViewProjectBtn") as Style
                };
                Grid.SetColumn(viewProjectBtn, 1);
                Grid.SetRow(viewProjectBtn, currentRow);
                viewProjectBtn.Click += new RoutedEventHandler(GoToProjectBtn_Click);
                finishedProjectsGrid.Children.Add(viewProjectBtn);

                Border border = new Border
                {
                    Style = AppWindow.FindResource("DashboardDisplayBorder") as Style
                };
                Grid.SetColumnSpan(border, 2);
                Grid.SetRow(border, currentRow++);
                finishedProjectsGrid.Children.Add(border);
            });
            FinishedProjectsPanel.Children.Add(finishedProjectsGrid);
        }

        private void ViewReportBtn_Click(object sender, RoutedEventArgs e)
        {
            ProjectButton btn = sender as ProjectButton;
            SwitchContent(AppWindow.Dashboard = new ProjectReport(btn.ProjectId));
        }

        private void PrintTopProjects()
        {
            OngoingProjectsPanel.Children.Clear();
            List<Project> projects = Db.GetTopProjects();
            projects.ForEach(p =>
            {
                TextBlock projectTitle = new TextBlock
                {
                    Text = p.ProjectName,
                    Style = AppWindow.FindResource("TopProjectTitle") as Style
                };
                TextBlock projectDesc = new TextBlock
                {
                    Text = p.ProjectDescription,
                    Style = AppWindow.FindResource("TopProjectDescription") as Style
                };
                ProjectButton goToProjectBtn = new ProjectButton(p)
                {
                    Content = "View project",
                    Style = AppWindow.FindResource("TopProjectViewBtn") as Style
                };
                goToProjectBtn.Click += new RoutedEventHandler(GoToProjectBtn_Click);
                StackPanel topProjectPanel = new StackPanel
                {
                    Style = AppWindow.FindResource("TopProjectPanel") as Style
                };
                topProjectPanel.Children.Add(projectTitle);
                topProjectPanel.Children.Add(projectDesc);
                topProjectPanel.Children.Add(goToProjectBtn);
                OngoingProjectsPanel.Children.Add(topProjectPanel);
            });
        }

        private void PrintProjectDeadlines()
        {
            ProjectDeadlines.Children.Clear();
            Grid projectGrid = new Grid
            {
                Style = AppWindow.FindResource("DeadlineViewGrid") as Style
            };
            projectGrid.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = GridLength.Auto
            });
            projectGrid.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            });
            int currentRow = 0;
           
            List<Project> projects = Db.GetProjectUpcomingDeadlines();
            projects.ForEach(p =>
            {
                for (int i = 0; i < 2; i++)
                {
                    projectGrid.RowDefinitions.Add(new RowDefinition
                    {
                        Height = GridLength.Auto
                    });
                }
                TextBlock projectName = new TextBlock
                {
                    Text = p.ProjectName,
                    Style = AppWindow.FindResource("DashboardDisplayName") as Style
                };
                Grid.SetColumn(projectName, 0);
                Grid.SetRow(projectName, currentRow);
                projectGrid.Children.Add(projectName);

                string dateString;
                SolidColorBrush dateColor;
                if (DateTime.Compare(p.ProjectDeadline.Value, DateTime.Now) < 0)
                {
                    dateString = "Over deadline";
                    dateColor = new SolidColorBrush(Colors.Black);
                }
                else
                {
                    dateString = p.ProjectDeadline.Value.ToShortDateString() + " " + p.ProjectDeadline.Value.ToShortTimeString();
                    if (DateTime.Compare(p.ProjectDeadline.Value, DateTime.Now.AddDays(7)) < 0)
                    {
                        dateColor = new SolidColorBrush(Colors.Red);
                    }
                    else if (DateTime.Compare(p.ProjectDeadline.Value, DateTime.Now.AddMonths(1)) < 0)
                    {
                        dateColor = new SolidColorBrush(Colors.Orange);
                    }
                    else
                    {
                        dateColor = new SolidColorBrush(Colors.Green);
                    }
                }
                TextBlock deadlineDate = new TextBlock
                {
                    Text = dateString,
                    Foreground = dateColor,
                    Style = AppWindow.FindResource("DeadlineViewDate") as Style
                };
                Grid.SetColumn(deadlineDate, 1);
                Grid.SetRow(deadlineDate, currentRow++);
                projectGrid.Children.Add(deadlineDate);

                TextBlock taskCount = new TextBlock
                {
                    Text = Db.GetActiveTaskCount(p.ProjectId) + " tasks left",
                    Style = AppWindow.FindResource("DashboardDisplaySubtext") as Style
                };
                Grid.SetColumn(taskCount, 0);
                Grid.SetRow(taskCount, currentRow);
                projectGrid.Children.Add(taskCount);

                ProjectButton deadlineViewProjectBtn = new ProjectButton(p)
                {
                    Content = "View project",
                    Style = AppWindow.FindResource("DashboardViewProjectBtn") as Style
                };
                Grid.SetColumn(deadlineViewProjectBtn, 1);
                Grid.SetRow(deadlineViewProjectBtn, currentRow);
                deadlineViewProjectBtn.Click += new RoutedEventHandler(GoToProjectBtn_Click);
                projectGrid.Children.Add(deadlineViewProjectBtn);

                Border border = new Border
                {
                    Style = AppWindow.FindResource("DashboardDisplayBorder") as Style
                };
                Grid.SetColumnSpan(border, 2);
                Grid.SetRow(border, currentRow++);
                projectGrid.Children.Add(border);
            });
            ProjectDeadlines.Children.Add(projectGrid);
        }

        private void PrintTaskDeadlines()
        {
            TaskDeadlines.Children.Clear();
            Grid taskGrid = new Grid
            {
                Style = AppWindow.FindResource("DeadlineViewGrid") as Style
            };
            taskGrid.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = GridLength.Auto
            });
            taskGrid.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            });
            int currentRow = 0;

            List<PTask> tasks = Db.GetTaskUpcomingDeadlines();
            tasks.ForEach(t =>
            {

                for (int i = 0; i < 2; i++)
                {
                    taskGrid.RowDefinitions.Add(new RowDefinition
                    {
                        Height = GridLength.Auto
                    });
                }
                TextBlock taskName = new TextBlock
                {
                    Text = t.TaskName,
                    Style = AppWindow.FindResource("DashboardDisplayName") as Style
                };
                Grid.SetColumn(taskName, 0);
                Grid.SetRow(taskName, currentRow);
                taskGrid.Children.Add(taskName);

                string dateString;
                SolidColorBrush dateColor;
                if (DateTime.Compare(t.TaskDeadline.Value, DateTime.Now) < 0)
                {
                    dateString = "Over deadline";
                    dateColor = new SolidColorBrush(Colors.Black);
                }
                else
                {
                    dateString = t.TaskDeadline.Value.ToShortDateString() + " " + t.TaskDeadline.Value.ToShortTimeString();
                    if (DateTime.Compare(t.TaskDeadline.Value, DateTime.Now.AddDays(5)) < 0)
                    {
                        dateColor = new SolidColorBrush(Colors.Red);
                    }
                    else if (DateTime.Compare(t.TaskDeadline.Value, DateTime.Now.AddDays(14)) < 0)
                    {
                        dateColor = new SolidColorBrush(Colors.Orange);
                    }
                    else
                    {
                        dateColor = new SolidColorBrush(Colors.Green);
                    }
                }
                TextBlock deadlineDate = new TextBlock
                {
                    Text = dateString,
                    Foreground = dateColor,
                    Style = AppWindow.FindResource("DeadlineViewDate") as Style
                };
                Grid.SetColumn(deadlineDate, 1);
                Grid.SetRow(deadlineDate, currentRow++);
                taskGrid.Children.Add(deadlineDate);

                TextBlock projectName = new TextBlock
                {
                    Text = t.Project.ProjectName,
                    Style = AppWindow.FindResource("DashboardDisplaySubtext") as Style
                };
                Grid.SetColumn(projectName, 0);
                Grid.SetRow(projectName, currentRow);
                taskGrid.Children.Add(projectName);

                ProjectButton deadlineViewProjectBtn = new ProjectButton(t.Project)
                {
                    Content = "View project",
                    Style = AppWindow.FindResource("DashboardViewProjectBtn") as Style
                };
                Grid.SetColumn(deadlineViewProjectBtn, 1);
                Grid.SetRow(deadlineViewProjectBtn, currentRow);
                deadlineViewProjectBtn.Click += new RoutedEventHandler(GoToProjectBtn_Click);
                taskGrid.Children.Add(deadlineViewProjectBtn);

                Border border = new Border
                {
                    Style = AppWindow.FindResource("DashboardDisplayBorder") as Style
                };
                Grid.SetColumnSpan(border, 2);
                Grid.SetRow(border, currentRow++);
                taskGrid.Children.Add(border);
            });
            TaskDeadlines.Children.Add(taskGrid);
        }

        private void GoToProjectBtn_Click(object sender, RoutedEventArgs e)
        {
            ProjectButton btn = sender as ProjectButton;
            SwitchContent(AppWindow.Dashboard = new ProjectPage(btn.ProjectId));
        }
    }
}
