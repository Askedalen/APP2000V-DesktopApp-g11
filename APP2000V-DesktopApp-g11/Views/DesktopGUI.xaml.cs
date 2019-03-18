using APP2000V_DesktopApp_g11.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace APP2000V_DesktopApp_g11.Views
{
    /// <summary>
    /// Interaction logic for DesktopGUI.xaml
    /// </summary>
    public partial class DesktopGUI : Window
    {
        public DesktopGUI()
        {
            InitializeComponent();
            ContentArea.Content = new Dashboard(this);
         
        }
        BlurEffect myEffect = new BlurEffect();
        private void NavBtn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            ClearBtnColor();
            btn.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF434547"));
            AnimatedUserControl currentContent = ContentArea.Content as AnimatedUserControl;
            if (sender.Equals(DashboardBtn)) currentContent.SwitchContent(new Dashboard(this));
            else if (sender.Equals(ProjectsBtn)) currentContent.SwitchContent(new Projects(this));
            else if (sender.Equals(EmployeesBtn)) currentContent.SwitchContent(new Employees(this));
            //else if (sender.Equals(ArchiveBtn)) ContentArea.Content = new Archive();
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
            SolidColorBrush color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF66A8F7"));
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

