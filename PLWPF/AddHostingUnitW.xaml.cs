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
using Microsoft.Win32;
using BE;
using BL;
using System.Collections.ObjectModel;

namespace PLWPF
{

    /// <summary>
    /// Interaction logic for AddHostingUnitW.xaml
    /// </summary>
    /// 
    public partial class AddHostingUnitW : Window
    {
        HostingUnit hostingUnit = new HostingUnit();
        BL.IBL bl;
        Host host;
        OpenFileDialog op;
        int i ;
        

        public AddHostingUnitW(Host h)
        {
            
            host = h;
            InitializeComponent();
            NavigationService.NavigationStack.Push(this);
            bl = BL.FactoryBL.GetBL();
            this.AreaCB.ItemsSource = Enum.GetValues(typeof(BE.AreasInTheCountry));
            this.TypeHostingUnitCB.ItemsSource = Enum.GetValues(typeof(BE.TypesOfVacation));
            hostingUnit.CntImg= 0;
            i = 0;
            DataContext = this;
           
        }




        private void Button_Click_UploadImage(object sender, RoutedEventArgs e)
        {
            
            op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            op.Multiselect = true;

            if (op.ShowDialog() == true)
            {
                paste(op.FileNames.Select(f => new MyPicture
                {
                    Url = new Uri(f, UriKind.Absolute),
                    Title = System.IO.Path.GetFileName(f)
                }));


            }
            hostingUnit.CntImg = ListBox1.Items.Count;
            
        }


        public class MyPicture
        {
            public Uri Url { get; set; } // filename of image
            public string Title { get; set; }
        }

        public ObservableCollection<MyPicture>MyPictures{ get; } = new ObservableCollection<MyPicture>();

        private void paste(IEnumerable<MyPicture> newPictures)
        {
           
            foreach (var item in newPictures)
            {
                MyPictures.Add(item);
            }

        }

        private void AreaCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            SubAreaCB.Visibility = Visibility.Visible;


            switch (AreaCB.SelectedValue.ToString())
            {

                case ("הכל"):
                    SubAreaCB.SelectedItem = "{Binding Path=All,Mode=TwoWay}";
                    this.SubAreaCB.ItemsSource = Enum.GetValues(typeof(BE.All));
                    break;

                case ("ירושלים"):
                    SubAreaCB.SelectedItem = "{Binding Path=Jerusalem,Mode=TwoWay}";
                    this.SubAreaCB.ItemsSource = Enum.GetValues(typeof(BE.Jerusalem));
                    break;

                case ("צפון"):
                    SubAreaCB.SelectedItem = "{Binding Path=North,Mode=TwoWay}";
                    this.SubAreaCB.ItemsSource = Enum.GetValues(typeof(BE.North));
                    break;
                case ("דרום"):
                    SubAreaCB.SelectedItem = "{Binding Path=South,Mode=TwoWay}";
                    this.SubAreaCB.ItemsSource = Enum.GetValues(typeof(BE.South));
                    break;
                case ("מרכז"):
                    SubAreaCB.SelectedItem = "{Binding Path=Center,Mode=TwoWay}";
                    this.SubAreaCB.ItemsSource = Enum.GetValues(typeof(BE.Center));
                    break;
            }
        }

