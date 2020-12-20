using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace Ultimative.Utilities
{
    public class AnimationUtil
    {
        public static DoubleAnimation CreateAnimation(double offset, double milliseconds)
        {
            return new DoubleAnimation()
            {
                By = offset,
                Duration = TimeSpan.FromMilliseconds(milliseconds)
            };
        }
    }
}
