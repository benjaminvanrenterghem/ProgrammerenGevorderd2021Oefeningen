using System;
using System.Windows.Data;

namespace KlantBeheer.WPF.ValueConverters
{
    public class YesNoToBooleanConverter : IValueConverter
    {
		public object Convert(object value, Type targetType, object parameter,
				System.Globalization.CultureInfo culture) // culture: bepaalt de taal
		{
			switch (value.ToString().ToLower())
			{
				case "yes":
				case "oui":
				case "ja":
					return true;
				case "no":
				case "non":
				case "nee":
				case "neen":
					return false;
			}
			return false;
		}

		public object ConvertBack(object value, Type targetType, object parameter,
		System.Globalization.CultureInfo culture)
		{
			if (value is bool) // ik maak zeker dat we een bool toekrijgen via is
			{
				if ((bool)value == true)
					return "yes";
				else
					return "no";
			}
			return "no";
		}
	}
}
