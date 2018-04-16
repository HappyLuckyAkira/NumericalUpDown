using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Xceed.Wpf.Toolkit;

namespace NumericalUpDownSample
{
    class DoubleUpDownBehaviors
    {
        
        /// <summary>
        /// True なら入力を数字のみに制限します。
        /// </summary>
        public static readonly DependencyProperty IsNumericProperty =
                    DependencyProperty.RegisterAttached(
                        "IsNumeric", typeof(bool),
                        typeof(DoubleUpDownBehaviors),
                        new UIPropertyMetadata(false, IsNumericChanged)
                    );

        [AttachedPropertyBrowsableForType(typeof(DoubleUpDown))]
        public static bool GetIsNumeric(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsNumericProperty);
        }

        [AttachedPropertyBrowsableForType(typeof(DoubleUpDownBehaviors))]
        public static void SetIsNumeric(DependencyObject obj, bool value)
        {
            obj.SetValue(IsNumericProperty, value);
        }

        private static void IsNumericChanged
            (DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var doubleUpDown = sender as DoubleUpDown;
            if (doubleUpDown == null) return;

            // イベントを登録・削除 
            doubleUpDown.PreviewTextInput -= DoubleUpDown_PreviewTextInput;
            DataObject.RemovePastingHandler(doubleUpDown, textbox_PastingHandler);
            doubleUpDown.Loaded -= DoubleUpDown_Loaded;
            
            var isNumeric = (bool)e.NewValue;
            if (isNumeric)
            {
                doubleUpDown.Loaded += DoubleUpDown_Loaded;
                doubleUpDown.PreviewTextInput += DoubleUpDown_PreviewTextInput;
                DataObject.AddPastingHandler(doubleUpDown, textbox_PastingHandler);
            }
        }

        private static void DoubleUpDown_Loaded(object sender, RoutedEventArgs e)
        {
            var doubleUpDown = sender as DoubleUpDown;
            var textBox = doubleUpDown.Template.FindName("PART_TextBox",doubleUpDown) as WatermarkTextBox;
            if (null != textBox)
            {
                InputMethod.SetIsInputMethodEnabled(textBox, false);
            }
        }

        //途中段階で、どこまで許可するか
        //入力中は緩めにしたいが、小数点とかはひとつにしたい。
        //ただ、ロケールにより、,と.両方を許可する必要あり
        //TryParseだと途中経過が、.だけに何故かなるので難しい
        
        private static void DoubleUpDown_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!DoubleValidationRule.IsCanInputKey(e.Text))
            {
                e.Handled = true;
            }
        }

        private static bool IsAllNumber(string text)
        {
            //return !text.Any(c => !char.IsNumber(c));
            double todouble;
            return Double.TryParse(text, out todouble);
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
