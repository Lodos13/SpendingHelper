using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface ISpendingHistoryRepository : IRepository<CSpendingHistoryDto, CSpendingHistoryKey>
    {
        ICollection<CSpendingHistoryDto> FindByPersonId(Int32 personId);
    }
}
