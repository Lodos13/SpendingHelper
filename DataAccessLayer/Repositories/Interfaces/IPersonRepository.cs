using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IPersonRepository : IRepository<CPersonDto,Int32>
    {
        CPersonDto FindItemByKeyFullLoad(Int32 key);
        ICollection<CPersonDto> FindByFamilyId(Int32 familyId);
    }
}
