using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IRepository<Ttype, Tkey>
    {
        Ttype FindItemByKey(Tkey key);
        ICollection<Ttype> GetAllItems();

        bool AddItem(Ttype item);
        bool UpdateItem(Ttype item);
        bool DeleteItemByKey(Tkey key);
    }
}
