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
    /// Interaction logic for GetHuName.xaml
    /// </summary>
    /// 
    public partial class GetHuName : Window
    {
        BL.IBL bl;
        Host host = new Host();
        GuestRequest guestRequest = new GuestRequest();
        HostingUnit hostingUnit = new HostingUnit();
        Order order;

        public GetHuName(Host H,GuestRequest g)
        {
           bl = BL.FactoryBL.GetBL();
            host = H;
            guestRequest = g;
            order = new Order();
            InitializeComponent();
            NavigationService.NavigationStack.Push(this);
            IEnumerable<string> nameHu = bl.NameOfUnit(host);
            this.NameHu.ItemsSource = nameHu;
        }

        private void NameHuCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
        void BackButton_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.NavigateBack();
        }
        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            order.HostingUnitKey = hostingUnit.HostingUnitKey;
            order.GuestRequestKey = guestRequest.GuestRequestKey;
            order.CreateDate = DateTime.Now;
            order.Status = OrderStatus.לא_טופל;
            
            

            try
            {
                bl.sumAdult(guestRequest, hostingUnit);
                 bl.sumChilds(guestRequest, hostingUnit);
                if (guestRequest.SubArea != All.הכל) 
                   if (hostingUnit.SubArea != guestRequest.SubArea)
                      throw new KeyNotFoundException(" האזור של היחידת האירוח אינו תואם לאיזור דרישת הלקוח");
                bl.AddOrderB(host, hostingUnit, order);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            this.Close();
        }
        
    }
}
