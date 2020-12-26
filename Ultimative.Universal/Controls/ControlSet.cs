using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Ultimative.Universal.Controls
{
    public class ControlSet
    {
        public static Rectangle GetSeparator(Orientation orientation)
        {
            var rec = new Rectangle()
            {
                Fill = new SolidColorBrush(Color.FromArgb(60, 0, 0, 0)),
            };
            if (orientation == Orientation.Horizontal)
            {
                rec.Height = 1;
                rec.VerticalAlignment = VerticalAlignment.Stretch;
            }
            else
            {
                rec.Width = 1;
                rec.HorizontalAlignment = HorizontalAlignment.Stretch;
            }
            return rec;
        }
    }
}
