using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface ILoginRepository : IRepository<CLoginDataDto, Int32>
    {
        CLoginDataDto FindLoginDataByEmail(String email);
        CLoginDataDto FindLoginDataByUsername(String userName);
    }
}
