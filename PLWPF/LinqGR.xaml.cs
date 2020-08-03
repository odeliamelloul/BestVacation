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
    /// Interaction logic for LinqGR.xaml
    /// </summary>
    public partial class LinqGR : Window
    {
        BL.IBL bl;
        public LinqGR()
        {
            InitializeComponent();
            NavigationService.NavigationStack.Push(this);
            bl = BL.FactoryBL.GetBL();
        }

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            ListView_GR.ItemsSource = bl.CloseGR();
        }

        private void NotTreated_Button_Click(object sender, RoutedEventArgs e)
        {
            ListView_GR.ItemsSource = bl.NoTreat();
        }
        void BackButton_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.NavigateBack();
        }
        private void MailHasSent_Click(object sender, RoutedEventArgs e)
        {
            ListView_GR.ItemsSource = bl.MailwasSent();
        }

        private void deleteGr1(object sender, RoutedEventArgs e)
        {
            DeleteGR win = new DeleteGR();
            win.Show();
            this.Visibility = Visibility.Collapsed;
        }
    }
}
