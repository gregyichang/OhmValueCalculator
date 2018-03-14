using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorService.Model
{
    public class ColorCode
    {
        public string color { get; set; }
        public double? significantDigits { get; set; }
        public  double? multiplier { get; set; }
        public double? tolerance { get; set; }
        public bool isSignificant
        {
            get { return significantDigits.HasValue; }
        }
        public bool isMultiplier
        {
            get { return multiplier.HasValue; }
        }
        public bool isTolerance
        {
            get { return tolerance.HasValue; }
        }
    }
}
