using CsvHelper;
using DataEnricher.Application.Models;
using DataEnricher.Application.Services;
using System.Globalization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CsvHelper.Configuration;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
        services
        .AddScoped<IEnricherService, EnricherService>()
        .AddHttpClient("GleifClient", config =>
            {
                config.BaseAddress = new Uri("https://api.gleif.org/api/");
                config.Timeout = new TimeSpan(0, 0, 10);
            })
        )
    .Build();

using IServiceScope serviceScope = host.Services.CreateScope();
IServiceProvider provider = serviceScope.ServiceProvider;
var _enricher = provider.GetRequiredService<IEnricherService>();

int iteration = 0;
int batchSize = 60;
var finalresult = new List<Transaction>();
var formattedDate = string.Format("output-{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now);

using (var reader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\..\\data.csv")))
{
    var configuration = new CsvConfiguration(CultureInfo.InvariantCulture);

    using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
    {
        ReadingExceptionOccurred = (args) =>
        {
            var ex = (CsvHelperException)args.Exception.GetBaseException();
            Console.WriteLine($"Error at row {ex.Context.Parser.RawRow}. Please fix row {ex.Context.Parser.RawRecord}.");
            return false;
        }
    }))
    {
        csv.Context.RegisterClassMap<InputDtoMap>();

        var records = csv.GetRecords<InputDTO>();
        foreach (var record in records)
        {
            if (record != null)
            {
                var result = await _enricher.EnrichRowAsync(record);
                finalresult.Add(result);
            }

            iteration++;

            using (var writer = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), $"..\\..\\..\\..\\enriched-data-{formattedDate}.csv")))
            using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
                csvWriter.WriteRecords(finalresult);

            if (iteration % batchSize == 0)
            {
                Thread.Sleep(5000);
            }

        }

    }
}

