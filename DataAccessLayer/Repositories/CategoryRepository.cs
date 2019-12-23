using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NLog;

namespace DataAccessLayer
{
    class CCategoryRepository : ICategoryRepository
    {
        static Logger log = LogManager.GetCurrentClassLogger();

        #region ILoginRepository implementation

        public bool AddItem(CCategoryDto item)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    context.CCategoriesDto.Add(item);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to add new category into DB. Message: {0}", ex.Message);
                return false;
            }

            return true;
        }

        public bool UpdateItem(CCategoryDto item)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    CCategoryDto category = context.CCategoriesDto.Where(c => c.CategoryID == item.CategoryID).FirstOrDefault();
                    if (category == null)
                    {
                        log.Info("Can't update category because it doesn't exist in database (categoryId = {0})", item.CategoryID);
                        return false;
                    }

                    category.Title = item.Title;
                    category.Description = item.Description;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to update category (Categoryid = {1}). Message: {0}", ex.Message, item.CategoryID);
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
                    CCategoryDto category = context.CCategoriesDto.Where(c => c.CategoryID == key).FirstOrDefault();
                    if (category == null)
                    {
                        log.Info("Can't delete category because it doesn't exist in database (categoryId = {0})", key);
                        return false;
                    }

                    context.CCategoriesDto.Remove(category);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to delete category frim DB (CategoryId = {1}). Message: {0}", ex.Message, key);
                return false;
            }

            return true;
        }

        public CCategoryDto FindItemByKey(int key)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                    return context.CCategoriesDto.Where(c => c.CategoryID == key).FirstOrDefault();
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to find category in DB (CategoryId = {1}). Message: {0}", ex.Message, key);
                return null;
            }
        }

        public ICollection<CCategoryDto> GetAllItems()
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                    return context.CCategoriesDto.ToList();
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to get all Categories from DB. Message: {0}", ex.Message);
                return new List<CCategoryDto>();
            }
        }

        #endregion
    }
}
