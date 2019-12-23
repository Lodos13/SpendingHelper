using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class CPerson
    {
        public Int32 PersonID { get; set; }
        public String Name { get; set; }
        public Int32 Budget { get; set; }
        public CFamily Family { get; set; }
        
        public List<CSubCategory> SubCategories { get; set; }
        public List<CSpendingHistory> Histories { get; set; }
        public List<CTarget> PersonalTargets { get; set; }
        public List<CReminder> Reminders { get; set; }

        public CPerson()
        {
            SubCategories = new List<CSubCategory>();
            Histories = new List<CSpendingHistory>();
            PersonalTargets = new List<CTarget>();
            Reminders = new List<CReminder>();
        }

        public CPerson(Int32 id, String name, Int32 budget) : this()
        {
            PersonID = id;
            Name = name;
            Budget = budget;
        }

    }
}
