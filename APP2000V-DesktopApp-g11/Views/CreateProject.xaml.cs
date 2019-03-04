using APP2000V_DesktopApp_g11.Assets;
using APP2000V_DesktopApp_g11.Models;
using APP2000V_DesktopApp_g11.Models.Database;
using MySql.Data.MySqlClient;
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
    /// Interaction logic for CreateProject.xaml
    /// </summary>
    public partial class CreateProject : AnimatedUserControl
    {
        Persistence Db = new Persistence();
        public CreateProject(DesktopGUI gui) : base(gui)
        {
            InitializeComponent();
        }

        private void RegisterProjectBtn_Click(object sender, RoutedEventArgs e)
        {
            // Parsing year, month and day for dates
            string[] startParts = ProjectStartInput.Text.Split('-');
            int sy = Int32.Parse(startParts[0]);
            int sm = Int32.Parse(startParts[1]);
            int sd = Int32.Parse(startParts[2]);
            string[] deadlineParts = ProjectStartInput.Text.Split('-');
            int dy = Int32.Parse(startParts[0]);
            int dm = Int32.Parse(startParts[1]);
            int dd = Int32.Parse(startParts[2]);

            // Uses Persistence object to insert the project into the database
            // Returns 0 if operation succeeds
            int result = Db.CreateProject(new Project
            {
                ProjectName = ProjectNameInput.Text,
                ProjectDescription = ProjectDescInput.Text,
                ProjectStart = new DateTime(sy, sm, sd),
                ProjectDeadline = new DateTime(dy, dm, dd)
            });

            if (result == 0)
            {
                ConfirmationBox.Text = "Project is registered!";
            }
            else
            {
                ConfirmationBox.Text = "Something went wrong!";
            }

        }
    }
}
