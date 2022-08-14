using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEnricher.Application.Models
{
    public class InputDtoMap : ClassMap<InputDTO>
    {
        public InputDtoMap()
        {
            Map(m => m.Uti).Name("transaction_uti").Validate(args => !string.IsNullOrEmpty(args.Field));
            Map(m => m.Isin).Name("isin").Validate(args => !string.IsNullOrEmpty(args.Field));
            Map(m => m.Notional).Name("notional").Validate(args => !string.IsNullOrEmpty(args.Field));
            Map(m => m.NotionalCurrency).Name("notional_currency").Validate(args => !string.IsNullOrEmpty(args.Field));
            Map(m => m.Type).Name("transaction_type").Validate(args => !string.IsNullOrEmpty(args.Field));
            Map(m => m.Date).Name("transaction_datetime").Validate(args => !string.IsNullOrEmpty(args.Field));
            Map(m => m.Rate).Name("rate").Validate(args => !string.IsNullOrEmpty(args.Field));
            Map(m => m.Lei).Name("lei").Validate(args => !string.IsNullOrEmpty(args.Field));
        }
    }
}
