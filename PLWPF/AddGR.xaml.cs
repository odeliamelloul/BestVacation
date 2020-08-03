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
using System.Windows.Interop;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for AddGR.xaml
    /// </summary>
 

    public partial class AddGR : Window
    {
        GuestRequest guestRequest;
        BL.IBL bl;
        bool Click1;
        public AddGR()
        {

            InitializeComponent();
            NavigationService.NavigationStack.Push(this);
            bl = BL.FactoryBL.GetBL();
            guestRequest = new GuestRequest();
            Click1 = false;

          this.poolCB.ItemsSource = Enum.GetValues(typeof(BE.ChoosingAttraction));
          this.jakouziCB.ItemsSource = Enum.GetValues(typeof(BE.ChoosingAttraction));
          this.GardenCB.ItemsSource = Enum.GetValues(typeof(BE.ChoosingAttraction));
          this.attractionCB.ItemsSource = Enum.GetValues(typeof(BE.ChoosingAttraction));
          this.TypeHostingUnitCB.ItemsSource = Enum.GetValues(typeof(BE.TypesOfVacation));
          this.AreaCB.ItemsSource = Enum.GetValues(typeof(BE.AreasInTheCountry));
           
        }
        int countAdd=0;

        public void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            countAdd++;


            if (poolCB.SelectedItem == null || jakouziCB.SelectedItem == null || GardenCB.SelectedItem == null ||
                attractionCB.SelectedItem == null || TypeHostingUnitCB == null)
            {
                MessageBox.Show($" עליך לבחור את כל האפשרויות ", "Guest Request", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
            if ((id.Text).Length != 9)
            {

                id.BorderBrush = Brushes.Red;
                IDlabel.Content = "הכנס ת.ז בעלת 9 תווים";
                IDlabel.BorderBrush = Brushes.Red;
                return;
            }
            if (PN.Text == "")
            {

                PN.BorderBrush = Brushes.Red;
                PnLabel.Content = "הכנס שם פרטי";
                PnLabel.BorderBrush = Brushes.Red;
                return;
            }
            if (FN.Text == "")
            {
                FN.BorderBrush = Brushes.Red;
                FnLabel.Content = "הכנס שם משפחה";
                FnLabel.BorderBrush = Brushes.Red;
               return;
            }
            if (int.Parse(txtNumAdult.Text) < 1)
            {
                FamilyTxt.BorderBrush = Brushes.Red;
                    return;
            }
            if (int.Parse(txtNumRoom.Text) < 1)
            {
                RoomTxt.BorderBrush = Brushes.Red;
                    return;
             }

                if (BreakfastCB.IsChecked == true)

                    guestRequest.Breakfast = true;
                else
                { guestRequest.Breakfast = false; }

                if (LunchCB.IsChecked == true)

                    guestRequest.Lunch = true;
                else
                { guestRequest.Lunch = false; }

                if (DinnerCB.IsChecked == true)

                    guestRequest.Dinner = true;
                else
                { guestRequest.Dinner = false; }

                guestRequest.Status = OrderStatus.לא_טופל;
                guestRequest.RegistrationDate = DateTime.Now;

                guestRequest.Pool = (ChoosingAttraction)Enum.Parse(typeof(ChoosingAttraction), poolCB.SelectedItem.ToString(), true);
                guestRequest.Jacuzzi = (ChoosingAttraction)Enum.Parse(typeof(ChoosingAttraction), jakouziCB.SelectedItem.ToString(), true);
                guestRequest.Garden = (ChoosingAttraction)Enum.Parse(typeof(ChoosingAttraction), GardenCB.SelectedItem.ToString(), true);
                guestRequest.ChildrensAttractions = (ChoosingAttraction)Enum.Parse(typeof(ChoosingAttraction), attractionCB.SelectedItem.ToString(), true);
                guestRequest.Type = (TypesOfVacation)Enum.Parse(typeof(TypesOfVacation), TypeHostingUnitCB.SelectedItem.ToString(), true);
                guestRequest.SubArea = Subarea();
                guestRequest.Area = (AreasInTheCountry)Enum.Parse(typeof(AreasInTheCountry), AreaCB.SelectedItem.ToString(), true);
                guestRequest.FamilyName = FN.Text;
                guestRequest.PrivateName = PN.Text;
                guestRequest.MailAddress = Email.Text;
                guestRequest.EntryDate = EntryDateDP.SelectedDate.Value.Date;
                guestRequest.ReleaseDate = RealeseDateDP.SelectedDate.Value.Date;
                guestRequest.ID = int.Parse(id.Text);
                guestRequest.Adults = int.Parse(txtNumAdult.Text);
                guestRequest.Children = int.Parse(txtNumChild.Text);
                guestRequest.Room = int.Parse(RoomTxt.Text);


                bl.AddGuestRequestB(guestRequest);
                vi.Visibility = Visibility.Visible;
                addlable.Visibility = Visibility.Visible;

                if (countAdd > 1)
                {
                    bl.UpdateGuestRequestB(guestRequest);
                    MessageBox.Show("פרטיך עודכנו במערכת");
                }

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            
            
        }

        #region
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

        #endregion

        #region 
        private int _numValue2 = 0;
        public int NumValue2
        {
            get { return _numValue2; }
            set
            {
                _numValue2 = value;   
                txtNumAdult.Text = _numValue2.ToString();
                int x = value + _numValue3;
                FamilyTxt.Content = x.ToString();
                

            }
        }
        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {
            if (txtNumAdult == null)
            {
                return;
            }

            if (!int.TryParse(txtNumAdult.Text, out _numValue2))
                txtNumAdult.Text = _numValue2.ToString();
          
        }

        private void Minus_Click2(object sender, RoutedEventArgs e)
        {
            if (NumValue2 > 0)
            { NumValue2--; }
            else NumValue2 = 0;
        }
        private void Plus_Click2(object sender, RoutedEventArgs e)
        {
            NumValue2++;
        }
        #endregion

        #region
        private int _numValue3 = 0;
        public int NumValue3
        {
            get { return _numValue3; }
            set
            {
                _numValue3 = value;
                txtNumChild.Text = _numValue3.ToString();
                int x = value + _numValue2;
                FamilyTxt.Content = x.ToString();
            }
        }
        private void TextBox_TextChanged_3(object sender, TextChangedEventArgs e)
        {
            if (txtNumChild == null)
            {
                return;
            }

            if (!int.TryParse(txtNumChild.Text, out _numValue3))

                txtNumChild.Text = _numValue3.ToString();

        }
       

        private void Minus_Click3(object sender, RoutedEventArgs e)
        {
            if (NumValue3 > 0)
            { NumValue3--; }
            else NumValue3 = 0;
        }
        private void Plus_Click3(object sender, RoutedEventArgs e)
        {
            NumValue3++;
        }

        #endregion
        private void SubAreaCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AreaBtn.Content= SubAreaCB.SelectedItem.ToString();
            if (SubAreaCB.SelectedItem != null) { gr3.Visibility = Visibility.Collapsed; }
        }
        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
         
        }
        private void ComboBox_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
        {

        }
        private void ComboBox_SelectionChanged_3(object sender, SelectionChangedEventArgs e)
        {

        }
        private void ComboBox_SelectionChanged_4(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Got_Focus_PN(object sender, RoutedEventArgs e)
        {
            PnLabel.Content = "";
            PN.BorderBrush = Brushes.Black;
        }

        private void Got_Focus_FN(object sender, RoutedEventArgs e)
        {
            FnLabel.Content = "";
            FN.BorderBrush = Brushes.Black;
        }
    
        private void Got_Focus_Mail(object sender, RoutedEventArgs e)
        {
            MailLabel.Content = "";
            Email.BorderBrush = Brushes.Black;
        }

        private void Id_GotFocus(object sender, RoutedEventArgs e)
        {
            IDlabel.Content = "";
            id.BorderBrush = Brushes.Black;
        }
        void BackButton_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.NavigateBack();
        }
  

        private void PN_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AreaCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SubAreaCB.Visibility = Visibility.Visible;

            switch (AreaCB.SelectedValue.ToString()) {

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
       private All Subarea()
        {

            switch (AreaCB.SelectedValue.ToString())
            {
                case ("הכל"):
                    SubAreaCB.SelectedItem = "{Binding Path=All,Mode=TwoWay}";
                    this.SubAreaCB.ItemsSource = Enum.GetValues(typeof(BE.All));
                    break;

                case ("ירושלים"):
                    if (SubAreaCB.SelectedValue.ToString() == "בית_וגן")
                        return All.בית_וגן;
                    if (SubAreaCB.SelectedValue.ToString() == "מרכז_העיר")
                        return All.מרכז_העיר;
                    if (SubAreaCB.SelectedValue.ToString() == "תלפיות")
                        return All.תלפיות;
                    if (SubAreaCB.SelectedValue.ToString() == "גילו")
                        return All.גילו;
                    if (SubAreaCB.SelectedValue.ToString() == "העיר_העתיקה")
                        return All.העיר_העתיקה;
                    if (SubAreaCB.SelectedValue.ToString() == "מרכז_העיר")
                        return All.מרכז_העיר;
                    if (SubAreaCB.SelectedValue.ToString() == "הכל")
                        return All.ירושלים;
                    break;

                case ("צפון"):
                    if (SubAreaCB.SelectedValue.ToString() == "קיריית_שמונה")
                        return All.קיריית_שמונה;
                    if (SubAreaCB.SelectedValue.ToString() == "גליל_עליון")
                        return All.גליל_עליון;
                    if (SubAreaCB.SelectedValue.ToString() == "כרמיאל")
                        return All.כרמיאל;
                    if (SubAreaCB.SelectedValue.ToString() == "צפת")
                        return All.צפת;
                    if (SubAreaCB.SelectedValue.ToString() == "עפולה")
                        return All.עפולה;
                    if (SubAreaCB.SelectedValue.ToString() == "בית_שאן")
                        return All.בית_שאן;
                    if (SubAreaCB.SelectedValue.ToString() == "נהריה")
                        return All.נהריה;
                    if (SubAreaCB.SelectedValue.ToString() == "טבריה")
                        return All.טבריה;
                    if (SubAreaCB.SelectedValue.ToString() == "הכל")
                        return All.צפון;
                    break;
                case ("דרום"):

                    if (SubAreaCB.SelectedValue.ToString() == "נתיבות")
                        return All.נתיבות;
                    if (SubAreaCB.SelectedValue.ToString() == "קריית_גת")
                        return All.קריית_גת;
                    if (SubAreaCB.SelectedValue.ToString() == "ערד")
                        return All.ערד;
                    if (SubAreaCB.SelectedValue.ToString() == "אופקים")
                        return All.אופקים;
                    if (SubAreaCB.SelectedValue.ToString() == "אשקלון")
                        return All.אשקלון;
                    if (SubAreaCB.SelectedValue.ToString() == "אשדוד")
                        return All.אשדוד;
                    if (SubAreaCB.SelectedValue.ToString() == "באר_שבע")
                        return All.באר_שבע;
                    if (SubAreaCB.SelectedValue.ToString() == "אילת")
                        return All.אילת;
                    if (SubAreaCB.SelectedValue.ToString() == "הכל")
                        return All.דרום;
                    break;

                case ("מרכז"):

                    if (SubAreaCB.SelectedValue.ToString() == "ראשון_לציון")
                        return All.ראשון_לציון;
                    if (SubAreaCB.SelectedValue.ToString() == "גיבעתיים")
                        return All.גיבעתיים;
                    if (SubAreaCB.SelectedValue.ToString() == "אור_יהודה")
                        return All.אור_יהודה;
                    if (SubAreaCB.SelectedValue.ToString() == "פתח_תקווה")
                        return All.פתח_תקווה;
                    if (SubAreaCB.SelectedValue.ToString() == "נתניה")
                        return All.נתניה;
                    if (SubAreaCB.SelectedValue.ToString() == "אשדוד")
                        return All.אשדוד;
                    if (SubAreaCB.SelectedValue.ToString() == "קיריית_אונו")
                        return All.קיריית_אונו;
                    if (SubAreaCB.SelectedValue.ToString() == "רמת_גן")
                        return All.רמת_גן;
                    if (SubAreaCB.SelectedValue.ToString() == "תל_אביב")
                        return All.תל_אביב;
                    if (SubAreaCB.SelectedValue.ToString() == "הכל")
                        return All.מרכז;
                    break;
                 
            }
            return All.הכל;
        }
        private void FamilyBtn_Click(object sender, RoutedEventArgs e)
        {

            gr2.Visibility = Visibility.Visible; 
          
        }
  
        private void Date_Click(object sender, RoutedEventArgs e)
        {

             gr5.Visibility = Visibility.Visible; 
            

        }

        
        private void Area_Click(object sender, RoutedEventArgs e)
        {
            gr3.Visibility = Visibility.Visible; 
            
        }

        
        private void Room_Button_Click(object sender, RoutedEventArgs e)
        {
          gr4.Visibility = Visibility.Visible; 
            
        }

        private void RealeseDateChanged(object sender, RoutedEventArgs e)
        {
            Click1 = true;
            TxtDateOut.Content = RealeseDateDP.SelectedDate.Value.Date.ToString();
            TimeSpan x = RealeseDateDP.SelectedDate.Value.Date - EntryDateDP.SelectedDate.Value.Date;
            calendarTxt.Content = x.Days.ToString();
            if (RealeseDateDP.SelectedDate != null && EntryDateDP.SelectedDate != null)
                gr5.Visibility = Visibility.Collapsed;
             
        }



        private void EntryDateChanged(object sender, RoutedEventArgs e)
        {

            TxtDateIn.Content = EntryDateDP.SelectedDate.Value.Date.ToString();
            if (Click1 == true)
            {
                TimeSpan x = RealeseDateDP.SelectedDate.Value.Date - EntryDateDP.SelectedDate.Value.Date;
                calendarTxt.Content = x.Days.ToString();
            }
            if (RealeseDateDP.SelectedDate != null && EntryDateDP.SelectedDate != null)
                gr5.Visibility = Visibility.Collapsed;
        }

        private void gr4_MouseLeave(object sender, RoutedEventArgs e)
        {
            gr4.Visibility = Visibility.Collapsed;
        }

        private void gr2_MouseLeave(object sender, RoutedEventArgs e)
        {
            gr2.Visibility = Visibility.Collapsed;
        }


    }

}

