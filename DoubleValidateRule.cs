﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumericalUpDownSample
{
    public class DoubleValidateRule
    {
        public bool IsCanInputKey(string inputkey)
        {
            string InputEnableKey = "0123456789";
            //private static void DoubleUpDown_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
            //だと、deleteキーや、BSキーが入力されても呼び出されないので不要
            //Preview Input　
            //char.IsNumber
            if (InputEnableKey.Contains(inputkey))
            {
                return true;
            }
            if (inputkey == ".")
            {
                return true;
            }
            return false;
        }
    }
}