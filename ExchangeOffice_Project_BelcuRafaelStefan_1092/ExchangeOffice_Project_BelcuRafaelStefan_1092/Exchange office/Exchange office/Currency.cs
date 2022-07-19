using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange_office
{
    [Serializable]
    class Currency
    {
        public int Euro { get; set; }
        public int Leu { get; set; }
        public int Dollar { get; set; }

        public Currency(int euro, int leu, int dollar)
        {
            Euro = euro;
            Leu = leu;
            Dollar = dollar;
        }
    }
}
