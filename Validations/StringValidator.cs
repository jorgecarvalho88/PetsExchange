using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validations
{
    public static class StringValidatior
    {
        public static string? ValidateLength(string value, string property, int length)
        {
            if (ValidateIsNullOrWhiteSpace(value, null) == null && value.Length > length)
            {
                return property + ": Max length is " + length + " chars";
            }

            return null;
        }

        public static string? ValidateIsNullOrWhiteSpace(string value, string? property)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return property + ": Required Field";
            }

            return null;
        }
    }
}
