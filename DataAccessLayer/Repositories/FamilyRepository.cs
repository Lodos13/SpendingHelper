using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using NLog;

namespace DataAccessLayer
{
    class CFamilyRepository : IFamilyRepository
    {
        static Logger log = LogManager.GetCurrentClassLogger();

        #region ILoginRepository implementation

        public bool AddItem(CFamilyDto item)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    context.CFamiliesDto.Add(item);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to add family into DB. Message: {0}", ex.Message);
                return false;
            }

            return true;
        }

        public bool UpdateItem(CFamilyDto item)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    CFamilyDto family = context.CFamiliesDto.Where(f => f.FamilyID == item.FamilyID).FirstOrDefault();
                    if (family == null)
                    {
                        log.Info("Can't update family because it doesn't exist in database (familyId = {0})", item.FamilyID);
                        return false;
                    }

                    family.FamilyName = item.FamilyName;
                    family.Budget = item.Budget;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to update family in BD (FamilyId = {1}). Message: {0}", ex.Message, item.FamilyID);
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
                    CFamilyDto family = context.CFamiliesDto.Where(f => f.FamilyID == key).FirstOrDefault();
                    if (family == null)
                    {
                        log.Info("Can't delete family because it doesn't exist in database (familyId = {0})", key);
                        return false;
                    }

                    context.CFamiliesDto.Remove(family);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to delete family (FamilyId = {1}). Message: {0}", ex.Message, key);
                return false;
            }

            return true;
        }

        public CFamilyDto FindItemByKey(int key)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                    return context.CFamiliesDto.Where(f => f.FamilyID == key).FirstOrDefault();
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to find family (FamilyId = {1}). Message: {0}", ex.Message, key);
                return null;
            }
        }

        public ICollection<CFamilyDto> GetAllItems()
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                    return context.CFamiliesDto.ToList();
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to get all families. Message: {0}", ex.Message);
                throw;
            }
        }

        #endregion

        //Данный метод искуственно сделан через двойной context.SaveChanges, что бы выполнить задание - 2 изменения в одной транзацкии.
        public Boolean CreateFamily(Int32 personId, String familyName, Int32 budget)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    CPersonDto personDto = context.CPeopleDto.Where(p => p.PersonID == personId).FirstOrDefault();
                    if (personDto == null)
                    {
                        log.Warn("Can't create new family because person (id = {0}) doesn't exist", personId);
                        return false;
                    }
                    if(personDto.FamilyID != null)
                    {
                        log.Warn("Can't create new family because person (id = {0}) already has family!", personId);
                        return false;
                    }

                    using (System.Data.Entity.DbContextTransaction transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            CFamilyDto familyDto = new CFamilyDto();
                            familyDto.FamilyName = familyName;
                            familyDto.Budget = budget;
                            familyDto.MembersCounter = 1;

                            context.CFamiliesDto.Add(familyDto);
                            context.SaveChanges();

                            Int32 familyId = familyDto.FamilyID;
                            personDto.FamilyID = familyId;
                            context.SaveChanges();

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            log.Error(ex, "There is an error in transaction when created new family. Message: {0}", ex.Message);
                            transaction.Rollback();
                            return false;
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to create new family in DB. Message: {0}", ex.Message);
                return false;
            }
        }

        public Boolean JoinFamily(Int32 personId, Int32 familyId)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    CFamilyDto familyDto = context.CFamiliesDto.Where(f => f.FamilyID == familyId).FirstOrDefault();
                    if(familyDto == null)
                    {
                        log.Warn("Person (id = {0}) cant join to the family (id = {1}) because family doesn't exist", personId, familyId);
                        return false;
                    }

                    CPersonDto personDto = context.CPeopleDto.Where(p => p.PersonID == personId).FirstOrDefault();
                    if(personDto == null)
                    {
                        log.Warn("Person (id = {0}) cant join to the family (id = {1}) because person doesn't exist", personId, familyId);
                        return false;
                    }

                    personDto.Family = familyDto;
                    familyDto.MembersCounter++;
                    context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying person (id={1}) to join to family (id={2}) in DB. Message: {0}", ex.Message, personId, familyId);
                return false;
            }
        }

        public Boolean LeaveFamily(Int32 personId)
        {
            try
            {
                CPersonRepository personRep = new CPersonRepository();
                CPersonDto personDto = personRep.FindItemByKey(personId);

                if(personDto.FamilyID == null)
                {
                    log.Warn("Person (id = {1}) can't leave family because he doesn't have one.", personId);
                    return false;
                }

                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    System.Data.SqlClient.SqlParameter personIdParam = new System.Data.SqlClient.SqlParameter("@personId", personId);
                    context.Database.ExecuteSqlCommand("LeaveFamilyProc @personId", personIdParam);
                }

                return true;
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to leave family in DB. Message: {0}", ex.Message);
                return false;
            }
        }

        public CFamilyDto FindByPersonId(int personId)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    return context.CPeopleDto.Where(p => p.PersonID == personId).FirstOrDefault().Family;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to find family by personId (id = {0}) in DB. Message: {1}", personId, ex.Message);
                return null;
            }
        }

        public CFamilyDto FindByPersonIdFullLoad(int personId)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    System.Data.Entity.Infrastructure.DbQuery<CFamilyDto> q1 = context.CFamiliesDto
                                                                                      .Include("FamilyTargets")
                                                                                      .Include("People.Reminders")
                                                                                      .Include("People.PersonalTargets.Category")
                                                                                      .Include("People.SpendingHistories.Category")
                                                                                      .Include("People.SubCategories.Category")
                                                                                      .Include("People.SubCategories.Payments")
                                                                                      .Include("People.LoginData");
                    IQueryable<CFamilyDto> q2 = q1.Where(f => f.People.Select(p => p.PersonID).Contains(personId));
                    return q2.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to find family by personId (id = {0}) in DB (full data load method version). Message: {1}", personId, ex.Message);
                return null;
            }
        }
    }
}
