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
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : AnimatedUserControl
    {
        public UserPage()
        {
            InitializeComponent();
        }

        public double WIDTH_COLUMN_SEPERATOR { get; private set; }

        protected override Size ArrangeOverride(Size finalSize)
        {
            List<UIElement> visibleItems = new List<UIElement>();
            double xColumn1 = 0;
            double xColumn2 = (finalSize.Width / 2) + (WIDTH_COLUMN_SEPERATOR / 2);
            double y = 0;

            double columnWidth = (finalSize.Width - WIDTH_COLUMN_SEPERATOR) / 2;

            //for (int i = 0; i < InternalChildren.Count; i++)
            //{
            //    UIElement child = InternalChildren[i];
            //    if (child.Visibility != Visibility.Collapsed)
            //    {
            //        visibleItems.Add(child);
            //    }
            //}

            for (int i = 0; i < visibleItems.Count; i++)
            {
                if (i >= (visibleItems.Count - 1))
                {
                    visibleItems[i].Arrange(new Rect(xColumn1, y, columnWidth, visibleItems[i].DesiredSize.Height));
                }
                else
                {
                    UIElement leftItem = visibleItems[i];
                    UIElement rightItem = visibleItems[i + 1];

                    double rowHeight = leftItem.DesiredSize.Height > rightItem.DesiredSize.Height ? leftItem.DesiredSize.Height : rightItem.DesiredSize.Height;

                    leftItem.Arrange(new Rect(xColumn1, y, columnWidth, rowHeight));
                    rightItem.Arrange(new Rect(xColumn2, y, columnWidth, rowHeight));

                    y += rowHeight;

                    i++;
                }
            }

            return finalSize;
        }
    }
}
