using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEnricher.Application.Models
{
    public class InputDTO
    {
        public string Uti { get; set; }

        public string Isin { get; set; }

        public float Notional { get; set; }

        public string NotionalCurrency { get; set; }

        public string Type { get; set; }

        public DateTime Date { get; set; }

        public float Rate { get; set; }

        public string Lei { get; set; }
    }
}
//transaction_uti,isin,notional,notional_currency,transaction_type,transaction_datetime,rate,lei
//1030291281MARKITWIRE0000000000000112874138,EZ9724VTXK48,763000.0,GBP,Sell,2020-11-25T15:06:22Z,0.0070956000,XKZZ2JZF41MRHTR1V493