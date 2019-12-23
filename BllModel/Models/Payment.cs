using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class CPayment
    {
        public Int32 PaymentID { get; set; }
        public DateTime Date { get; set; }
        public Decimal Spended { get; set; }

        public CPayment() { }

        public CPayment(Int32 id, DateTime date, Decimal spended)
        {
            PaymentID = id;
            Date = date;
            Spended = spended;
        }
    }
}
