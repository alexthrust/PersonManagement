﻿using System;

namespace PersonManagement.Extensions
{
    public static class StringExtensions
    {
        public static string ToUpperCaseFirstLetter(this string str)
        {
            if (!string.IsNullOrEmpty(str) && str.Length > 1)
            {
                return char.ToUpperInvariant(str[0]) + str.Substring(1);
            }
            return str;
        }
    }
}