﻿using APP2000V_DesktopApp_g11.Views;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace APP2000V_DesktopApp_g11.Assets
{
    public class AnimatedUserControl : UserControl
    {
        
        private ContentControl ContentArea; // Contains the User Control, used to switch content after FadeOutAnimation is finished
        private DoubleAnimation FadeOutAnimation; // Animation for fading out
        private DoubleAnimation FadeInAnimation; // Animation for fading in
        private AnimatedUserControl NewContent; // NewContent is given a value when the content is being changed

        public AnimatedUserControl() : base()
        {
            if (Application.Current is App) 
                // Only runs default constructor if something other than App is running the user control
                // Without this the XAML-editor in Visual Studio does not work
            {
                DesktopGUI gui = App.Current.MainWindow as DesktopGUI;
                ContentArea = gui.ContentArea;

                /*
                    CREATE ANIMATIONS
                */
            
                // Animation for fade out
                FadeOutAnimation = new DoubleAnimation();
                FadeOutAnimation.From = 1;
                FadeOutAnimation.To = 0;
                FadeOutAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(100));
                //EventHandler for animation completed
                FadeOutAnimation.Completed += new EventHandler(fadeOutAnimation_Completed);

                //Animation for fade in
                FadeInAnimation = new DoubleAnimation();
                FadeInAnimation.From = 0;
                FadeInAnimation.To = 1;
                FadeInAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(100));
            }
        }

        private void fadeOutAnimation_Completed(object sender, EventArgs e)
        {
            // Changes the content and trigger NewContent's FadeIn method
            NewContent.Opacity = 0;
            ContentArea.Content = NewContent;
            NewContent.FadeIn();
        }

        public void SwitchContent(AnimatedUserControl newContent)
        {
            // Starts FadeOutAnimation and assigns NewContent
            BeginAnimation(AnimatedUserControl.OpacityProperty, FadeOutAnimation);
            NewContent = newContent;
        }

        public void FadeIn()
        {
            //Starts animation for fading out. Usually triggered from another AnimatedUserControl
            BeginAnimation(AnimatedUserControl.OpacityProperty, FadeInAnimation);
        }

    }
}
