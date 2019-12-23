using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NLog;

namespace DataAccessLayer
{
    class CRemindersRepository : IRemindersRepository
    {
        static Logger log = LogManager.GetCurrentClassLogger();

        #region ILoginRepository implementation

        public bool AddItem(CReminderDto item)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    context.CRemindersDto.Add(item);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to add CReminders into DB. Message: {0}", ex.Message);
                return false;
            }

            return true;
        }

        public bool UpdateItem(CReminderDto item)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    CReminderDto reminder = context.CRemindersDto.Where(r => r.PersonID == item.PersonID && r.Title.Equals(item.Title)).FirstOrDefault();

                    if (reminder == null)
                    {
                        log.Info("Can't update reminder because it doesn't exist in database id = {0}, Title: {1}", item.PersonID, item.Title);
                        return false;
                    }

                    reminder.DayOfMonth = item.DayOfMonth;
                    reminder.Description = item.Description;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to update CReminders (id = {1} title: {2}). Message: {0}", ex.Message, item.PersonID, item.Title);
                return false;
            }

            return true;
        }

        public bool DeleteItemByKey(CReminderKey key)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    CReminderDto reminder = context.CRemindersDto.Where(r => r.PersonID == key.PersonId && r.Title.Equals(key.Title)).FirstOrDefault();
                    if (reminder == null)
                    {
                        log.Info("Can't delete reminder because it doesn't exist in database id = {0}, Title: {1}", key.PersonId, key.Title);
                        return false;
                    }

                    context.CRemindersDto.Remove(reminder);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to delete CReminders (id = {1} title: {2}). Message: {0}", ex.Message, key.PersonId, key.Title);
                return false;
            }

            return true;
        }

        public CReminderDto FindItemByKey(CReminderKey key)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    return context.CRemindersDto.Where(r => r.PersonID == key.PersonId && r.Title.Equals(key.Title)).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to find CReminders by key (id = {1} title: {2}). Message: {0}", ex.Message, key.PersonId, key.Title);
                return null;
            }
        }

        public ICollection<CReminderDto> GetAllItems()
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    return context.CRemindersDto.ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to get all CReminders. Message: {0}", ex.Message);
                throw;
            }
        }

        #endregion

        public ICollection<CReminderDto> FindByPersonId(int personId)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    return context.CRemindersDto.Where(r => r.PersonID == personId).ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to find person's (id = {0}) reminders in DB. Message: {1}", personId, ex.Message);
                return null;
            }
        }
    }
}
