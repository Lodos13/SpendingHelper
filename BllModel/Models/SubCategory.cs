using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class CSubCategory
    {
        public Int32 SubCategoryID { get; set; }
        public String Title { get;  set; }
        public String Description { get; set; }
        public CCategoryType CategoryType { get; set; }

        public List<CPayment> Payments { get; set; }

        public CSubCategory()
        {
            Payments = new List<CPayment>();
        }

        public CSubCategory(int subCategoryID, string title, string description, CCategoryType categoryType) : this()
        {
            SubCategoryID = subCategoryID;
            Title = title;
            Description = description;
            CategoryType = categoryType;
        }
    }
}
