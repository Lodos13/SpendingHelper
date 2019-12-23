using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

using NLog;

using BusinessLogicLayer;
using Utils;

namespace BllHost
{
    public class BllFacadeForUIController : ApiController, IBllFacadeForUI
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        IBllFacadeForUI _recorder = new CBllFacadeForUI();


        [HttpGet]
        public CPerson GetPerson(String token)
        {
            try
            {
                log.Trace("Entered BllFacadeForUIController.GetPerson");

                return _recorder.GetPerson(token);
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in BllFacadeForUIController.GetPerson. Message: {0}", ex.Message);
                return null;
            }
        }

        [HttpGet]
        public ICollection<CCategoryType> GetAllRegisterdCategories()
        {
            try
            {
                log.Trace("Entered BllFacadeForUIController.GetAllRegisterdCategories");
                return _recorder.GetAllRegisterdCategories();
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in BllFacadeForUIController.GetAllRegisterdCategories. Message: {0}", ex.Message);
                return null;
            }
        }


        [HttpGet]
        public CFamilyAccessResult GetFamily(String token)
        {
            try
            {
                log.Trace("Entered BllFacadeForUIController.GetFamily");
                return _recorder.GetFamily(token);
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in BllFacadeForUIController.GetFamily. Message: {0}", ex.Message);
                return null;
            }
        }

        [HttpPost]
        public Boolean CreateFamily(String token, String familyName, Int32 budget)
        {
            try
            {
                log.Trace("Entered BllFacadeForUIController.CreateFamily");
                return _recorder.CreateFamily(token, familyName, budget);
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in BllFacadeForUIController.CreateFamily. Message: {0}", ex.Message);
                return false;
            }
        }

        [HttpPut]
        public Boolean JoinFamily(String token, Int32 familyId)
        {
            try
            {
                log.Trace("Entered BllFacadeForUIController.JoinFamily");
                return _recorder.JoinFamily(token, familyId);
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in BllFacadeForUIController.JoinFamily. Message: {0}", ex.Message);
                return false;
            }
        }

        [HttpPut]
        public Boolean LeaveFamily(String token)
        {
            try
            {
                log.Trace("Entered BllFacadeForUIController.LeaveFamily");
                return _recorder.LeaveFamily(token);
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in BllFacadeForUIController.LeaveFamily. Message: {0}", ex.Message);
                return false;
            }
        }


        [HttpPost]
        public Int32 AddPayment(String token, String dateStr, String category, String subCategory, Decimal spended)
        {
            log.Trace("Entered BllFacadeForUIController.AddPayment");
            DateTime date = CSDateTimeHelper.ConvertStringToDate(dateStr);
            return AddPayment(token, date, category, subCategory, spended);
        }
        [NonAction]
        public Int32 AddPayment(String token, DateTime date, String category, String subCategory, Decimal spended)
        {
            try
            {
                log.Trace("Entered BllFacadeForUIController.AddPayment 2");
                return _recorder.AddPayment(token, date, category, subCategory, spended);
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in BllFacadeForUIController.AddPayment. Message: {0}", ex.Message);
                return 0;
            }
        }

        [HttpPut]
        public Boolean EditPayment(String token, Int32 paymentId, String newDateStr, String newCategoryTitle, String newSubCategoryTitle, Decimal newSum)
        {
            log.Trace("Entered BllFacadeForUIController.EditPayment");
            DateTime newDate = CSDateTimeHelper.ConvertStringToDate(newDateStr);
            return EditPayment(token, paymentId, newDate, newCategoryTitle, newSubCategoryTitle, newSum);
        }
        [NonAction]
        public Boolean EditPayment(String token, Int32 paymentId, DateTime newDate, String newCategoryTitle, String newSubCategoryTitle, Decimal newSum)
        {
            try
            {
                log.Trace("Entered BllFacadeForUIController.EditPayment");
                return _recorder.EditPayment(token, paymentId, newDate, newCategoryTitle, newSubCategoryTitle, newSum);
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in BllFacadeForUIController.EditPayment. Message: {0}", ex.Message);
                return false;
            }
        }

        [HttpDelete]
        public Boolean DeletePayment(String token, Int32 paymentId)
        {
            try
            {
                log.Trace("Entered BllFacadeForUIController.DeletePayment");
                return _recorder.DeletePayment(token, paymentId);
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in BllFacadeForUIController.DeletePayment. Message: {0}", ex.Message);
                return false;
            }
        }



        [HttpPost]
        public Int32 AddSubCategory(String token, String categoryTitle, String subCategoryTitle, String subCategoryDescription)
        {
            log.Trace("Entered BllFacadeForUIController.AddSubCategory");
            return _recorder.AddSubCategory(token, categoryTitle, subCategoryTitle, subCategoryDescription);
        }

        [HttpPut]
        public Boolean EditSubCategory(String token, Int32 subCategoryId, String newCategoryTitle, String newSubCategoryTitle, String newSubCategoryDescription)
        {
            try
            {
                log.Trace("Entered BllFacadeForUIController.EditSubCategory");
                return _recorder.EditSubCategory(token, subCategoryId, newCategoryTitle, newSubCategoryTitle, newSubCategoryDescription);
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in BllFacadeForUIController.EditSubCategory. Message: {0}", ex.Message);
                return false;
            }
        }

        [HttpDelete]
        public Boolean DeleteSubCategory(String token, Int32 subCategoryId)
        {
            try
            {
                log.Trace("Entered BllFacadeForUIController.DeleteSubCategory");
                return _recorder.DeleteSubCategory(token, subCategoryId);
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in BllFacadeForUIController.DeleteSubCategory. Message: {0}", ex.Message);
                return false;
            }
        }



        [HttpGet]
        public CLoginVerificationResult VerifyPassword(String username, String plainPassword)
        {
            try
            {
                log.Trace("Entered BllFacadeForUIController.VerifyPassword");
                return _recorder.VerifyPassword(username, plainPassword);
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in BllFacadeForUIController.VerifyPassword. Message: {0}", ex.Message);
                return null;
            }
        }

        [HttpPost]
        public CRegistationResult RegisterPerson(String name, String username, String email, String plainPassword, String plainPasswordConfirmation)
        {
            try
            {
                log.Trace("Entered BllFacadeForUIController.RegisterPerson");
                return _recorder.RegisterPerson(name, username, email, plainPassword, plainPasswordConfirmation);
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in BllFacadeForUIController.RegisterPerson. Message: {0}", ex.Message);
                return null;
            }
        }

        [HttpDelete]
        public Boolean LogOut(String token)
        {
            try
            {
                log.Trace("Entered BllFacadeForUIController.LogOut");
                return _recorder.LogOut(token);
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in BllFacadeForUIController.LogOut. Message: {0}", ex.Message);
                return false;
            }
        }

        [HttpGet]
        public String Ping()
        {
            return "Pong";
        }

        
    }
}
