using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IPersonalTargetsRepository : IRepository<CPersonalTargetDto, CPersonalTargetKey>
    {
        ICollection<CPersonalTargetDto> FindByPersonId(Int32 personId);
    }
}
