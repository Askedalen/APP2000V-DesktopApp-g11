﻿using APP2000V_DesktopApp_g11.Assets;
using APP2000V_DesktopApp_g11.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace APP2000V_DesktopApp_g11.Views
{
    public partial class DesktopGUI : Window
    {
        public AnimatedUserControl Dashboard { get; set; }
        public AnimatedUserControl Projects { get; set; }
        public AnimatedUserControl Employees { get; set; }
        public AnimatedUserControl Archive { get; set; }

        public DesktopGUI()
        {
            InitializeComponent();
            this.Hide();
            new SplashScreen();
        }

        public void OpenWindow()
        {
            ContentArea.Content = new Dashboard();
            Log.Gui = this;
            this.Show();
        }

        BlurEffect myEffect = new BlurEffect();
        private void NavBtn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            ClearBtnColor();
            btn.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#41474c"));
            AnimatedUserControl currentContent = ContentArea.Content as AnimatedUserControl;
            if (sender.Equals(DashboardBtn))
            {
                if (Dashboard is null)
                {
                    currentContent.SwitchContent(Dashboard = new Dashboard());
                }
                else if (currentContent == Dashboard)
                {
                    currentContent.SwitchContent(Dashboard = new Dashboard());
                }
                else
                {
                    currentContent.SwitchContent(Dashboard);
                }
            }
            else if (sender.Equals(ProjectsBtn))
            {
                if (Projects is null)
                {
                    currentContent.SwitchContent(Projects = new Projects());
                }
                else if (currentContent == Projects)
                {
                    currentContent.SwitchContent(Projects = new Projects());
                }
                else
                {
                    currentContent.SwitchContent(Projects);
                }
            }
            else if (sender.Equals(EmployeesBtn))
            {
                if (Employees is null)
                {
                    currentContent.SwitchContent(Employees = new Employees());
                }
                else if (currentContent == Employees)
                {
                    currentContent.SwitchContent(Employees = new Employees());
                }
                else
                {
                    currentContent.SwitchContent(Employees);
                }
            }
            else if (sender.Equals(ArchiveBtn))
            {
                if (Archive is null)
                {
                    currentContent.SwitchContent(Archive = new Archive());
                }
                else if (currentContent == Archive)
                {
                    currentContent.SwitchContent(Archive = new Archive());
                }
                else
                {
                    currentContent.SwitchContent(Archive);
                }
            }
            myEffect.Radius = 10;
            Effect = myEffect;
            using (LoadingWindow lw = new LoadingWindow(Simulator))
            {
                lw.Owner = this;
                lw.ShowDialog();

            }
            myEffect.Radius = 0;
            Effect = myEffect;
        }



        private void ClearBtnColor()
        {
            SolidColorBrush color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#68737c"));
            DashboardBtn.Background = color;
            ProjectsBtn.Background = color;
            EmployeesBtn.Background = color;
            ArchiveBtn.Background = color;
        }

        void Simulator()
        {
            for (int i = 0; i < 100; i++)
                Thread.Sleep(5);
        }
    }
}

