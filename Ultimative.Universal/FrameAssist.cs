using System.Windows;

namespace Ultimative.Universal
{
    public class FrameAssist
    {
        private static readonly object DefaultFrameContent = null;

        #region AttachedProperty : CornerRadiusProperty
        /// <summary>
        /// Controls the corner radius of the surrounding box.
        /// </summary>
        public static readonly DependencyProperty FrameContentProperty
            = DependencyProperty.RegisterAttached("FrameContent", typeof(object), typeof(FrameAssist), new PropertyMetadata(DefaultFrameContent));

        public static object GetFrameContent(DependencyObject element) => element.GetValue(FrameContentProperty);
        public static void SetFrameContent(DependencyObject element, object value) => element.SetValue(FrameContentProperty, value);
        #endregion
    }
}
