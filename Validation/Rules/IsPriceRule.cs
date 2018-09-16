using System;
using System.Collections.Generic;
using System.Text;

namespace Validation.Rules
{
    public class IsPriceRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set ; }

        public bool Check(T value)
        {
            string stringValue = value as string;
            if (!IsDouble(stringValue))
                return false;
            if (Has2PlacesAfterDot(stringValue))
                return true;
            return false;
        }

        private bool IsDouble(string value)
        {
            return Double.TryParse(value, out _);
        }
        private bool Has2PlacesAfterDot(string value)
        {
            var strings = value.Split(',');
            if (strings.Length < 2)
                return true;

            else if (strings[1].Length > 2)
                return false;

            else return true;
        }
    }
}
