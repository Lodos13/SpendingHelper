using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NLog;

namespace DataAccessLayer
{
    class CSubCategoryRepository : ISubCategoryRepository
    {
        static Logger log = LogManager.GetCurrentClassLogger();

        #region ILoginRepository implementation

        public Boolean AddItem(CSubCategoryDto item)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    context.CSubCategoriesDto.Add(item);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to add subCategory into DB. Message: {0}", ex.Message);
                return false;
            }

            return true;
        }

        public Boolean UpdateItem(CSubCategoryDto item)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    CSubCategoryDto subCategory = context.CSubCategoriesDto.Where(s => s.SubCategoryID == item.SubCategoryID).FirstOrDefault();
                    if (subCategory == null)
                    {
                        log.Info("Can't update subCategory because it doesn't exist in database (SubCategoryid = {0})", item.SubCategoryID);
                        return false;
                    }

                    subCategory.ParentCategoryID = item.ParentCategoryID;
                    subCategory.Title = item.Title;
                    subCategory.Description = item.Description;

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to update subCategory (SubCategoryid = {1}). Message: {0}", ex.Message, item.SubCategoryID);
                return false;
            }

            return true;
        }

        public Boolean DeleteItemByKey(Int32 key)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    CSubCategoryDto subCategory = context.CSubCategoriesDto.Where(s => s.SubCategoryID == key).FirstOrDefault();
                    if (subCategory == null)
                    {
                        log.Info("Can't delete subCategory because it doesn't exist in database (SubCategoryid = {0})", key);
                        return false;
                    }

                    context.CSubCategoriesDto.Remove(subCategory);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to delete subCategory (SubCategoryId = {1}). Message: {0}", ex.Message, key);
                return false;
            }

            return true;
        }

        public CSubCategoryDto FindItemByKey(Int32 key)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                    return context.CSubCategoriesDto.Where(s => s.SubCategoryID == key).FirstOrDefault();
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to find subCategory (SubCategoryId = {1}). Message: {0}", ex.Message, key);
                return null;
            }
        }

        public ICollection<CSubCategoryDto> GetAllItems()
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                    return context.CSubCategoriesDto.ToList();
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to get all subCategories. Message: {0}", ex.Message);
                throw;
            }
        }

        #endregion

        public ICollection<CSubCategoryDto> FindByPersonId(Int32 personId)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    return context.CSubCategoriesDto.Where(s => s.PersonID == personId).ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to find person's (id = {0}) subCategories in DB. Message: {1}", personId, ex.Message);
                return null;
            }
        }

        public Int32 TryAddSubCategory(Int32 personId, String categoryTitle, String subCategoryTitle, String subCategoryDescription)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    CPersonDto personDto = context.CPeopleDto
                                                  .Include("SubCategories.Category")
                                                  .Where(p => p.PersonID == personId)
                                                  .FirstOrDefault();
                    if (personDto == null)
                    {
                        log.Info("Can't add payment because person (id = {0}) doesn't exist", personId);
                        return 0;
                    }


                    CCategoryDto category = context.CCategoriesDto.Where(c => c.Title.Equals(categoryTitle)).FirstOrDefault();
                    if(category == null)
                    {
                        log.Info("Can't add SubCategory because category (title  = {0}) doesn't exist", categoryTitle);
                        return 0;
                    }

                    CSubCategoryDto subCatDto = new CSubCategoryDto();
                    subCatDto.Person = personDto;
                    subCatDto.Title = subCategoryTitle;
                    subCatDto.Description = subCategoryDescription;
                    subCatDto.Category = category;

                    context.CSubCategoriesDto.Add(subCatDto);
                    context.SaveChanges();

                    return subCatDto.SubCategoryID;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to add subCategory into DB. Message: {0}", ex.Message);
                return 0;
            }
        }

        public Boolean TyrEditSubCategory(Int32 personId, Int32 subCategoryId, String newCategoryTitle, String newSubCategoryTitle, String newSubCategoryDescription)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    CSubCategoryDto subCategoryDto = context.CSubCategoriesDto.Where(s => s.SubCategoryID == subCategoryId).FirstOrDefault();
                    if(subCategoryDto == null)
                    {
                        log.Warn("Can't find subCategory (id = {0}) to edit)", subCategoryId);
                        return false;
                    }

                    if (subCategoryDto.PersonID != personId)
                    {
                        log.Warn("Can't edit subCategory because person (id = {0}) not an owner of subCategory (id = {1})", personId, subCategoryId);
                        return false;
                    }

                    if (newCategoryTitle != subCategoryDto.Category.Title)
                    {
                        CCategoryDto newCategoryDto = context.CCategoriesDto.Where(c => c.Title == newCategoryTitle).FirstOrDefault();
                        if (newCategoryDto == null)
                        {
                            log.Warn("Can't find new category (title = {0}) to edit subCategory(id = {1}))", newCategoryTitle, subCategoryId);
                            return false;
                        }

                        subCategoryDto.ParentCategoryID = newCategoryDto.CategoryID;
                    }

                    subCategoryDto.Title = newSubCategoryTitle;
                    subCategoryDto.Description = newSubCategoryDescription;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to edit subCategory (id = {1}) into DB. Message: {0}", ex.Message, subCategoryId);
                return false;
            }

            return true;
        }

        public Boolean TryDeleteSubCategory(Int32 personId, Int32 subCategoryId)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    CSubCategoryDto subCategoryDto = context.CSubCategoriesDto.Where(s => s.SubCategoryID == subCategoryId).FirstOrDefault();
                    if (subCategoryDto == null)
                    {
                        log.Warn("Can't find subCategory (id = {0}) to delete)", subCategoryId);
                        return false;
                    }

                    if (subCategoryDto.PersonID != personId)
                    {
                        log.Warn("Can't delete subCategory because person (id = {0}) not an owner of subCategory (id = {1})", personId, subCategoryId);
                        return false;
                    }
                }

                return DeleteItemByKey(subCategoryId);
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to edit subCategory (id = {1}) into DB. Message: {0}", ex.Message, subCategoryId);
                return false;
            }
        }
    }
}
