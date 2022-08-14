using DataEnricher.Application.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataEnricher.Application.Services
{
    public class EnricherService : IEnricherService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ConcurrentDictionary<string, GleifResponse?> _cache;
        
        public EnricherService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _cache = new ConcurrentDictionary<string, GleifResponse?>();
        }

        public async Task<IEnumerable<Transaction>> EnrichDataAsync(IEnumerable<InputDTO> input)
        {
            var httpClient = _httpClientFactory.CreateClient("GleifClient");
            var result = new List<Transaction>();
            GleifResponse gleifInfo;
            foreach (var transaction in input)
            {
                if (!_cache.TryGetValue(transaction.Lei, out gleifInfo))
                {
                    gleifInfo = _cache.GetOrAdd(transaction.Lei, await GetGleifInfo(transaction.Lei, httpClient));
                }

                result.Add(EnrichTransaction(transaction, gleifInfo));
            }

            return result;
        }

        public async Task<Transaction> EnrichRowAsync(InputDTO transaction)
        {

            var httpClient = _httpClientFactory.CreateClient("GleifClient");
            GleifResponse gleifInfo;

            if (!_cache.TryGetValue(transaction.Lei, out gleifInfo))
            {
                gleifInfo = _cache.GetOrAdd(transaction.Lei, await GetGleifInfo(transaction.Lei, httpClient));
            }

            return EnrichTransaction(transaction, gleifInfo);
        }

        private async Task<GleifResponse?> GetGleifInfo(string lei, HttpClient httpClient)
        {
            using (var response = await httpClient.GetAsync($"v1/lei-records?filter[lei]={lei}"))
            {
                try
                {
                    response.EnsureSuccessStatusCode();
                    var stream = await response.Content.ReadAsStreamAsync();
                    var gleifinfo = await JsonSerializer.DeserializeAsync<GleifResponse>(stream);
                    return gleifinfo;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Potentially invalid LEI code {lei}");
                    return null;
                }
            }
        }

        private Transaction EnrichTransaction(InputDTO transaction, GleifResponse? gleifInfo)
        {
            var result = new Transaction()
            {
                Lei = transaction.Lei,
                Date = transaction.Date,
                Isin = transaction.Isin,
                Notional = transaction.Notional,
                NotionalCurrency = transaction.NotionalCurrency,
                Rate = transaction.Rate,
                Type = transaction.Type,
                Uti = transaction.Uti,
            };

            if (gleifInfo != null)
            {
                var bic = gleifInfo.data.FirstOrDefault()?.attributes.bic.FirstOrDefault();
                result.Bic = bic ?? "missing in response";

                var data = gleifInfo.data.FirstOrDefault();
                result.Legalname = data == null ? "missing in response" : data.attributes.entity.legalName.name;
                result.LegalCountry = data == null ? "missing in response" : data.attributes.entity.legalAddress.country;

                result.Costs = result.LegalCountry switch
                {
                    "NL" => CostCalculator.NL(transaction).ToString(),
                    "GB" => CostCalculator.GB(transaction).ToString(),
                    _ => "not supported"
                };
            }

            return result;
        }
    }
}