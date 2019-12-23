using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NLog;

namespace DataAccessLayer
{
    class CPersonalTargetsRepository : IPersonalTargetsRepository
    {
        static Logger log = LogManager.GetCurrentClassLogger();

        #region ILoginRepository implementation

        public bool AddItem(CPersonalTargetDto item)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    context.CPersonalTargetsDto.Add(item);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to add CFamilyTargets into DB. Message: {0}", ex.Message);
                return false;
            }

            return true;
        }

        public bool UpdateItem(CPersonalTargetDto item)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    CPersonalTargetDto target = context.CPersonalTargetsDto.Where(t => t.PersonID == item.PersonID &&
                                                                                       t.CategoryID == item.CategoryID &&
                                                                                       t.Month == item.Month)
                                                                           .FirstOrDefault();
                    if (target == null)
                    {
                        log.Info("Can't update CPersonalTarget because it doesn't exist in database (PersonId = {0}, categoryId = {1}, date = {2})",
                                 item.PersonID, item.CategoryID, item.Month);
                        return false;
                    }

                    target.Type = item.Type;
                    target.Amount = item.Amount;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to update CPersonalTarget (PersonId = {1}, categoryId = {2}, date = {3}). Message: {0}",
                          ex.Message, item.PersonID, item.CategoryID, item.Month);
                return false;
            }

            return true;
        }

        public bool DeleteItemByKey(CPersonalTargetKey key)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    CPersonalTargetDto target = context.CPersonalTargetsDto.Where(t => t.PersonID == key.PersonId &&
                                                                                       t.CategoryID == key.CategoryId &&
                                                                                       t.Month == key.Month)
                                                                           .FirstOrDefault();
                    if (target == null)
                    {
                        log.Info("Can't delete CPersonalTarget because it doesn't exist in database (PersonId = {0}, categoryId = {1}, date = {2})",
                                 key.PersonId, key.CategoryId, key.Month);
                        return false;
                    }

                    context.CPersonalTargetsDto.Remove(target);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to delete CPersonalTargets (PersonId = {1}, categoryId = {2}, date = {3}). Message: {0}",
                          ex.Message, key.PersonId, key.CategoryId, key.Month);
                return false;
            }

            return true;
        }

        public CPersonalTargetDto FindItemByKey(CPersonalTargetKey key)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    return context.CPersonalTargetsDto.Where(t => t.PersonID == key.PersonId &&
                                                                  t.CategoryID == key.CategoryId &&
                                                                  t.Month == key.Month)
                                                      .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to find CPersonalTargets by key (PersonId = {1}, categoryId = {2}, date = {3}). Message: {0}",
                          ex.Message, key.PersonId, key.CategoryId, key.Month);
                return null;
            }
        }

        public ICollection<CPersonalTargetDto> GetAllItems()
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    return context.CPersonalTargetsDto.ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to get all CPersonalTargets. Message: {0}", ex.Message);
                throw;
            }
        }

        #endregion

        public ICollection<CPersonalTargetDto> FindByPersonId(int personId)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    return context.CPersonalTargetsDto.Where(t => t.PersonID == personId).ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to find person's (id = {0}) personal targets in DB. Message: {1}", personId, ex.Message);
                return null;
            }
        }
    }
}
