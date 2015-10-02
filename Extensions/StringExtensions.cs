using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainBit.Utility.Extensions
{
    public static class StringExtensions
    {
        public static string TrimSafe(this string value)
        {
            return value != null ? value.Trim() : null;
        }
    }
}