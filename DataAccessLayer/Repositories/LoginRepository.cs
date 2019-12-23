using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NLog;

namespace DataAccessLayer
{
    class CLoginRepository : ILoginRepository
    {
        static Logger log = LogManager.GetCurrentClassLogger();

        #region ILoginRepository implementation

        public bool AddItem(CLoginDataDto item)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    context.CLoginDatasDto.Add(item);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to add loginData into DB. Message: {0}", ex.Message);
                return false;
            }

            return true;
        }

        public bool UpdateItem(CLoginDataDto item)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    CLoginDataDto loginData = context.CLoginDatasDto.Where(l => l.PersonID == item.PersonID).FirstOrDefault();
                    if(loginData == null)
                    {
                        log.Info("Can't update loginData because it doesn't exist in database (personId = {0})", item.PersonID);
                        return false;
                    }

                    loginData.email = item.email;
                    loginData.Username = item.Username;
                    loginData.PasswordHash = item.PasswordHash;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to update loginData (PersonId = {1}). Message: {0}", ex.Message, item.PersonID);
                return false;
            }

            return true;
        }

        public bool DeleteItemByKey(int key)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    CLoginDataDto loginData = context.CLoginDatasDto.Where(l => l.PersonID == key).FirstOrDefault();
                    if (loginData == null)
                    {
                        log.Info("Can't delete loginData because it doesn't exist in database (personId = {0})", key);
                        return false;
                    }

                    context.CLoginDatasDto.Remove(loginData);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to delete loginData (PersonId = {1}). Message: {0}", ex.Message, key);
                return false;
            }

            return true;
        }

        public CLoginDataDto FindItemByKey(int key)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                    return context.CLoginDatasDto.Where(l => l.PersonID == key).FirstOrDefault();
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to find loginData (PersonId = {1}). Message: {0}", ex.Message, key);
                return null;
            }
        }

        public ICollection<CLoginDataDto> GetAllItems()
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                    return context.CLoginDatasDto.ToList();
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to get all loginDatas. Message: {0}", ex.Message);
                throw;
            }
        }

        #endregion

        public CLoginDataDto FindLoginDataByEmail(string email)
        {
            try
            {
                //SpendingHelperDBEntities context = CSConnectionPool.GetConnection();
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    CLoginDataDto loginDto = (from login in context.CLoginDatasDto
                                              where login.email.ToLower().Equals(email.ToLower())
                                              select login).FirstOrDefault();
                    return loginDto;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to FindLoginDataByEmail. Message: {0}", ex.Message);
                return null;
            }
        }

        public CLoginDataDto FindLoginDataByUsername(string username)
        {
            try
            {
                SpendingHelperDBEntities connection = CSConnectionPool.GetConnection();
                CLoginDataDto loginDto = connection.CLoginDatasDto.FirstOrDefault(l => l.Username.Equals(username));
                return loginDto;
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to FindLoginDataByUsername. Message: {0}", ex.Message);
                return null;
            }
        }

    }
}
