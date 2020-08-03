using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using BE;
using BL;

namespace PLWPF
{
    #region GuestRequest
    public class ConvertersFullName : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            GuestRequest guestRequest = (GuestRequest)value;
            return guestRequest.PrivateName + " " + guestRequest.FamilyName;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ConvertersMeal : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            GuestRequest guestRequest = (GuestRequest)value;
            string meal = "";
            if (guestRequest.Breakfast == true)
                meal += "B ";
            else
                meal += " ";
            if (guestRequest.Lunch == true)
                meal += "L ";
            else
                meal += " ";
            if (guestRequest.Dinner == true)
                meal += "D";

            return meal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ConvertersPool : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            GuestRequest guestRequest = (GuestRequest)value;
            return guestRequest.Pool == ChoosingAttraction.אפשרי || guestRequest.Pool == ChoosingAttraction.הכרחי ? "V" : "X";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ConvertersJacuzzi : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            GuestRequest guestRequest = (GuestRequest)value;
            return guestRequest.Jacuzzi == ChoosingAttraction.אפשרי || guestRequest.Jacuzzi == ChoosingAttraction.הכרחי ? "V" : "X";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ConvertersPChildrensAttractions : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            GuestRequest guestRequest = (GuestRequest)value;
            return guestRequest.ChildrensAttractions == ChoosingAttraction.אפשרי || guestRequest.ChildrensAttractions == ChoosingAttraction.הכרחי ? "V" : "X";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ConvertersGarden : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            GuestRequest guestRequest = (GuestRequest)value;
            return guestRequest.Garden == ChoosingAttraction.אפשרי || guestRequest.Garden == ChoosingAttraction.הכרחי ? "V" : "X";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    #endregion
 
    #region HostingUnit
    public class ConverterHostName : IValueConverter
    {
        BL.IBL bl;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bl = BL.FactoryBL.GetBL();
            HostingUnit hostingUnit = (HostingUnit)value;
            return hostingUnit.Owner.PrivateName + " " + hostingUnit.Owner.FamilyName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class ConvertersPoolHU : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            HostingUnit hostingUnit = (HostingUnit)value;
            return hostingUnit.Pool ? "V" : "X";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ConvertersGardenHU : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            HostingUnit hostingUnit = (HostingUnit)value;
            return hostingUnit.Garden ? "V" : "X";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ConvertersjakuziHU : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            HostingUnit hostingUnit = (HostingUnit)value;
            return hostingUnit.Jacuzzi ? "V" : "X";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ConvertersAtractionHU : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            HostingUnit hostingUnit = (HostingUnit)value;
            return hostingUnit.ChildrensAttractions ? "V" : "X";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ConvertersMealHU : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            HostingUnit hostingUnit = (HostingUnit)value;
            string meal = "";
            if (hostingUnit.Breakfast == true)
                meal += "B ";
            else
                meal += " ";
            if (hostingUnit.Lunch == true)
                meal += "L ";
            else
                meal += " ";
            if (hostingUnit.Dinner == true)
                meal += "D";
            return meal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    #endregion

    #region order
    public class ConvertersFullNameorder : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Order order = (Order)value;
            return order.PrivateName + " " + order.FamilyName;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    #endregion
}
