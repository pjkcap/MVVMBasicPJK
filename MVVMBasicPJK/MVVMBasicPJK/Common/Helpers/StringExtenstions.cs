using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVVMBasicPJK.Common.Helpers
{
    public static class StringExtenstions
    {
        public static string OnlyDigitNumber(this string value)
        {
            return new string(value.ToCharArray().Where(char.IsDigit).ToArray());
        }
    }
}
