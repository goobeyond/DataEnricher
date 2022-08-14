using DataEnricher.Application.Models;
using DataEnricher.Application.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEnricher.Application.UnitTests.Services
{
    [TestClass]
    public class CostCalculatorTests
    {

        [TestMethod]
        [DataRow(100, 0.5f, 100)]
        [DataRow(200, 0.5f, 200)]
        
        public void NL(float notional, float rate, float expected)
        {
            var input = new InputDTO()
            {
                Notional = notional,
                Rate = rate,
            };

            var result = CostCalculator.NL(input);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow(10, 20, 100)]
        [DataRow(10, 10, 0)]
        [DataRow(20, 10, -200)]
        public void GB(float notional, float rate, float expected)
        {
            var input = new InputDTO()
            {
                Notional = notional,
                Rate = rate,
            };

            var result = CostCalculator.GB(input);
            Assert.AreEqual(expected, result);
        }
    }
}
