using CsvHelper;
using CsvHelper.Configuration;
using DataEnricher.Application.Models;
using DataEnricher.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace data_enricher.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {

        private readonly ILogger<DataController> _logger;
        private readonly IEnricherService _enricher;

        public DataController(ILogger<DataController> logger, IEnricherService enricherService)
        {
            _logger = logger;
            _enricher = enricherService;
        }

        /// <summary>
        /// Enrich the CSV.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Transaction>>> ProcessCSV(IFormFile file)
        {
            if (file == null)
            {
                return BadRequest("File must be provided.");
            } 

            if (file.ContentType != "text/csv")
            {
                return BadRequest("Invalid file format.");
            }

            IEnumerable<InputDTO> records;

            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                try
                {
                    csv.Context.RegisterClassMap<InputDtoMap>();
                    records = csv.GetRecords<InputDTO>().ToList();
                }
                catch (Exception e)
                {
                    return UnprocessableEntity($"File could not be processed. Error: {e.Message}");
                }
            }
            var result = await _enricher.EnrichDataAsync(records);
            return Ok(result);
        }
    }
}
