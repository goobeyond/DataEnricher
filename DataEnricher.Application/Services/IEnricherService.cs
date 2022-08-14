using DataEnricher.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEnricher.Application.Services
{
    public interface IEnricherService
    {
        Task<IEnumerable<Transaction>> EnrichDataAsync(IEnumerable<InputDTO> input);
        Task<Transaction> EnrichRowAsync(InputDTO transaction);
    }
}
