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
    /// Interaction logic for GetStatusOrder.xaml
    /// </summary>
    public partial class GetStatusOrder : Window
    {
        BL.IBL bl;
        Order order;
        Host host;
        public GetStatusOrder(Host H,Order o)
        {

            InitializeComponent();
            NavigationService.NavigationStack.Push(this);
            this.ChangeStatus.ItemsSource = Enum.GetValues(typeof(BE.OrderStatus));
            host = H;
            order = o;
            bl = BL.FactoryBL.GetBL();
        }

        private void ChangeStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        void BackButton_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.NavigateBack();
        }
        private void Update_btn_Click(object sender, RoutedEventArgs e)
        {
            order.Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), ChangeStatus.SelectedItem.ToString(), true);

            try
            {
                bl.OrderChangedB(host,order);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message.ToString());
            }

            this.Close();
        }
    }
}

    
