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
using BL;
using BE;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for PrivateHost.xaml
    /// </summary>
    public partial class PrivateHost : Window
    {
        BL.IBL bl;
        Host H = new Host();
        Order order = new Order();

        public PrivateHost()
        {
            InitializeComponent();
            NavigationService.NavigationStack.Push(this);

            bl = BL.FactoryBL.GetBL();
           
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IdTxt.Text.Length == 9 ) 
                {
                    grHost.Visibility = Visibility.Visible;
                    H = bl.CheckId(int.Parse(IdTxt.Text));     
                }
                else
                { 
                    
                    throw new FormatException("הכנס ת.ז בעלת 9 ספרות");
                }
            }
            catch (Exception exp)
            {
                grHost.Visibility = Visibility.Collapsed;
                MessageBox.Show(exp.Message);

            }
        }
        void BackButton_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.NavigateBack();
        }
        private void Update_Button_Click(object sender, RoutedEventArgs e)
        {
          
            UpdateHostingUnit Uhu = new UpdateHostingUnit(H);
            Uhu.Show();
            this.Visibility = Visibility.Collapsed;
        }
        private void OrderBtn_Upd_Click(object sender, RoutedEventArgs e)
        {

            UpdateOrder gso = new UpdateOrder(H);
            gso.Show();
            this.Visibility = Visibility.Collapsed;
        }
        private void Order_Button_Click(object sender, RoutedEventArgs e)
        {
            grOrder.Visibility = Visibility.Visible;
        }
        private void OrderBtn_Add_Click(object sender, RoutedEventArgs e)
        {
            AddOrder AO = new AddOrder(H);
            AO.Show();
            this.Visibility = Visibility.Collapsed;
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            DeleteHostingUnit Dhu = new DeleteHostingUnit(H);
            Dhu.Show();
            this.Visibility = Visibility.Collapsed;

        }

        private void IdTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
          
        }
        private void grOrder_MouseLeave(object sender, RoutedEventArgs e)
        {
            grOrder.Visibility = Visibility.Collapsed;
        }
        private void Button_Click_AddHU(object sender, RoutedEventArgs e)
        {
            AddHostingUnitW w1 = new AddHostingUnitW(H);
            w1.Show();
            this.Visibility = Visibility.Collapsed;
        }
    }
}
