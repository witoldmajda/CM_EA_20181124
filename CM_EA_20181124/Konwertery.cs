using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace CM_EA_20181124
{
    public class BoolToBrushConverter : IValueConverter
    {
        public Brush KolorDlaFalszu { get; set; } = Brushes.Black;
        public Brush KolorDlaPrawdy { get; set; } = Brushes.Gray;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool bvalue = (bool)value;
            return !bvalue ? KolorDlaFalszu : KolorDlaPrawdy;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class PriorytetZadaniaToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Model.PriorytetZadania priorytetZadania = (Model.PriorytetZadania)value;
            return Model.Zadanie.OpisPriorytetu(priorytetZadania);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string opisPriorytetu = (value as string).ToLower();
            return Model.Zadanie.ParsujOpisPriorytetu(opisPriorytetu);
        }
    }

    public class PriorytetZadaniaToBrush : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Model.PriorytetZadania priorytetZadania = (Model.PriorytetZadania)value;
            switch (priorytetZadania)
            {
                case Model.PriorytetZadania.MniejWazne:
                    return Brushes.Olive;
                case Model.PriorytetZadania.Wazne:
                    return Brushes.Orange;
                case Model.PriorytetZadania.Krytyczne:
                    return Brushes.OrangeRed;
                default:
                    throw new Exception("Nierozpoznany priorytet zadania");
            }
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class BoolToTextDecorationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool bvalue = (bool)value;
            return bvalue ? TextDecorations.Strikethrough : null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
