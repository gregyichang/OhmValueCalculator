using System;
using System.Linq;
using Xunit;
using CalculatorService.App;
using Moq;
using Calculator.Controllers;

namespace Test
{
    public class CalculatorControllerTest
    {
        private readonly CalculatorController _calcCtrl;
        private Mock<IOhmValueCalculator> mockCalcService = new Mock<IOhmValueCalculator>();

        public CalculatorControllerTest()
        {
            _calcCtrl = new CalculatorController(mockCalcService.Object);
        }

        [Fact]
        public void Calculate_ShouldCallService()
        {
            //prepare data
            var bandAColoe = "Yellow";
            var bandBColoe = "Violet";
            var bandCColoe = "Red";
            var bandDColoe = "Gold";

            //action
            var result = _calcCtrl.CalculateOhmValue(bandAColoe, bandBColoe, bandCColoe, bandDColoe);

            //Expection
            mockCalcService.Verify(t => t.CalculateOhmValue(bandAColoe, bandBColoe, bandCColoe, bandDColoe), Times.Once);
        }

        [Fact]
        public void GetColorCode_ShouldCallService()
        {
            //prepare data

            //action
            var result = _calcCtrl.ColorCodes();

            //Expection
            mockCalcService.Verify(t => t.GetColorCode(), Times.Once);
        }

    }
}
