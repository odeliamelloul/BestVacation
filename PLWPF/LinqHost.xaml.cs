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
    /// Interaction logic for LinqHost.xaml
    /// </summary>
    public partial class LinqHost : Window
    {
        BL.IBL bl;

      public LinqHost()
        {
            InitializeComponent();
            NavigationService.NavigationStack.Push(this);
            bl = BL.FactoryBL.GetBL();
        }

        private void Host_Click(object sender, RoutedEventArgs e)
        {
            ListView_Host.ItemsSource = bl.Get_Host();
        }
        void BackButton_Click(object sender, RoutedEventArgs e)

        {
            NavigationService.NavigateBack();
        }
    }
}
