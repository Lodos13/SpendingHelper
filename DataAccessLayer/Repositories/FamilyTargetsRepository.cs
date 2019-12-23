using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NLog;

namespace DataAccessLayer
{
    class CFamilyTargetsRepository : IFamilyTargetsRepository
    {
        static Logger log = LogManager.GetCurrentClassLogger();

        #region ILoginRepository implementation

        public bool AddItem(CFamilyTargetDto item)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    context.CFamilyTargetsDto.Add(item);
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

        public bool UpdateItem(CFamilyTargetDto item)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    CFamilyTargetDto target = context.CFamilyTargetsDto.Where(t => t.FamilyID == item.FamilyID &&
                                                                                   t.CategoryID == item.CategoryID &&
                                                                                   t.Month == item.Month)
                                                                       .FirstOrDefault();

                    if (target == null)
                    {
                        log.Info("Can't update CFamilyTargets because it doesn't exist in database (FamilyId = {0}, categoryId = {1}, date = {2})",
                                 item.FamilyID, item.CategoryID, item.Month);
                        return false;
                    }

                    target.Type = item.Type;
                    target.Amount = item.Amount;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to update CFamilyTargets (FamilyId = {1}, categoryId = {2}, date = {3}). Message: {0}", 
                          ex.Message, item.FamilyID, item.CategoryID, item.Month);
                return false;
            }

            return true;
        }

        public bool DeleteItemByKey(FamilyTargetKey key)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    CFamilyTargetDto target = context.CFamilyTargetsDto.Where(t => t.FamilyID == key.FamilyId &&
                                                                                   t.CategoryID == key.CategoryId &&
                                                                                   t.Month == key.Month)
                                                                       .FirstOrDefault();
                    if (target == null)
                    {
                        log.Info("Can't delete CFamilyTargets because it doesn't exist in database (FamilyId = {0}, categoryId = {1}, date = {2})",
                                 key.FamilyId, key.CategoryId, key.Month);
                        return false;
                    }

                    context.CFamilyTargetsDto.Remove(target);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to delete CFamilyTargets (FamilyId = {1}, categoryId = {2}, date = {3}). Message: {0}",
                          ex.Message, key.FamilyId, key.CategoryId, key.Month);
                return false;
            }

            return true;
        }

        public CFamilyTargetDto FindItemByKey(FamilyTargetKey key)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    return context.CFamilyTargetsDto.Where(t => t.FamilyID == key.FamilyId &&
                                                                t.CategoryID == key.CategoryId &&
                                                                t.Month == key.Month)
                                                    .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to find CFamilyTargets by key (FamilyId = {1}, categoryId = {2}, date = {3}). Message: {0}",
                          ex.Message, key.FamilyId, key.CategoryId, key.Month);
                return null;
            }
        }

        public ICollection<CFamilyTargetDto> GetAllItems()
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    return context.CFamilyTargetsDto.ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to get all CFamilyTargets. Message: {0}", ex.Message);
                throw;
            }
        }

        #endregion

        public ICollection<CFamilyTargetDto> FindByFamilyId(int familyId)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    return context.CFamilyTargetsDto.Where(t => t.FamilyID == familyId).ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to find family's (id = {0}) targets in DB. Message: {1}", familyId, ex.Message);
                return null;
            }
        }
    }
}
