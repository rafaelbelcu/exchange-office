using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange_office
{
    [Serializable]
    class Exchange_rate
    {
        public String FromCurrency { get; set; }
        public String ToCurrency { get; set; }
        public int Amount { get; set; }

        public Exchange_rate(string fromCurrency, string toCurrency, int amount)
        {
            FromCurrency = fromCurrency;
            ToCurrency = toCurrency;
            Amount = amount;
        }
    }
}
