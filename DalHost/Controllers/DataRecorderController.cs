using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

using NLog;

using BlltoDalTransport;
using Utils;

namespace DalHost
{
    public class DataRecorderController : ApiController, IDataRecorder
    {
        static private Logger log = LogManager.GetCurrentClassLogger();

        IDataRecorder _recorder = new CDataRecorderDB();

        [HttpPost]
        public void RegisterPerson(String name, String username, String email, String salt, String passwordHash)
        {
            try
            {
                _recorder.RegisterPerson(name, username, email, salt, passwordHash);
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in DataRecorderController.RegisterPerson. Message: {0}", ex.Message);
                return;
            }
        }



        [HttpPost]
        public Int32 AddPayment(Int32 personId, String dateStr, String category, String subCategory, Decimal spended)
        {
            DateTime date = CSDateTimeHelper.ConvertStringToDate(dateStr);
            return AddPayment(personId, date, category, subCategory, spended);
        }
        [NonAction]
        public Int32 AddPayment(Int32 personId, DateTime date, String category, String subCategory, Decimal spended)
        {
            try
            {
                return _recorder.AddPayment(personId, date, category, subCategory, spended);
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in DataRecorderController.AddPayment (personId = {1}). Message: {0}", ex.Message, personId);
                return 0;
            };
        }

        [HttpPut]
        public Boolean EditPayment(Int32 personId, Int32 paymentId, String newDateStr, String newCategoryTitle, String newSubCategoryTitle, Decimal newSum)
        {
            DateTime newDate = CSDateTimeHelper.ConvertStringToDate(newDateStr);
            return EditPayment(personId, paymentId, newDate, newCategoryTitle, newSubCategoryTitle, newSum);
        }
        [NonAction]
        public Boolean EditPayment(Int32 personId, Int32 paymentId, DateTime newDate, String newCategoryTitle, String newSubCategoryTitle, Decimal newSum)
        {
            try
            {
                return _recorder.EditPayment(personId, paymentId, newDate, newCategoryTitle, newSubCategoryTitle, newSum);
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in DataRecorderController.EditPayment (personId = {1}). Message: {0}", ex.Message, personId);
                return false;
            }
        }

        [HttpDelete]
        public Boolean DeletePayment(Int32 personId, Int32 paymentId)
        {
            try
            {
                return _recorder.DeletePayment(personId, paymentId);
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in DataRecorderController.DeletePayment (personId = {1}). Message: {0}", ex.Message, personId);
                return false;
            }
        }



        [HttpPost]
        public Int32 AddSubCategory(Int32 personId, String categoryTitle, String subCategoryTitle, String subCategoryDescription)
        {
            try
            {
                return _recorder.AddSubCategory(personId, categoryTitle, subCategoryTitle, subCategoryDescription);
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in DataRecorderController.AddSubCategory (personId = {1}). Message: {0}", ex.Message, personId);
                return 0;
            };
        }

        [HttpPut]
        public Boolean EditSubCategory(Int32 personId, Int32 subCategoryId, String newCategoryTitle, String newSubCategoryTitle, String newSubCategoryDescription)
        {
            try
            {
                return _recorder.EditSubCategory(personId, subCategoryId, newCategoryTitle, newSubCategoryTitle, newSubCategoryDescription);
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in DataRecorderController.EditSubCategory (personId = {1}, subCategoryId = {2}). Message: {0}", ex.Message, personId, subCategoryId);
                return false;
            }
        }

        [HttpDelete]
        public Boolean DeleteSubCategory(Int32 personId, Int32 subCategoryId)
        {
            try
            {
                return _recorder.DeleteSubCategory(personId, subCategoryId);
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in DataRecorderController.EditSubCategory (personId = {1}, subCategoryId = {2}). Message: {0}", ex.Message, personId, subCategoryId);
                return false;
            }
        }


        [HttpPost]
        public Boolean CreateFamily(Int32 personId, String familyName, Int32 budget)
        {
            try
            {
                return _recorder.CreateFamily(personId, familyName, budget);
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in DataRecorderController.CreateFamily (personId = {1}). Message: {0}", ex.Message, personId);
                return false;
            }
        }

        [HttpPut]
        public Boolean JoinFamily(Int32 personId, Int32 familyId)
        {
            try
            {
                return _recorder.JoinFamily(personId, familyId);
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in DataRecorderController.JoinFamily (personId = {1}). Message: {0}", ex.Message, personId);
                return false;
            }
        }

        [HttpPut]
        public Boolean LeaveFamily(Int32 personId)
        {
            try
            {
                return _recorder.LeaveFamily(personId);
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in DataRecorderController.LeaveFamily (personId = {1}). Message: {0}", ex.Message, personId);
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
