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
    /// Interaction logic for LinqHu.xaml
    /// </summary>
    public partial class LinqHu : Window
    {
        BL.IBL bl;
       
        public LinqHu()
        {
            InitializeComponent();
            NavigationService.NavigationStack.Push(this);
            bl = BL.FactoryBL.GetBL();
           
        }

        private void AvailibleUnit_Click(object sender, RoutedEventArgs e)
        {
            if (EntryDate_DatePicker.SelectedDate != null && SumOFday.Text != "")
            {
                EntryDate_DatePicker.SelectedDate = null;
                SumOFday.Text = "";
            }
            Bhu.Visibility = Visibility.Visible;

        }

        private void AllUnit_Click(object sender, RoutedEventArgs e)
        {
            
                ListView_HU.ItemsSource = bl.Get_HostingUnitsListB();

        }

        private void BusyUnit_Click(object sender, RoutedEventArgs e)
        {
            if (EntryDate_DatePicker.SelectedDate != null && SumOFday.Text != "")
            { EntryDate_DatePicker.SelectedDate = null;
                SumOFday.Text = "";
            }
            BUSY.Visibility = Visibility.Visible;

        }


        void BackButton_Click(object sender, RoutedEventArgs e)

        {
            NavigationService.NavigateBack();
        }

        private void AvailibleClickCheck(object sender, RoutedEventArgs e)
        {
            if (EntryDate_DatePicker.SelectedDate!= null && SumOFday.Text != "")
                ListView_HU.ItemsSource = bl.AvailableUnitList(EntryDate_DatePicker.SelectedDate.Value.Date, int.Parse(SumOFday.Text));
            Bhu.Visibility = Visibility.Collapsed;
        }

        private void BusyClickCheck(object sender, RoutedEventArgs e)
        {
            if (EntryDate_DatePicker.SelectedDate != null && SumOFday.Text != "")
                ListView_HU.ItemsSource = bl.BusyUnitList(EntryDate_DatePicker.SelectedDate.Value.Date, int.Parse(SumOFday.Text));
            BUSY.Visibility = Visibility.Collapsed;
        }
    }
}
