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
using BE;
using BL;
namespace PLWPF
{
    /// <summary>
    /// Interaction logic for AddHost.xaml
    /// </summary>
    public partial class AddHost : Window
    {

        Host host;
        BL.IBL bl;
        public AddHost()
        {
            InitializeComponent();
            NavigationService.NavigationStack.Push(this);
            bl = BL.FactoryBL.GetBL();
            host = new Host();
        }


        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((id.Text).Length != 9)
                {

                    id.BorderBrush = Brushes.Red;
                    IDlabel.Content = "הכנס ת.ז בעלת 9 תווים";
                    IDlabel.BorderBrush = Brushes.Red;
                    return;
                }
                else { IDlabel.BorderBrush =Brushes.Transparent; }
                if (PN.Text == "")
                {

                    PN.BorderBrush = Brushes.Red;
                    PnLabel.Content = "הכנס שם פרטי";
                    PnLabel.BorderBrush = Brushes.Red;
                    return;
                }
                if (FN.Text == "")
                {
                    FN.BorderBrush = Brushes.Red;
                    FnLabel.Content = "הכנס שם משפחה";
                    FnLabel.BorderBrush = Brushes.Red;
                    return;
                }
            host.FamilyName = FN.Text;
            host.PrivateName = PN.Text;
            host.MailAddress = Mail.Text;
            host.HostKey = int.Parse(id.Text);
            host.PhoneNumber = int.Parse(Phone.Text);
            //host.BankAccount = GetBank();
            bl.AddHostB(host);
            vi.Visibility = Visibility.Visible;
            addlable.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Host", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }
        //private BankBranch GetBank()
        //{
        //    BankBranch b= bl.Brunch_bankB().FirstOrDefault(x => x.BankNumber == int.Parse(BankNum.Text));
        //    b.BankAccountNumber = int.Parse(AccontNum.Text);

        //    return b;
        //}
        void BackButton_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.NavigateBack();
        }

        private void id_TextChanged(object sender, TextChangedEventArgs e)
        {
           
                //if (id.Text.Length == 9)
                //    host.HostKey = int.Parse(id.Text);
                //else throw new KeyNotFoundException("הכנס ת.ז בעלת 9 ספרות");
          
        }
    }
}
