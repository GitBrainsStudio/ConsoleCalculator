using ConsoleCalculator.ExpressionCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ConsoleCalculator.Tests
{
    public class CalculatatorTests
    {
        [Fact]
        public void ReturnOnlyFloat()
        {
            Addition additionInCalc = new Addition();

            var result = additionInCalc.Result(12321, 45454);

            Assert.IsType<float>(result);
        }
    }
}
