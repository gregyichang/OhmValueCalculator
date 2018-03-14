using System;
using System.Collections.Generic;
using CalculatorService.Model;

namespace CalculatorService.App
{
    public class OhmValueCalculator : IOhmValueCalculator
    {
        const double default_tolerance = 0.2;
        private readonly List<ColorCode> _colorCodes;
        private readonly Dictionary<string, ColorCode> _colorCodeDict;

        public OhmValueCalculator()
        {
            _colorCodes = new List<ColorCode>();
            _colorCodeDict = new Dictionary<string, ColorCode>();

            var blackColor = new ColorCode { color = "Black", significantDigits = 0, multiplier = 1 };
            _colorCodeDict.Add(blackColor.color, blackColor);
            _colorCodes.Add(blackColor);
            var redColor = new ColorCode { color = "Red", significantDigits = 2, multiplier = 100, tolerance = 0.02 };
            _colorCodeDict.Add(redColor.color, redColor);
            _colorCodes.Add(redColor);
            var orangeColor= new ColorCode { color = "Orange", significantDigits = 3, multiplier = 1000 };
            _colorCodeDict.Add(orangeColor.color, orangeColor);
            _colorCodes.Add(orangeColor);
            var yelllowColor = new ColorCode { color = "Yellow", significantDigits = 4, multiplier = 10000 };
            _colorCodeDict.Add(yelllowColor.color, yelllowColor);
            _colorCodes.Add(yelllowColor);
            var greenColor = new ColorCode { color = "Green", significantDigits = 5, multiplier = 100000, tolerance = 0.005 };
            _colorCodeDict.Add(greenColor.color, greenColor);
            _colorCodes.Add(greenColor);
            var blueColor = new ColorCode { color = "Blue", significantDigits = 6, multiplier = 1000000 };
            _colorCodeDict.Add(blueColor.color, blueColor);
            _colorCodes.Add(blueColor);
            var violetColor = new ColorCode { color = "Violet", significantDigits = 7, multiplier = 10000000 };
            _colorCodeDict.Add(violetColor.color, violetColor);
            _colorCodes.Add(violetColor);
            var greyColor = new ColorCode { color = "Grey", significantDigits = 8 };
            _colorCodeDict.Add(greyColor.color, greyColor);
            _colorCodes.Add(greyColor);
            var whiteColor = new ColorCode { color = "White", significantDigits = 9 };
            _colorCodeDict.Add(whiteColor.color, whiteColor);
            _colorCodes.Add(whiteColor);
            var goldColor = new ColorCode { color = "Gold", tolerance = 0.05 };
            _colorCodeDict.Add(goldColor.color, goldColor);
            _colorCodes.Add(goldColor);
            var silverColor = new ColorCode { color = "Silver", tolerance = 0.10 };
            _colorCodeDict.Add(silverColor.color, silverColor);
            _colorCodes.Add(silverColor);
        }

        /// <summary>
        /// Calculates the Ohm value of a resistor based on the band colors.
        /// </summary>
        /// <param name="bandAColor">The color of the first figure of component value band.</param>
        /// <param name="bandBColor">The color of the second significant figure band.</param>
        /// <param name="bandCColor">The color of the decimal multiplier band.</param>
        /// <param name="bandDColor">The color of the tolerance value band.</param>
        public OhmValue CalculateOhmValue(string bandAColor, string bandBColor, string bandCColor, string bandDColor)
        {
            if (!_colorCodeDict.ContainsKey(bandAColor)) throw new InvalidOperationException("invalid parameters");
            var colorACode = _colorCodeDict[bandAColor];
            if (!_colorCodeDict.ContainsKey(bandBColor)) throw new InvalidOperationException("invalid parameters");
            var colorBCode = _colorCodeDict[bandBColor];
            if (!_colorCodeDict.ContainsKey(bandCColor)) throw new InvalidOperationException("invalid parameters");
            var colorCCode = _colorCodeDict[bandCColor];
            if (!string.IsNullOrEmpty(bandDColor) && !_colorCodeDict.ContainsKey(bandDColor)) throw new InvalidOperationException("invalid parameters");

            if (!colorACode.isSignificant || !colorBCode.isSignificant || !colorCCode.isMultiplier) throw new InvalidOperationException("invalid parameters");

            double abValue = colorACode.significantDigits.Value * 10.0 + colorBCode.significantDigits.Value;
            double abcValue = abValue * colorCCode.multiplier.Value;
            var ohmValue = new OhmValue();
            var tolerate = GetTolerate(bandDColor);

            ohmValue.minValue = abcValue * (1 - tolerate);
            ohmValue.maxValue = abcValue * (1 + tolerate);

            return ohmValue;
        }

        /// <summary>
        /// return color codes for resistor based on the band colors.
        /// </summary>
        public IEnumerable<ColorCode> GetColorCode()
        {
            return _colorCodes;
        }

        private double GetTolerate(string bandDColor)
        {
            if (!string.IsNullOrEmpty(bandDColor))
            {
                return default_tolerance;
            }
            else if (_colorCodeDict[bandDColor].isTolerance)
            {
                return _colorCodeDict[bandDColor].tolerance.Value;
            }else
            {
                throw new InvalidOperationException("invalid parameters");
            }
        }
    }
}
