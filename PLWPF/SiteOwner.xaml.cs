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
using System.Windows.Shapes;
using System.Net;
using System.Net.Mail;
using BE;
using BL;
using System.Timers;
using System.Threading;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for SiteOwner.xaml
    /// </summary>
    public partial class SiteOwner : Window
    {
        BL.IBL bl;

        public SiteOwner()
        {
            InitializeComponent();
            //Thread T = new Thread(updetListOrders);
            //T.Start();
            bl = BL.FactoryBL.GetBL();
            NavigationService.NavigationStack.Push(this);
            SiteOwner1.Passwords = Configuration.passwords;


        }

        private void Button_Click_Enter(object sender, RoutedEventArgs e)

        {
            if(PSW.Password== SiteOwner1.Passwords.ToString())
            GrSO.Visibility = Visibility.Visible;
            else
                MessageBox.Show($"הסיסמא שהוקשה אינה תקינה אנה נסה שנית ", "בעל האתר", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        private void Btn_GR_Click(object sender, RoutedEventArgs e)
        {
            LinqGR win = new LinqGR();
            win.Show();
            this.Visibility = Visibility.Collapsed;
        }

        private void BtnHU_Button_Click(object sender, RoutedEventArgs e)
        {
            LinqHu win = new LinqHu();
            win.Show();
            this.Visibility = Visibility.Collapsed;
        }

        private void BtnHost_Button_Click(object sender, RoutedEventArgs e)
        {
            LinqHost win = new LinqHost();
            win.Show();
            this.Visibility = Visibility.Collapsed;
        }

  
        void BackButton_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.NavigateBack();
        }


        private void ForgetPsw_Click(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            Configuration.passwords = rnd.Next(100000001, 1000000000);
            SiteOwner1.Passwords = Configuration.passwords;

            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient();

                mail.To.Add("vacationodeliaruthi@gmail.com");
                mail.From = new MailAddress("odeliamel04@gmail.com");
                mail.Subject = "סיסמא חדשה לאתר";
                mail.IsBodyHtml = false;
                mail.Body = SiteOwner1.Passwords.ToString();
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.Host = "smtp.gmail.com";
                SmtpServer.Credentials = new System.Net.NetworkCredential("vacationodeliaruthi@gmail.com", "308508rc");
                SmtpServer.EnableSsl = false;
                SmtpServer.Send(mail);
                MessageBox.Show($"סיסמתך החדשה נישלחה אליך למייל בהצלחה", "Site Owner", MessageBoxButton.OK, MessageBoxImage.None);
                bl.updatepasswords();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }
        /////
        //private void updetListOrders()
        //{
        //    while (true)
        //    {

        //        OrderDailyMethod();
        //        ReqDailyMethod();
        //        Thread.Sleep(86400000);//24 שעות 
        //    }
        //}
        //private void ReqDailyMethod()
        //{
        //    IEnumerable<GuestRequest> listOfreq = bl.DaysPassedOnReq(31);
        //    List<GuestRequest> g = new List<GuestRequest>();
        //    foreach (GuestRequest o in listOfreq)
        //    {
        //        g.Add(o);
        //    }
        //    g.ForEach(element => element.Status = Status.Closed);
        //    g.ForEach(element => bl.Updategr(element));
        //}

        //private void OrderDailyMethod()
        //{
        //    IEnumerable<Order> listOfOrder = bl.DaysPassedOnOrders(31);
        //    List<Order> ord = new List<Order>();
        //    foreach (Order o in listOfOrder)
        //    {
        //        ord.Add(o);
        //    }
        //    ord.ForEach(element => element.Status = OrderStatus.נסגרה_עסקה);
        //    ord.ForEach(element =>bl.UpdateOrder(element));
        //}
        private void commision_Button_Click(object sender, RoutedEventArgs e)
        {
            btn_commition.Content =bl.Accumulation();
        }
    }
}
