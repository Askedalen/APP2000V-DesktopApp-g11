using APP2000V_DesktopApp_g11.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace APP2000V_DesktopApp_g11.Controllers
{
    public class Log
    {
        public static DesktopGUI Gui;
        public async static void Error(string input)
        {
            Gui.ErrorMessage.Text = input;
            Gui.ErrorPopup.IsOpen = true;
            await Task.Delay(5000);
            Gui.ErrorPopup.IsOpen = false;
        }
    }
}
