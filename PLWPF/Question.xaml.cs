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

namespace PLWPF
{
 
    public partial class Question : Window
    {
        public Question()
        {
            InitializeComponent();
            NavigationService.NavigationStack.Push(this);
        }
        void BackButton_Click(object sender, RoutedEventArgs e)
        {
          
            NavigationService.NavigateBack();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Feedback win = new Feedback();
            win.Show();
            this.Visibility=Visibility.Collapsed;
        }
    }
}
