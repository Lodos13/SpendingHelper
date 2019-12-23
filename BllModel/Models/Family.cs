using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class CFamily
    {
        public Int32 FamilyID { get; set; }
        public String FamilyName { get; set; }
        public Int32 Budget { get; set; }
        public Int32 MembersCounter { get; set; }

        public List<CTarget> FamilyTargets { get; set; }
        public List<CPerson> FamilyMembers { get; set; }

        public CFamily()
        {
            FamilyTargets = new List<CTarget>();
            FamilyMembers = new List<CPerson>();
        }
        public CFamily(Int32 id, String familyName, Int32 budget, Int32 membersCount) : this()
        {
            FamilyID = id;
            FamilyName = familyName;
            Budget = budget;
            MembersCounter = membersCount;
        }
    }
}
