using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NLog;

namespace DataAccessLayer
{
    class CPersonRepository : IPersonRepository
    {
        static Logger log = LogManager.GetCurrentClassLogger();

        #region ILoginRepository implementation

        public bool AddItem(CPersonDto item)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    context.CPeopleDto.Add(item);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to add PersonDto into DB. Message: {0}", ex.Message);
                return false;
            }

            return true;
        }

        public bool UpdateItem(CPersonDto item)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    CPersonDto person = context.CPeopleDto.Where(p => p.PersonID == item.PersonID).FirstOrDefault();
                    if (person == null)
                    {
                        log.Info("Can't update person data because it doesn't exist in database (personId = {0})", item.PersonID);
                        return false;
                    }

                    person.Name = item.Name;
                    person.FamilyID = item.FamilyID;
                    person.Budget = item.Budget;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to update person in DB (PersonId = {1}). Message: {0}", ex.Message, item.PersonID);
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
                    CPersonDto person = context.CPeopleDto.Where(p => p.PersonID == key).FirstOrDefault();
                    if (person == null)
                    {
                        log.Info("Can't delete person data because it doesn't exist in database (personId = {0})", key);
                        return false;
                    }

                    context.CPeopleDto.Remove(person);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to delete person data from DB (PersonId = {1}). Message: {0}", ex.Message, key);
                return false;
            }

            return true;
        }

        public CPersonDto FindItemByKey(int key)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    return context.CPeopleDto.FirstOrDefault(p => p.PersonID == key);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to find Person (id = {0}) in DB. Message: {1}", key, ex.Message);
                return null;
            }
        }

        public ICollection<CPersonDto> GetAllItems()
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                    return context.CPeopleDto.ToList();
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to get all people data. Message: {0}", ex.Message);
                throw;
            }
        }

        #endregion

        public CPersonDto FindItemByKeyFullLoad(int key)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    System.Data.Entity.Infrastructure.DbQuery<CPersonDto> query = context.CPeopleDto
                                                                                         .Include("Reminders")
                                                                                         .Include("PersonalTargets")
                                                                                         .Include("SpendingHistories")
                                                                                         .Include("SubCategories")
                                                                                         .Include("SubCategories.Payments")
                                                                                         .Include("LoginData")
                                                                                         .Include("Family.FamilyTargets")
                                                                                         .Include("Family.People.Reminders")
                                                                                         .Include("Family.People.PersonalTargets") 
                                                                                         .Include("Family.People.SpendingHistories")
                                                                                         .Include("Family.People.SubCategories") 
                                                                                         .Include("Family.People.SubCategories.Payments")
                                                                                         .Include("Family.People.LoginData");
                    CPersonDto person = query.FirstOrDefault(p => p.PersonID == key);
                    return person;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to find Person (id = {0}) in DB. Message: {1}", key, ex.Message);
                return null;
            }
        }

        public ICollection<CPersonDto> FindByFamilyId(int familyId)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    return context.CPeopleDto.Where(p => p.FamilyID == familyId).ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to find Person by family (id = {0}) in DB. Message: {1}", familyId, ex.Message);
                return null;
            }
        }
    }
}
