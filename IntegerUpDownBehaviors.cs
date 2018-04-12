using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Xceed.Wpf.Toolkit;

namespace NumericalUpDownSample
{
    public class IntegerUpDownBehaviors
    {
        /// <summary>
        /// True なら入力を数字のみに制限します。
        /// </summary>
        public static readonly DependencyProperty IsNumericProperty =
                    DependencyProperty.RegisterAttached(
                        "IsNumeric", typeof(bool),
                        typeof(IntegerUpDownBehaviors),
                        new UIPropertyMetadata(false, IsNumericChanged)
                    );

        [AttachedPropertyBrowsableForType(typeof(IntegerUpDown))]
        public static bool GetIsNumeric(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsNumericProperty);
        }

        [AttachedPropertyBrowsableForType(typeof(IntegerUpDownBehaviors))]
        public static void SetIsNumeric(DependencyObject obj, bool value)
        {
            obj.SetValue(IsNumericProperty, value);
        }

        private static void IsNumericChanged
            (DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var integerUpDown = sender as IntegerUpDown;
            if (integerUpDown == null) return;

            // イベントを登録・削除 
            integerUpDown.PreviewTextInput -= OnPreviewTextInput;
            DataObject.RemovePastingHandler(integerUpDown, textbox_PastingHandler);
            integerUpDown.Loaded -= IntegerUpDown_Loaded;
            
            var isNumeric = (bool)e.NewValue;
            if (isNumeric)
            {
                integerUpDown.Loaded += IntegerUpDown_Loaded;
                integerUpDown.PreviewTextInput += OnPreviewTextInput;
                DataObject.AddPastingHandler(integerUpDown, textbox_PastingHandler);
            }
        }

        private static void IntegerUpDown_Loaded(object sender, RoutedEventArgs e)
        {
            var integerUpDown = sender as IntegerUpDown;
            var textBox = integerUpDown.Template.FindName("PART_TextBox", integerUpDown) as WatermarkTextBox;
            if (null != textBox)
            {
                InputMethod.SetIsInputMethodEnabled(textBox, false);
            }
        }

        private static bool IsAllNumber(string text)
        {
            return !text.Any(c => !char.IsNumber(c));
        }
        private static void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsAllNumber(e.Text))
            {
                e.Handled = true;
            }
        }
        //ペーストに対応するために必要
        private static void textbox_PastingHandler(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(DataFormats.Text))
            {
                string text = Convert.ToString(e.DataObject.GetData(DataFormats.Text));
                if (!IsAllNumber(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }
    }
}