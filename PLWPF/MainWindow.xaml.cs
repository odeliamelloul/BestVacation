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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool grGrLeave = false;
        bool HostLeave = false;
        //The timer that will handle the transitions
        private DispatcherTimer _tmr = new DispatcherTimer();
        //The list of images to loop through
        private List<BitmapImage> _lstImages = new List<BitmapImage>();
        //The list-index of the currently visible image
        private int _intCurrentImageIndex = 0;

        public MainWindow()
        {
            InitializeComponent();

            //Prepare a list of images
            _lstImages.Add(new BitmapImage(new Uri("/Resources/1000000511.jpg", UriKind.RelativeOrAbsolute)));
            _lstImages.Add(new BitmapImage(new Uri("/Resources/new 10 (2).jpg", UriKind.RelativeOrAbsolute)));
            _lstImages.Add(new BitmapImage(new Uri("/Resources/new 14 (2).jpg", UriKind.RelativeOrAbsolute)));
             _lstImages.Add(new BitmapImage(new Uri("/Resources/new 12 (2).jpg", UriKind.RelativeOrAbsolute)));
            _lstImages.Add(new BitmapImage(new Uri("/Resources/new 13 (2).jpg", UriKind.RelativeOrAbsolute)));
            _lstImages.Add(new BitmapImage(new Uri("/Resources/new 5 (3).jpg", UriKind.RelativeOrAbsolute)));
            _lstImages.Add(new BitmapImage(new Uri("/Resources/new 9 (2).jpg", UriKind.RelativeOrAbsolute)));
            //_lstImages.Add(new BitmapImage(new Uri("/Resources/הורדה.jpg", UriKind.RelativeOrAbsolute)));
            _lstImages.Add(new BitmapImage(new Uri("/Resources/new 8 (2).jpg", UriKind.RelativeOrAbsolute)));

            //Initialize the first Image-control (i.e. display the very first image)
            img1.Source = _lstImages[_intCurrentImageIndex];

            //Timer-setup
            _tmr.Interval = new TimeSpan(0, 0, 7);
            _tmr.Tick += new EventHandler(Timer_Tick);
            _tmr.Start();

            this.Closing += new System.ComponentModel.CancelEventHandler(Window_Closing);

            NavigationService.NavigationStack.Push(this);
            //SunTimes.CalculateSunRiseSetTimes(35.224,);
           

        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            grGR.Visibility = Visibility.Visible;

        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            grHu.Visibility = Visibility.Visible;

           
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {


        }

        private void Button_Click_PrivateHost(object sender, RoutedEventArgs e)
        {
            PrivateHost w1 = new PrivateHost();
            w1.Show();
            this.Visibility = Visibility.Collapsed;
        }

        private void enter(object sender, MouseEventArgs e)
        {
            Button b = sender as Button;
            if (b != null)
            {
                b.Content = b.Content;
            }

        }
        private void live(object sender, MouseEventArgs e)
        {

        }
        private void leaveHost(object sender, MouseEventArgs e)
        { 

        }
        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_AddGr(object sender, RoutedEventArgs e)
        {
            AddGR win = new AddGR();
            win.Show();
            this.Visibility = Visibility.Collapsed;
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            QuestionAnswer win = new QuestionAnswer();
            win.Show();
            this.Visibility = Visibility.Collapsed;
        }
        private void Button_Click_feed(object sender, RoutedEventArgs e)
        {
            Feedback win = new Feedback();
            win.Show();
            this.Visibility = Visibility.Collapsed;
        }

        private void Button_Click_(object sender, RoutedEventArgs e)
        {
            Feedback win = new Feedback();
            win.Show();
            this.Close();
        }
        private void grHu_MouseLeave(object sender, RoutedEventArgs e)
        {
            HostLeave = true;
            grHu.Visibility = Visibility.Collapsed;

        }
        private void grGR_MouseLeave(object sender, RoutedEventArgs e)
        {
            grGrLeave = true;
            grGR.Visibility = Visibility.Collapsed;
        }

        private void Button_Click_SiteOwner(object sender, RoutedEventArgs e)
        {
            SiteOwner SO = new SiteOwner();
            SO.Show();
            this.Visibility = Visibility.Collapsed;
        }
        public System.Collections.Specialized.NameValueCollection ServerVariables;
        private void Language_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_AddHost(object sender, RoutedEventArgs e)
        {
            AddHost Ah = new AddHost();
            Ah.Show();
            this.Visibility = Visibility.Collapsed;
        }
        private void Button_Click_UpdateGr(object sender, RoutedEventArgs e)
        {
            updateGuestRequest win = new updateGuestRequest();
            win.Show();
            this.Visibility = Visibility.Collapsed;
        }


            void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
            {
                //Clean up.
                _tmr.Stop();
                _tmr = null;
            }

            void Timer_Tick(object sender, EventArgs e)
            {
                Image imgSet;
                Image imgOther;
                Storyboard sb;

                //Get the image to work on, depending on which image is currently up front,
                //plus the Storyboard that'll fade in and roll down the current image and
                //hide the other one.
                if ((int)img1.GetValue(Panel.ZIndexProperty) == 1)
                {
                    imgSet = img2;
                    imgOther = img1;
                    sb = (Storyboard)this.FindResource("RollInImg2_FadeOutImg1");
                }
                else
                {
                    imgSet = img1;
                    imgOther = img2;
                    sb = (Storyboard)this.FindResource("RollInImg1_FadeOutImg2");
                }

                //Either get the next image's index or start over with the first image.
                if (_intCurrentImageIndex + 1 >= _lstImages.Count)
                    _intCurrentImageIndex = 0;
                else
                    _intCurrentImageIndex++;

                //Set the source of the image to fade in ...
                imgSet.Source = _lstImages[_intCurrentImageIndex];

                //Reverse the ZIndex of both images
                imgSet.SetValue(Panel.ZIndexProperty, 1);
                imgOther.SetValue(Panel.ZIndexProperty, 0);

                //The second animation targets the RenderTransform's TranslateTransform.
                //We want the image to roll down from the top to the bottom, so let's use the reversed value of 
                //the Window's ActualHeight to have the control be moved in from outside of the Window.
                DoubleAnimation da = (DoubleAnimation)sb.Children[1];
                //da.From = this.ActualHeight * -1;

            // When the new image start roll down from top to bottom, roll the existing image further down,
            // which will give the exact feel of continuously rolling image effect.
            DoubleAnimation da1 = (DoubleAnimation)sb.Children[2];
            da1.To = this.ActualHeight;
            //Start the transition
            sb.Begin();
            }
        }

}
