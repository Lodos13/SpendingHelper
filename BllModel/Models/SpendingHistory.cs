using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class CSpendingHistory
    {
        public DateTime Month { get; set; }
        public Decimal Spended { get; set; }

        public CSpendingHistory()
        { }

        public CSpendingHistory(DateTime month, decimal spended)
        {
            Month = month;
            Spended = spended;
        }
    }
}
