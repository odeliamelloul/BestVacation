using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for AddOrder.xaml
    /// </summary>
    /// 

    public partial class AddOrder : Window
    {
        BL.IBL bl;
        GuestRequest g;
        Host host;
        Order order;
        HostingUnit hostingUnit = new HostingUnit();


        IEnumerable<GuestRequest> guestRequest;
        public AddOrder(Host H)
        {
            bl = BL.FactoryBL.GetBL();
            guestRequest = new ObservableCollection<GuestRequest>();
            g = new GuestRequest();
            host = H;
            this.DataContext = host;
            InitializeComponent();
            NavigationService.NavigationStack.Push(this);

            IEnumerable<string> nameHu = bl.NameOfUnit(host);
            this.NameHu.ItemsSource = nameHu;
        }
        private bool button1WasClicked = false;


        private void Button_Click_All(object sender, RoutedEventArgs e)
        {
            button1WasClicked = false;
            ListView_GR.ItemsSource = bl.Get_GuestRequestListB();
           
        }
            private void Button_Click_HU(object sender, RoutedEventArgs e)
            {
            
            NameHu.Visibility = Visibility.Visible;
            }

        void BackButton_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.NavigateBack();
        }

        private void NameHuCB_SelectionChanged(object sender, SelectionChangedEventArgs e)

        {
            button1WasClicked = true;
            guestRequest = bl.Get_GuestRequestListB();
            hostingUnit = bl.Host_ToHostingUnit(host, NameHu.SelectedItem.ToString());
            GuestRequest G = new GuestRequest();

            if (hostingUnit.Pool == false)
             guestRequest = bl.Condition_Guest_Request1(bl.PoolF, guestRequest); 
            if (hostingUnit.Garden == false)
            guestRequest = bl.Condition_Guest_Request1(bl.GardenF, guestRequest); 
            if (hostingUnit.Jacuzzi == false)
            guestRequest = bl.Condition_Guest_Request1(bl.GardenF, guestRequest); 
            if (hostingUnit.ChildrensAttractions == false)
            guestRequest = bl.Condition_Guest_Request1(bl.AttractionF, guestRequest); 
             if (hostingUnit.Breakfast == false)
            guestRequest = bl.Condition_Guest_Request1(bl.Breakfast, guestRequest);
            if (hostingUnit.Lunch == false)
             guestRequest = bl.Condition_Guest_Request1(bl.Lunch, guestRequest);
            if (hostingUnit.Dinner == false)
            guestRequest = bl.Condition_Guest_Request1(bl.Dinner, guestRequest);
            if (hostingUnit.Children > 0)
            { 
                guestRequest = bl.Condition_Guest_Request2(g => g.Children <= hostingUnit.Children, guestRequest);
            }
            if (hostingUnit.Adults > 0)
            {
                guestRequest = bl.Condition_Guest_Request2(g => g.Adults <= hostingUnit.Children, guestRequest);
            }
            if (hostingUnit.Room > 0)
            {
                guestRequest = bl.Condition_Guest_Request2(g => g.Room <= hostingUnit.Children, guestRequest);
            }
            guestRequest = bl.Condition_Guest_Request2(g => g.Type == hostingUnit.Type, guestRequest);
            guestRequest = bl.Condition_Guest_Request2(g => g.SubArea == hostingUnit.SubArea, guestRequest);
            ListView_GR.ItemsSource = guestRequest;
            }
        


            private void SubAreaCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
            }



        private void MouseDoubleClick_ListView_GR(object sender, RoutedEventArgs e)
        {
            List<HostingUnit> hostingUnits = new List<HostingUnit>();
            if (((ListView)sender).SelectedItem != null)
            {
                if (button1WasClicked == true)
                {
                    order = new Order();
                    hostingUnits = bl.Host_ToHostingUnits(host);
                    GuestRequest g = new GuestRequest();
                    g = ((ListView)sender).SelectedItem as GuestRequest;

                    if (g.Status != OrderStatus.נשלח_מייל )
                    {
                        order.CreateDate = DateTime.Now;
                        order.Status = OrderStatus.נשלח_מייל;
                        order.HostingUnitKey = hostingUnit.HostingUnitKey;
                        order.GuestRequestKey = g.GuestRequestKey;

                        
                        try
                        {
                            //bl.sumAdult(g, hostingUnit);
                            //bl.sumChilds(g, hostingUnit);
                            if (g.SubArea != All.הכל)
                                if (hostingUnit.SubArea != g.SubArea)
                                    throw new KeyNotFoundException(" האזור של היחידת האירוח אינו תואם לאיזור דרישת הלקוח");
                            bl.AddOrderB(host, hostingUnit, order);
                        }
                   
                        catch (Exception exp)
                        {
                          MessageBox.Show(exp.Message);
                        }

                    }
                    else {MessageBox.Show("כבר נשלח מייל ללקוח זה"); }
                    
                }
                else
                {
                    GetHuName SO = new GetHuName(host, g);
                    SO.Show();
                    this.Visibility = Visibility.Collapsed;
                }
            }


        }



    }
}

