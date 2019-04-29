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
            TextBlock errorHeadline = new TextBlock
            {
                Text = "Something went wrong!",
                Style = Gui.FindResource("MessageHeadline") as Style
            };
            TextBlock errorMessage = new TextBlock
            {
                Text = input,
                Style = Gui.FindResource("MessageText") as Style
            };
            StackPanel errorPanel = new StackPanel
            {
                Style = Gui.FindResource("ErrorPanel") as Style
            };
            errorPanel.Children.Add(errorHeadline);
            errorPanel.Children.Add(errorMessage);
            Gui.MessagesPanel.Children.Add(errorPanel);
            
            await Task.Delay(5000);
            Gui.MessagesPanel.Children.Remove(errorPanel);
        }

        public async static void Message(string headline, string message)
        {
            TextBlock messageHeadline = new TextBlock
            {
                Text = headline,
                Style = Gui.FindResource("MessageHeadline") as Style
            };
            TextBlock messageText = new TextBlock
            {
                Text = message,
                Style = Gui.FindResource("MessageText") as Style
            };
            StackPanel messagePanel = new StackPanel
            {
                Style = Gui.FindResource("MessagePanel") as Style
            };

            messagePanel.Children.Add(messageHeadline);
            messagePanel.Children.Add(messageText);
            Gui.MessagesPanel.Children.Add(messagePanel);

            await Task.Delay(5000);
            Gui.MessagesPanel.Children.Remove(messagePanel);
        }
    }
}
