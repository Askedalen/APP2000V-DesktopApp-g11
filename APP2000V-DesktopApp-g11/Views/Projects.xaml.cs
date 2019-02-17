﻿using System;
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
    /// Interaction logic for Projects.xaml
    /// </summary>
    public partial class Projects : UserControl
    {
        private ContentControl ContentArea;
        public Projects(ContentControl contentControl)
        {
            InitializeComponent();
            ContentArea = contentControl;
        }

        private void NewProjectBtn_Click(object sender, RoutedEventArgs e)
        {
            this.ContentArea.Content = new CreateProject();
        }
    }
}