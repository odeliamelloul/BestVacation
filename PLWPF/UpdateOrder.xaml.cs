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
    /// Interaction logic for UpdateOrder.xaml
    /// </summary>
    public partial class UpdateOrder : Window
    {
        BL.IBL bl;
     
        Host host;
        HostingUnit hostingUnit;
        IEnumerable<Order> orders;
        Order o;
        public UpdateOrder(Host h)
        {
            host = h;
            InitializeComponent();
            NavigationService.NavigationStack.Push(this);
            bl = BL.FactoryBL.GetBL();
            IEnumerable<string> nameHu = bl.NameOfUnit(host);
            this.NameHu.ItemsSource = nameHu;//לcombobox מכניס את הערכים  
        }

        private void NameHuCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            hostingUnit = bl.Host_ToHostingUnit(host, NameHu.SelectedItem.ToString());
            orders = bl.UnitToOrder(hostingUnit);
            ListView_order.ItemsSource = orders;

        }
        private void MouseDoubleClick_ListView_GR(object sender, RoutedEventArgs e)
        {
            if (((ListView)sender).SelectedItem != null)
            {

                o = ((ListView)sender).SelectedItem as Order;
                GetStatusOrder WIN = new GetStatusOrder(host,o);
                WIN.Show();

                //ListView_order.ItemsSource = bl.UnitToOrder(hostingUnit);
            }
        }
        void BackButton_Click(object sender, RoutedEventArgs e)

        {
            NavigationService.NavigateBack();
        }
        private void ListView_GR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
