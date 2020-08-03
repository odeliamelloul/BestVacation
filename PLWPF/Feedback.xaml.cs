using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for feedback.xaml
    /// </summary>
    public partial class Feedback : Window
    {
        public Feedback()
        {
            InitializeComponent();
            NavigationService.NavigationStack.Push(this);
        }
        void BackButton_Click(object sender, RoutedEventArgs e)

        { 
            NavigationService.NavigateBack();
        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient();
                mail.To.Add("vacationodeliaruthi@gmail.com");
                mail.From = new MailAddress("vacationodeliaruthi@gmail.com");
                mail.Subject = "feedback to my site";
                mail.IsBodyHtml = false;
                mail.Body = "הערות לאתר" +" "+
                             txt.Text.ToString();
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.Host = "smtp.gmail.com";
                SmtpServer.Credentials = new System.Net.NetworkCredential("vacationodeliaruthi@gmail.com", "308508rc");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
                vi.Visibility = Visibility.Visible;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
