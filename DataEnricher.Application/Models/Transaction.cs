using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEnricher.Application.Models
{
    public class Transaction
    {
        public string Uti { get; set; }
        public string Isin { get; set; }
        public float Notional { get; set; }
        public string NotionalCurrency { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public float Rate { get; set; }
        public string Lei { get; set; }
        public string Legalname { get; set; }
        public string LegalCountry { get; set; }
        public string Bic { get; set; }
        public string Costs { get; set; }

    }
}