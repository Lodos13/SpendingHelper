using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public static class CSRepositoryFactory
    {
        public static ICategoryRepository GetCategoryRepository()
        {
            return new CCategoryRepository();
        }
        public static IFamilyRepository GetFamilyRepository()
        {
            return new CFamilyRepository();
        }
        public static ILoginRepository GetLoginRepository()
        {
            return new CLoginRepository();
        }
        public static IPaymentRepository GetPaymentRepository()
        {
            return new CPaymentRepository();
        }
        public static IPersonRepository GetPersonRepository()
        {
            return new CPersonRepository();
        }
        public static ISubCategoryRepository getSubCategoryRepository()
        {
            return new CSubCategoryRepository();
        }
        public static ISpendingHistoryRepository GetSpendingHistoryRepository()
        {
            return new CSpendingHistoryRepository();
        }
        public static IPersonalTargetsRepository GetPersonalTargetsRepository()
        {
            return new CPersonalTargetsRepository();
        }
        public static IRemindersRepository GetRemindersRepository()
        {
            return new CRemindersRepository();
        }
        public static IFamilyTargetsRepository GetFamilyTargetsRepository()
        {
            return new CFamilyTargetsRepository();
        }
    }
}
