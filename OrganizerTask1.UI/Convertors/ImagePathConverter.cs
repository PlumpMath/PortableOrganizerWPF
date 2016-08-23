using OrganizerTasks1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace OrganizerTask1.UI.Convertors
{
    public class ImagePathConverter : IValueConverter
    {
        Dictionary<TaskStatus, BitmapImage> imageCashe = new Dictionary<TaskStatus, BitmapImage>();

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var status = (TaskStatus)value;
            if (!imageCashe.ContainsKey(status))
            {
                var uri = new Uri(string.Format(@"../Images/{0}/{1}.png", parameter, status), UriKind.Relative);
                imageCashe.Add(status, new BitmapImage(uri));
            }
            return imageCashe[status];
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
