using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using CalculatorService.App;
using CalculatorService.Model;

namespace Calculator.Controllers
{
    [Route("api/[controller]")]
    public class CalculatorController : Controller
    {
        private readonly IOhmValueCalculator _calculator = null;

        public CalculatorController(IOhmValueCalculator caculator)
        {
            _calculator = caculator;
        }

        [HttpGet("[action]")]
        [ProducesResponseType(typeof(IEnumerable<ColorCode>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult ColorCodes()
        {
            return Ok(_calculator.GetColorCode());
        }

        /// <summary>
        /// Calculates the Ohm value of a resistor based on the band colors.
        /// </summary>
        /// <param name="bandAColor">The color of the first figure of component value band.</param>
        /// <param name="bandBColor">The color of the second significant figure band.</param>
        /// <param name="bandCColor">The color of the decimal multiplier band.</param>
        /// <param name="bandDColor">The color of the tolerance value band.</param>
        [HttpPost("CalculateOhmValue")]
        [ProducesResponseType(typeof(OhmValue), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult CalculateOhmValue(string bandAColor, string bandBColor, string bandCColor, string bandDColor)
        {
            return Ok(_calculator.CalculateOhmValue(bandAColor, bandBColor, bandCColor, bandDColor));
        }
    }
}
