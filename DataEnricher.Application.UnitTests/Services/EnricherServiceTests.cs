using DataEnricher.Application.Models;
using DataEnricher.Application.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DataEnricher.Application.UnitTests
{
    [TestClass]
    public class EnricherServiceTests
    {
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
        private readonly EnricherService _target;

        public EnricherServiceTests()
        {
            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
            
            _target = new EnricherService(_httpClientFactoryMock.Object);
        }

        [TestMethod]
        public async Task EnrichRow_ValidData_ReturnsEnrichedData()
        {
            var Legalname = "CITIGROUP GLOBAL MARKETS LIMITED";
            var LegalCountry = "GB";
            var Bic = "SBILGB2LXXX";

            _httpClientFactoryMock
                .Setup(x => x.CreateClient(It.IsAny<string>()))
                .Returns(new HttpClient(new HttpMessageHandlerStub(Bic, Legalname, LegalCountry)) { BaseAddress = new System.Uri("https://www.abc.com")});

            var input = new InputDTO()
            {
                Uti = "uti",
                Lei = "lei",
                Date = System.DateTime.Now,
                Isin = "isin",
                Notional = 1000,
                NotionalCurrency = "test",
                Rate = 1,
                Type = "test"
            };

            var result = await _target.EnrichRowAsync(input);

            Assert.IsNotNull(result);
            Assert.AreEqual(Legalname, result.Legalname);
            Assert.AreEqual(LegalCountry, result.LegalCountry);
            Assert.AreEqual(Bic, result.Bic);
            Assert.IsNotNull(result.Costs);
        }

        [TestMethod]
        public async Task EnrichRow_UnsuportedLegalCountry_CostsIsUnsupported()
        {
            var Legalname = "CITIGROUP GLOBAL MARKETS LIMITED";
            var LegalCountry = "FR";
            var Bic = "SBILGB2LXXX";

            _httpClientFactoryMock
                .Setup(x => x.CreateClient(It.IsAny<string>()))
                .Returns(new HttpClient(new HttpMessageHandlerStub(Bic, Legalname, LegalCountry)) { BaseAddress = new System.Uri("https://www.abc.com") });

            var input = new InputDTO()
            {
                Uti = "uti",
                Lei = "lei",
                Date = System.DateTime.Now,
                Isin = "isin",
                Notional = 1000,
                NotionalCurrency = "test",
                Rate = 1,
                Type = "test"
            };

            var result = await _target.EnrichRowAsync(input);

            Assert.IsNotNull(result);
            Assert.AreEqual(Legalname, result.Legalname);
            Assert.AreEqual(LegalCountry, result.LegalCountry);
            Assert.AreEqual(Bic, result.Bic);
            Assert.AreEqual("not supported", result.Costs);
        }
    }
}