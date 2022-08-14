using DataEnricher.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEnricher.Application.Services
{
    public static class CostCalculator
    {
        public static float NL(InputDTO transaction)
        {
            return Math.Abs(transaction.Notional * (1 / transaction.Rate) - transaction.Notional);
        }

        public static float GB(InputDTO transaction)
        {
            return transaction.Notional * (transaction.Rate - transaction.Notional);
        }
    }
}
