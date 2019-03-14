using APP2000V_DesktopApp_g11.Views;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace APP2000V_DesktopApp_g11.Assets
{
    /// <summary>
    /// Denne klassen gjør at user controls fader inn og ut når de byttes ut.
    /// Et par ting å tenke på her:
    /// - Bytte ut alle UserControl med AnimatedUserControl.
    /// - Content i ContentArea byttes ikke lenger ut med ContentArea.Content,
    ///   men med SwitchContent(AnimatedUserControl newContent).
    /// - Alle AnimatedUserControl må kalle base(DesktopGUI gui) for at
    ///   konstruktøren i denne klassen skal kjøres. Hvis ikke base kalles, 
    ///   vil ikke animasjonene fungere. "DesktopGUI gui" er hovedinduet.
    /// - Se Dashboard.xaml og Dashbard.xaml.cs for eksempler.
    /// - Jeg har skrevet kommentarene her på engelsk i tilfelle Boban skal
    ///   se på det.
    /// - Jeg har ikke kopiert koden. Dette var fullstendig min egen idé,
    ///   så hvis det er litt teit eller klønete, er det lov å si ifra.
    /// - Jævlig nice animasjon, da.
    /// </summary>
    public class AnimatedUserControl : UserControl
    {
        
        private ContentControl ContentArea; // Contains the User Control, used to switch content after FadeOutAnimation is finished
        private DoubleAnimation FadeOutAnimation; // Animation for fading out
        private DoubleAnimation FadeInAnimation; // Animation for fading in
        private AnimatedUserControl NewContent; // NewContent is given a value when the content is being changed

        public AnimatedUserControl() : base()
        {
            // Needs default constructor for UserControl to work properly
        }
        public AnimatedUserControl(DesktopGUI gui) : base()
        {
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
