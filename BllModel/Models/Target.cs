using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class CTarget 
    {
        public DateTime Month { get; set; }
        public String Type { get; set; }
        public Decimal Amount { get; set; }
        public CCategoryType CategoryType { get; set; }

        public CTarget() { }

        public CTarget(DateTime month, string type, decimal amount, CCategoryType categoryType)
        {
            Month = month;
            Type = type;
            Amount = amount;
            CategoryType = categoryType;
        }
    }
}
