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
    /// Interaction logic for DeleteHostingUnit.xaml
    ///  GuestRequest guestRequest;
    /// </summary>
    
    public partial class DeleteHostingUnit : Window
    {
        //GuestRequest ;
        BL.IBL bl;
        Host host = new Host();
        public DeleteHostingUnit(Host H)
        {
            bl = BL.FactoryBL.GetBL();
            host = H;
            this.DataContext = host;
            InitializeComponent();
            NavigationService.NavigationStack.Push(this);
            IEnumerable<string> nameHu = bl.NameOfUnit(host);
            this.NameHu.ItemsSource = nameHu;


            InitializeComponent();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                    bl.RemoveHostingUnitB(bl.Host_ToHostingUnit(host, NameHu.SelectedItem.ToString()));
                MessageBox.Show($" היחידת אירוח נמחקה בהצלחה ", " מחיקת יחידת אירוח", MessageBoxButton.OK, MessageBoxImage.None);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
              
        }
        void BackButton_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.NavigateBack();
        }
    }
}
