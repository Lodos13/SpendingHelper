using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class CCategoryType
    {
        public Int32 CategoryID { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }

        public CCategoryType()
        {}

        public CCategoryType(int id, string title, string description)
        {
            CategoryID = id;
            Title = title;
            Description = description;
        }
        public CCategoryType(CCategoryType prototype)
        {
            CategoryID = prototype.CategoryID;
            Title = prototype.Title;
            Description = prototype.Description;
        }

        
    }
}
