﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Resume.Utils.ExtensionMethods
{
    public static class StringExtensionMethods
    {
        public static string ToSnakeCase(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            var startUnderscores = Regex.Match(input, @"^_+");
            CultureInfo cultureInfo = new CultureInfo("en-US");
            return startUnderscores + Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower(cultureInfo);
        }
    }
}
