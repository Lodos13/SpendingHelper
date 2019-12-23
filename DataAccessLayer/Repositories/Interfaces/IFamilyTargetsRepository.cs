using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IFamilyTargetsRepository : IRepository<CFamilyTargetDto, FamilyTargetKey>
    {
        ICollection<CFamilyTargetDto> FindByFamilyId(Int32 familyId);
    }
}
