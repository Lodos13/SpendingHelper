using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IFamilyRepository : IRepository<CFamilyDto,Int32>
    {
        Boolean CreateFamily(Int32 personId, String familyName, Int32 budget);
        Boolean JoinFamily(Int32 personId, Int32 familyId);
        Boolean LeaveFamily(Int32 personId);

        CFamilyDto FindByPersonId(Int32 personId);
        CFamilyDto FindByPersonIdFullLoad(Int32 personId);
    }
}
