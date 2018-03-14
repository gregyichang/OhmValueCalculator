using System;
using System.Linq;
using Xunit;
using CalculatorService.App;
using CalculatorService;

namespace Test
{
    public class CalculatorServiceTests
    {
        private readonly OhmValueCalculator _calculator;

        public CalculatorServiceTests()
        {
            _calculator = new OhmValueCalculator();
        }

        [Fact]
        public void GetColorCode()
        {
            //prepare data

            //action
            var colorCodes = _calculator.GetColorCode();

            //Expection
            Assert.True(colorCodes.Count == 12, "Should have 12 color codes");
            Assert.True(colorCodes.Where(x=>x.isSignificant).Count() == 10, "Should have 10 color codes with significant digit");
            Assert.True(colorCodes.Where(x => x.isMultiplier).Count() == 8, "Should have 8 color codes with multipler");
            Assert.True(colorCodes.Where(x => x.isTolerance).Count() == 5, "Should have 5 color codes with tolerance");
        }

        [Fact]
        public void CalculateOhmValue_InvalidBandAException()
        {
            //prepare data
            var bandAColoe = "BadColorName";
            var bandBColoe = "Red";
            var bandCColoe = "Brown";
            var bandDColoe = "Brown";

            //action

            //Expection
            Assert.Throws<InvalidParameterException>(() => _calculator.CalculateOhmValue(bandAColoe, bandBColoe, bandCColoe, bandDColoe));
        }

        [Fact]
        public void CalculateOhmValue_InvalidBandBException()
        {
            //prepare data
            var bandAColoe = "Red";
            var bandBColoe = "BadColorName";
            var bandCColoe = "Brown";
            var bandDColoe = "Brown";

            //action

            //Expection
            Assert.Throws<InvalidParameterException>(() => _calculator.CalculateOhmValue(bandAColoe, bandBColoe, bandCColoe, bandDColoe));
        }

        [Fact]
        public void CalculateOhmValue_InvalidBandCException()
        {
            //prepare data
            var bandAColoe = "Red";
            var bandBColoe = "Brown";
            var bandCColoe = "BadColorName";
            var bandDColoe = "Brown";

            //action

            //Expection
            Assert.Throws<InvalidParameterException>(() => _calculator.CalculateOhmValue(bandAColoe, bandBColoe, bandCColoe, bandDColoe));
        }

        [Fact]
        public void CalculateOhmValue_InvalidBandDException()
        {
            //prepare data
            var bandAColoe = "Red";
            var bandBColoe = "Brown";
            var bandCColoe = "Yellow";
            var bandDColoe = "BadColorName";

            //action

            //Expection
            Assert.Throws<InvalidParameterException>(() => _calculator.CalculateOhmValue(bandAColoe, bandBColoe, bandCColoe, bandDColoe));
        }

        [Fact]
        public void CalculateOhmValue_YellowVioletRedGold()
        {
            //prepare data
            var bandAColoe = "Yellow";
            var bandBColoe = "Violet";
            var bandCColoe = "Red";
            var bandDColoe = "Gold";

            //action
            var result = _calculator.CalculateOhmValue(bandAColoe, bandBColoe, bandCColoe, bandDColoe);
            
            //Expection
            Assert.True(result.minValue == 4465, "Min value is 4465 ohm");
            Assert.True(result.maxValue == 4935, "Max value is 4935 ohm");
        }
    }
}
