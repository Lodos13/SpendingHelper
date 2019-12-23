using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NLog;

namespace BlltoDalTransport
{
    internal class CDataSupplierProxy : IDataSupplier
    {
        static private Logger log = LogManager.GetCurrentClassLogger();

        private IDataSupplier _supplier;

        public CDataSupplierProxy(String source = "DAL")
        {
            if (source.Equals("DAL"))
                _supplier = new CDataSupplierDB();
            else
            {
                log.Fatal("Data supplier are not defined");
                throw new ArgumentException();
            }
        }

        public List<CCategoryType> GetCategoryTypes()
        {
            return _supplier.GetCategoryTypes();
        }

        public CPerson GetPersonById(int personId)
        {
            return _supplier.GetPersonById(personId);
        }

        public CFamily GetFamilyByPersonId(int personId)
        {
            return _supplier.GetFamilyByPersonId(personId);
        }


        public CLoginData FindLoginByUsernameOrEmail(String username)
        {
            return _supplier.FindLoginByUsernameOrEmail(username);
        }

        public bool IsUsernameExisted(string username)
        {
            return _supplier.IsUsernameExisted(username);
        }

        public bool IsEmailExisted(string email)
        {
            return _supplier.IsEmailExisted(email);
        }
    }
}
