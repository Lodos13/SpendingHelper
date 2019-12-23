using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    //TODO: create pool
    //this class is not pool yet
    class CSConnectionPool
    {
        static private SpendingHelperDBEntities db;
        static CSConnectionPool()
        {
            db = new SpendingHelperDBEntities();
        }
        static public SpendingHelperDBEntities GetConnection()
        {
            return db;
        }
    }
}
