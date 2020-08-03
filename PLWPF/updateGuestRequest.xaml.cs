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
    /// Interaction logic for updateGuestRequest.xaml
    /// </summary>
    public partial class updateGuestRequest : Window
    {
        BL.IBL bl;
        GuestRequest gr ;
        bool Click1;
        public updateGuestRequest()
        {
            InitializeComponent();
            NavigationService.NavigationStack.Push(this);
            gr = new GuestRequest();
            bl = BL.FactoryBL.GetBL();
            Click1 = false;
            this.AreaCB.ItemsSource = Enum.GetValues(typeof(BE.AreasInTheCountry));
            this.poolCB.ItemsSource = Enum.GetValues(typeof(BE.ChoosingAttraction));
            this.GardenCB.ItemsSource = Enum.GetValues(typeof(BE.ChoosingAttraction));
            this.jakouziCB.ItemsSource = Enum.GetValues(typeof(BE.ChoosingAttraction));
            this.attractionCB.ItemsSource = Enum.GetValues(typeof(BE.ChoosingAttraction));
            this.TypeHostingUnitCB.ItemsSource = Enum.GetValues(typeof(BE.TypesOfVacation));
        }

        private void id_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        void BackButton_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.NavigateBack();
        }
        int countClickUpd= 0;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                if (id.Text.Length == 9)
                {
                    gr= bl.CheckGr(int.Parse(id.Text));
                    idGrid.Visibility = Visibility.Collapsed;
                    UpdateGrid.Visibility = Visibility.Visible;
                    
                    FN.Text = gr.FamilyName;
                    PN.Text = gr.PrivateName;
                    Email.Text = gr.MailAddress;
                    poolCB.SelectedItem = gr.Pool;
                    GardenCB.SelectedItem = gr.Garden;
                    jakouziCB.SelectedItem = gr.Jacuzzi;
                    attractionCB.SelectedItem = gr.ChildrensAttractions;
                    TypeHostingUnitCB.SelectedItem = gr.Type;
                    AreaBtn.Content = gr.SubArea;
                    RoomTxt.Text = gr.Room.ToString();
                    txtNumAdult.Text = gr.Adults.ToString();
                    txtNumChild.Text = gr.Children.ToString();
                    FamilyTxt.Content = (gr.Adults + gr.Children).ToString();
                    TxtDateIn.Content = gr.EntryDate;
                    TxtDateOut.Content = gr.ReleaseDate;
                    EntryDateDP.SelectedDate = gr.EntryDate;
                    RealeseDateDP.SelectedDate = gr.ReleaseDate;
                    BreakfastCB.IsChecked = gr.Breakfast;
                    LunchCB.IsChecked = gr.Lunch;
                    DinnerCB.IsChecked = gr.Dinner;

                }
                else
                {
                    throw new FormatException("הכנס ת.ז בעלת 9 ספרות");
                }


            }
            catch (Exception exp)
            {
             
                MessageBox.Show(exp.Message);

            }
    
           

        }

        private void Button_Click_UpdateGR(object sender, RoutedEventArgs e)
        {
            

            if (BreakfastCB.IsChecked == true)

                gr.Breakfast = true;
            else
            { gr.Breakfast = false; }

            if (LunchCB.IsChecked == true)

                gr.Lunch = true;
            else
            { gr.Lunch = false; }

            if (DinnerCB.IsChecked == true)

                gr.Dinner = true;
            else
            { gr.Dinner = false; }

            


            try
            {

             bl.UpdateGuestRequestB(gr);
             vi.Visibility = Visibility.Visible;
             addlable.Visibility = Visibility.Visible;
             System.Threading.Thread.Sleep(1500);
             UpdateGrid.Visibility = Visibility.Collapsed;
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
            if (txtNumChild == null)   {return;}

            if (!int.TryParse(txtNumChild.Text, out _numValue3))

                txtNumChild.Text = _numValue3.ToString();

        }


        private void Minus_Click3(object sender, RoutedEventArgs e)
        {
            if (NumValue3 > 0)
            { NumValue3--; }
            else NumValue3 = 0;
        }
        private void Plus_Click3(object sender, RoutedEventArgs e) { NumValue3++;}

        #endregion
        private void SubAreaCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AreaBtn.Content = SubAreaCB.SelectedItem.ToString();
            if (SubAreaCB.SelectedItem != null)
            {
              gr3.Visibility = Visibility.Collapsed;
              gr.SubArea = (All)Enum.Parse(typeof(All), SubAreaCB.SelectedItem.ToString(), true);
            }
        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {gr.Pool = (ChoosingAttraction)Enum.Parse(typeof(ChoosingAttraction), poolCB.SelectedItem.ToString(), true); }

        private void ComboBox_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
        { gr.Garden = (ChoosingAttraction)Enum.Parse(typeof(ChoosingAttraction), GardenCB.SelectedItem.ToString(), true);}

        private void ComboBox_SelectionChanged_3(object sender, SelectionChangedEventArgs e)
        { gr.Jacuzzi = (ChoosingAttraction)Enum.Parse(typeof(ChoosingAttraction), jakouziCB.SelectedItem.ToString(), true);}

        private void ComboBox_SelectionChanged_4(object sender, SelectionChangedEventArgs e)
        { gr.ChildrensAttractions = (ChoosingAttraction)Enum.Parse(typeof(ChoosingAttraction), attractionCB.SelectedItem.ToString(), true); }

        private void ComboBox_SelectionChanged_5(object sender, SelectionChangedEventArgs e)
        { gr.Type = (TypesOfVacation)Enum.Parse(typeof(TypesOfVacation), TypeHostingUnitCB.SelectedItem.ToString(), true);}

        private void PN_TextChanged(object sender, TextChangedEventArgs e)
        {gr.PrivateName = PN.Text.ToString();}

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
        private void FamilyBtn_Click(object sender, RoutedEventArgs e) { gr2.Visibility = Visibility.Visible; }

        private void Date_Click(object sender, RoutedEventArgs e){  gr5.Visibility = Visibility.Visible;}

        private void Area_Click(object sender, RoutedEventArgs e) {gr3.Visibility = Visibility.Visible; }

        private void Room_Button_Click(object sender, RoutedEventArgs e) { gr4.Visibility = Visibility.Visible;}

        private void RealeseDateChanged(object sender, RoutedEventArgs e)
          {
            
            TxtDateOut.Content = RealeseDateDP.SelectedDate.Value.Date.ToString();
            TimeSpan x = RealeseDateDP.SelectedDate.Value.Date - EntryDateDP.SelectedDate.Value.Date;
            calendarTxt.Content = x.Days.ToString();
            if (RealeseDateDP.SelectedDate != null && EntryDateDP.SelectedDate != null) {
                gr.ReleaseDate = RealeseDateDP.SelectedDate.Value.Date;
                gr5.Visibility = Visibility.Collapsed;
                
            }

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
            { gr.EntryDate = EntryDateDP.SelectedDate.Value.Date; 
                gr5.Visibility = Visibility.Collapsed;
             }
        }

        private void gr4_MouseLeave(object sender, RoutedEventArgs e) { gr4.Visibility = Visibility.Collapsed;}

        private void gr2_MouseLeave(object sender, RoutedEventArgs e) {   gr2.Visibility = Visibility.Collapsed;}


    }

}


 
