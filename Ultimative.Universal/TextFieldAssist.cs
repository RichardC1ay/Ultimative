using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ultimative.Universal
{
    public class TextFieldAssist
    {
        private static readonly string DefaultPromptText = "";

        #region AttachedProperty : PromptTextProperty
        /// <summary>
        /// Controls the corner radius of the surrounding box.
        /// </summary>
        public static readonly DependencyProperty PromptTextProperty
            = DependencyProperty.RegisterAttached("PromptText", typeof(string), typeof(TextFieldAssist), new PropertyMetadata(DefaultPromptText));

        public static string GetPromptText(DependencyObject element) => (string)element.GetValue(PromptTextProperty);
        public static void SetPromptText(DependencyObject element, string value) => element.SetValue(PromptTextProperty, value);
        #endregion
    }
}
