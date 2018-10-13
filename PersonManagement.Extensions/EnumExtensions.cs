using System;

namespace PersonManagement.Extensions
{
    public static class EnumExtensions
    {
        public static Int32 ToInt32(this Enum input)
        {
            return Convert.ToInt32(input);
        }
    }
}