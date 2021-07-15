using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace DragAndDropSampleManaged.Converters
{
    class TermAndConditionNodeTyeConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string EnumString;
            try
            {
                EnumString = Enum.GetName((value.GetType()), value);
                switch (EnumString)
                {
                    case "Project":
                        return "H";
                        
                    case "Paragraph":
                        return "P";
                        
                    case "Subparagraph":
                        return "S";

                    default:
                        return "S";
                        
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
