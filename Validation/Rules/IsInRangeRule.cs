using System;
using System.Collections.Generic;
using System.Text;

namespace Validation.Rules
{
    public class IsInRangeRule<T> : IValidationRule<T>
    {
        public IsInRangeRule(double min,double max)
        {
            Min = min;
            Max = max;
        }
        public string ValidationMessage { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }

        public bool Check(T value)
        {
            double doubleValue;
            if (!Double.TryParse(value as string, out doubleValue))
                return false;
            if (doubleValue >= Min && doubleValue <= Max)
                return true;
            return false;
        }
    }
}
