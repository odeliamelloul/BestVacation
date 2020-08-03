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
    /// Interaction logic for DeleteGR.xaml
    /// </summary>
    public partial class DeleteGR : Window
    {
        BL.IBL bl;
        GuestRequest g;
  

        public DeleteGR()
        {
            InitializeComponent();
            bl = BL.FactoryBL.GetBL();
            ListView_GR.ItemsSource = bl.Get_GuestRequestListB();
        }

        private void MouseDoubleClick_ListView_GR(object sender, RoutedEventArgs e)
        {
            if (((ListView)sender).SelectedItem != null)
            {
                g = new GuestRequest();
                g = ((ListView)sender).SelectedItem as GuestRequest;
                bl.RemoveGuestRequestB(g);
                MessageBox.Show("הלקוח נמחק מהמערכת");
                ListView_GR.ItemsSource = bl.Get_GuestRequestListB();
            }

        }
        void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.NavigateBack();

        }

    }
}
