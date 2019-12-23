using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NLog;

namespace DataAccessLayer
{
    class CPaymentRepository : IPaymentRepository
    {
        static Logger log = LogManager.GetCurrentClassLogger();

        #region ILoginRepository implementation

        public bool AddItem(CPaymentDto item)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    context.CPaymentsDto.Add(item);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to add new payment into DB. Message: {0}", ex.Message);
                return false;
            }

            return true;
        }

        public bool UpdateItem(CPaymentDto item)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    CPaymentDto payment = context.CPaymentsDto.Where(p => p.PaymentID == item.PaymentID).FirstOrDefault();
                    if (payment == null)
                    {
                        log.Info("Can't update payment because it doesn't exist in database (paymentId = {0})", item.PaymentID);
                        return false;
                    }

                    payment.SubCategoryID = item.SubCategoryID;
                    payment.Date = item.Date;
                    payment.Spended = item.Spended;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to update payment (paymentId = {1}). Message: {0}", ex.Message, item.PaymentID);
                return false;
            }

            return true;
        }

        public bool DeleteItemByKey(Int32 key)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    CPaymentDto payment = context.CPaymentsDto.Where(p => p.PaymentID == key).FirstOrDefault();
                    if (payment == null)
                    {
                        log.Info("Can't update payment because it doesn't exist in database (paymentId = {0})", key);
                        return false;
                    }

                    context.CPaymentsDto.Remove(payment);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to delete payment (paymentId = {1}). Message: {0}", ex.Message, key);
                return false;
            }

            return true;
        }

        public CPaymentDto FindItemByKey(Int32 key)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                    return context.CPaymentsDto.Where(l => l.PaymentID == key).FirstOrDefault();
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to find payment (paymentId = {1}). Message: {0}", ex.Message, key);
                return null;
            }
        }

        public ICollection<CPaymentDto> GetAllItems()
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    return context.CPaymentsDto.ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to get all payments. Message: {0}", ex.Message);
                throw;
            }
        }

        #endregion

        public Int32 AddPayment(Int32 personId, DateTime date, String categoryTitle, String subCategoryTitle, Decimal spended)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    CPersonDto personDto = context.CPeopleDto
                                               .Include("SubCategories.Category")
                                               .Include("SubCategories.Payments")
                                               .Where(p => p.PersonID == personId)
                                               .FirstOrDefault();
                    if(personDto == null)
                    {
                        log.Info("Can't add payment because person (id = {0}) doesn't exist", personId);
                        return 0;
                    }

                    CSubCategoryDto subCategoryDto = personDto.SubCategories.Where(s => s.Title.Equals(subCategoryTitle) && s.Category.Title.Equals(categoryTitle)).FirstOrDefault();
                    if(subCategoryDto == null)
                    {
                        log.Info("Can't add payment because person (id = {0}) doesn't have subCategory (title = {1}) with category (title = {2})", 
                                  personId, subCategoryTitle, categoryTitle);
                        return 0;
                    }

                    CPaymentDto paymentDto = new CPaymentDto();
                    paymentDto.Date = date;
                    paymentDto.SubCategoryID = subCategoryDto.SubCategoryID;
                    paymentDto.Spended = spended;

                    //AddItem(paymentDto);
                    subCategoryDto.Payments.Add(paymentDto);
                    context.SaveChanges();

                    return paymentDto.PaymentID;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to add new payment into DB. Message: {0}", ex.Message);
                return 0;
            }
        }

        public Boolean EditPayment(Int32 personId, Int32 paymentId, DateTime newDate, String newCategoryTitle, String newSubCategoryTitle, Decimal newSum)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    if(context.CPaymentsDto.Where(p => p.PaymentID == paymentId).First().SubCategory.PersonID != personId)
                    {
                        log.Warn("Can't edit payment because person (id = {0}) not a owner of payment (id = {1})", personId, paymentId);
                        return false;
                    }

                    CPersonDto personDto = context.CPeopleDto.Where(p => p.PersonID == personId).FirstOrDefault();
                    if (personDto == null)
                    {
                        log.Info("Can't edit payment because person (id = {0}) doesn't exist", personId);
                        return false;
                    }

                    CSubCategoryDto subCategoryDto = personDto.SubCategories.Where(s => s.Title.Equals(newSubCategoryTitle) && s.Category.Title.Equals(newCategoryTitle)).FirstOrDefault();
                    if (subCategoryDto == null)
                    {
                        log.Info("Can't add payment because person (id = {0}) doesn't have subCategory (title = {1}) with category (title = {2})",
                                  personId, newSubCategoryTitle, newCategoryTitle);
                        return false;
                    }

                    CPaymentDto paymentDto = new CPaymentDto();
                    paymentDto.PaymentID = paymentId;
                    paymentDto.SubCategoryID = subCategoryDto.SubCategoryID;
                    paymentDto.Date = newDate;
                    paymentDto.Spended = newSum;

                    return UpdateItem(paymentDto);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to edit payment in DB. Message: {0}", ex.Message);
                return false;
            }
        }

        public Boolean DeletePayment(Int32 personId, Int32 paymentId)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    if (context.CPaymentsDto.Where(p => p.PaymentID == paymentId).First().SubCategory.PersonID != personId)
                    {
                        log.Warn("Can't delete payment because person (id = {0}) not a owner of payment (id = {1})", personId, paymentId);
                        return false;
                    }

                    return DeleteItemByKey(paymentId);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to edit payment in DB. Message: {0}", ex.Message);
                return false;
            }
        }

        public ICollection<CPaymentDto> FindBySubCategoryId(Int32 subCategoryId)
        {
            try
            {
                using (SpendingHelperDBEntities context = new SpendingHelperDBEntities())
                {
                    return context.CPaymentsDto.Where(p => p.SubCategoryID == subCategoryId).ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to find subCategory's (id = {0}) payments in DB. Message: {1}", subCategoryId, ex.Message);
                return null;
            }
        }


    }
}
