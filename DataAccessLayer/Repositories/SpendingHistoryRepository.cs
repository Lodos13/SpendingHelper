using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NLog;

namespace DataAccessLayer
{
    class CSpendingHistoryRepository : ISpendingHistoryRepository
    {
        static Logger log = LogManager.GetCurrentClassLogger();

        #region ILoginRepository implementation

        public bool AddItem(CSpendingHistoryDto item)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    context.CSpendingHistoriesDto.Add(item);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to add SpendingHistory into DB. Message: {0}", ex.Message);
                return false;
            }

            return true;
        }

        public bool UpdateItem(CSpendingHistoryDto item)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    CSpendingHistoryDto spendingHistory = context.CSpendingHistoriesDto.Where(h => h.PersonID == item.PersonID &&
                                                                                                   h.CategoryID == item.CategoryID &&
                                                                                                   h.Month == item.Month)
                                                                                       .FirstOrDefault();
                    if (spendingHistory == null)
                    {
                        log.Info("Can't update SpendingHistory because it doesn't exist in database (personId = {0}, categoryId = {1}, Month: {2})", 
                                 item.PersonID, item.CategoryID, item.Month);
                        return false;
                    }

                    spendingHistory.Spended = item.Spended;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to update SpendingHistory (personId = {1}, categoryId = {2}, Month: {3}). Message: {0}", 
                          ex.Message, item.PersonID, item.CategoryID, item.Month);
                return false;
            }

            return true;
        }

        public bool DeleteItemByKey(CSpendingHistoryKey key)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    CSpendingHistoryDto spendingHistory = context.CSpendingHistoriesDto.Where(h => h.PersonID == key.PersonId &&
                                                                                                   h.CategoryID == key.CategoryId &&
                                                                                                   h.Month == key.Month)
                                                                                       .FirstOrDefault();
                    if (spendingHistory == null)
                    {
                        log.Info("Can't Delete SpendingHistory because it doesn't exist in database (personId = {0}, categoryId = {1}, Month: {2})",
                                 key.PersonId, key.CategoryId, key.Month);
                        return false;
                    }

                    context.CSpendingHistoriesDto.Remove(spendingHistory);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to delete SpendingHistory (personId = {1}, categoryId = {2}, Month: {3}). Message: {0}",
                          ex.Message, key.PersonId, key.CategoryId, key.Month);
                return false;
            }

            return true;
        }

        public CSpendingHistoryDto FindItemByKey(CSpendingHistoryKey key)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                    return context.CSpendingHistoriesDto.Where(h => h.PersonID == key.PersonId &&
                                                                    h.CategoryID == key.CategoryId &&
                                                                    h.Month == key.Month)
                                                        .FirstOrDefault();
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to find SpendingHistory in DB (personId = {1}, categoryId = {2}, Month: {3}). Message: {0}",
                          ex.Message, key.PersonId, key.CategoryId, key.Month);
                return null;
            }
        }

        public ICollection<CSpendingHistoryDto> GetAllItems()
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    return context.CSpendingHistoriesDto.ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to get all SpendingHistories from DB. Message: {0}", ex.Message);
                throw;
            }
        }

        #endregion

        public ICollection<CSpendingHistoryDto> FindByPersonId(int personId)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    return context.CSpendingHistoriesDto.Where(s => s.PersonID == personId).ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to find person's (id = {0}) spending history in DB. Message: {1}", personId, ex.Message);
                return null;
            }
        }
    }
}
