using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange_office
{
    [Serializable]
   public class Transaction
    {
        public String TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public String FromCurrency { get; set; }
        public String ToCurrency { get; set; }
        public int Amount { get; set; }
        public int Euro { get; set; }
        public int Leu { get; set; }
        public int Dollar { get; set; }

        public Transaction(String transactionType, DateTime transactionDate, int euro, int leu, int dollar, string fromCurrency, string toCurrency, int amount)
        {
            TransactionType = transactionType;
            TransactionDate = transactionDate;
            Euro = euro;
            Leu = leu;
            Dollar = dollar;
            FromCurrency = fromCurrency;
            ToCurrency = toCurrency;
            Amount = amount;
        }
    }
}