        private void SubAreaCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AreaBtn.Content = SubAreaCB.SelectedItem.ToString();
            if (SubAreaCB.SelectedItem != null) { gr3.Visibility = Visibility.Collapsed; }
        }



        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            if (poolCB.IsChecked == true)
                hostingUnit.Pool = true;
            else { hostingUnit.Pool = false; }

            if (jakouziCB.IsChecked == true)
                hostingUnit.Jacuzzi = true;
            else { hostingUnit.Jacuzzi = false; }

            if (GardenCB.IsChecked == true)
                hostingUnit.Garden = true;
            else { hostingUnit.Garden = false; }

            if (AttractionCB.IsChecked == true)
                hostingUnit.ChildrensAttractions = true;
            else { hostingUnit.ChildrensAttractions = false; }

            if (BreakfastCB.IsChecked == true)
                hostingUnit.Breakfast = true;
            else { hostingUnit.Breakfast = false; }

            if (LunchCB.IsChecked == true)
                hostingUnit.Lunch = true;
            else { hostingUnit.Lunch = false; }

            if (DinnerCB.IsChecked == true)
                hostingUnit.Dinner = true;
            else { hostingUnit.Dinner = false; }

            hostingUnit.Type = (TypesOfVacation)Enum.Parse(typeof(TypesOfVacation), TypeHostingUnitCB.SelectedItem.ToString(), true);
            hostingUnit.SubArea = (All)Enum.Parse(typeof(All), SubAreaCB.SelectedItem.ToString(), true);
            hostingUnit.Area = (AreasInTheCountry)Enum.Parse(typeof(AreasInTheCountry), AreaCB.SelectedItem.ToString(), true);
            hostingUnit.HostingUnitName = HuName.Text;
            hostingUnit.Adults = int .Parse(SumAdults.Text);
            hostingUnit.Children = int.Parse(SumChids.Text);
            hostingUnit.PhoneNumber= int.Parse(Phone.Text);
            hostingUnit.NumOfStars = int.Parse(txtValue.Text);
            try
            {

                if (HuName.Text == "")
                {
                    HuName.BorderBrush = Brushes.Red;
                    NLabel.Content = "הכנס שם פרטי";
                    NLabel.BorderBrush = Brushes.Red;
                }
                hostingUnit.Owner =host;
                hostingUnit.Room = int.Parse(RoomTxt.Text);

                bl.AddHostingUnitB(hostingUnit);
                upd.Visibility = Visibility.Visible;
                vi.Visibility = Visibility.Visible;

                foreach (string filename in op.FileNames)
                {
                    i++;
                    bl.AddhostingUnitImage(hostingUnit.HostingUnitKey, filename, i);
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            } 

        }
        private int _numValue1 = 0;

        public int NumValue1
        {
            get { return _numValue1; }
            set
            {
                _numValue1 = value;
                txtNumRoom.Text = value.ToString();
                RoomTxt.Text = value.ToString();
            }
        }
        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            if (txtNumRoom == null)
            {
                return;
            }

            if (!int.TryParse(txtNumRoom.Text, out _numValue1))
            {
                txtNumRoom.Text = _numValue1.ToString();

            }


        }
        private void Area_Click(object sender, RoutedEventArgs e)
        {
            gr3.Visibility = Visibility.Visible;
        }
        private void Minus_Click1(object sender, RoutedEventArgs e)
        {
            if (NumValue1 > 0)
            { NumValue1--; }
            else NumValue1 = 0;

        }
        private void Plus_Click1(object sender, RoutedEventArgs e)
        {
            NumValue1++;
        }
        private void Id_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void RoomTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (RoomTxt == null)
            {
                return;
            }

            if (!int.TryParse(RoomTxt.Text, out _numValue1))
            {
                RoomTxt.Text = _numValue1.ToString();

            }
        }

        void BackButton_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.NavigateBack();
        }


        private void Room_Button_Click(object sender, RoutedEventArgs e)
        {
            gr4.Visibility = Visibility.Visible;
        }
        private void Room_MouseLeave(object sender, RoutedEventArgs e)
        {
            gr4.Visibility = Visibility.Collapsed;
        }


        private void startext(object sender, TextChangedEventArgs e)
        {
            try
            {
                ratings1.Value = Decimal.Parse(txtValue.Text);
                ratings1.NumberOfStars = int.Parse(txtValue.Text);
                ratings1.BackgroundColor = Brushes.White;
                ratings1.StarForegroundColor = Brushes.Orange;
                ratings1.StarOutlineColor = Brushes.DarkGray;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Try typing some correct numbers, and give it a chance");
            }
        }
    }

        
  
}
