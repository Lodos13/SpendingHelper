using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class CReminder
    {
        public String Title { get; set; }
        public String Description { get; set; }
        public Int32 DayOfMonth { get; set; }

        public CReminder() { }

        public CReminder(string title, string description, int dayOfMonth)
        {
            Title = title;
            Description = description;
            DayOfMonth = dayOfMonth;
        }
    }
}
