using System;
using System.Globalization;

namespace NumericalUpDownSample
{
    public class DoubleValidationRule
    {
        public static bool IsCanInputKey(string inputkey)
        {
            string InputEnableKey = "0123456789-";
            //private static void DoubleUpDown_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
            //だと、deleteキーや、BSキーが入力されても呼び出されないので不要
            //Preview Input　
            //char.IsNumber

            if (InputEnableKey.Contains(inputkey))
            {
                return true;
            }
            NumberFormatInfo nfi = CultureInfo.CurrentCulture.NumberFormat;

            if (inputkey == nfi.CurrencyDecimalSeparator)
            {
                return true;
            }
            return false;
        }

        public static bool IsCanInputString(string inputstring)
        {
            if (inputstring == "-") return true;
            NumberFormatInfo nfi = CultureInfo.CurrentCulture.NumberFormat;
            if (inputstring == nfi.CurrencyDecimalSeparator)
            {
                return true;
            }

            double todouble;
            return Double.TryParse(inputstring, out todouble);
        }
    }
}