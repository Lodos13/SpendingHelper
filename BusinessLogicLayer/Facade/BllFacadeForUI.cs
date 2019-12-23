using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NLog;

using LoginService;
using BlltoDalTransport;
using System.Security;

namespace BusinessLogicLayer
{
    public class CBllFacadeForUI : IBllFacadeForUI
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Return NULL if person's data doesn't exist in DB
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public CPerson GetPerson(String token)
        {
            try
            {
                log.Trace("Entered GetPerson");

                Int32 personId;
                if (IsTokenBad(token, out personId))
                    return null;

                IDataSupplier dataSupplier = new CDataSupplierProxy();
                return dataSupplier.GetPersonById(personId);
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure in CBllFacadeForUI.GetPerson. Message: {0}", ex.Message);
                return null;
            }
        }


        public ICollection<CCategoryType> GetAllRegisterdCategories()
        {
            try
            {
                log.Trace("Entered GetAllRegisterdCategories");
                IDataSupplier dataSupplier = new CDataSupplierProxy();
                return dataSupplier.GetCategoryTypes();
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure in CBllFacadeForUI.GetAllRegisterdCategories. Message: {0}", ex.Message);
                return null;
            }
        }


        public CFamilyAccessResult GetFamily(String token)
        {
            try
            {
                log.Trace("Entered GetFamily");

                Int32 personId;
                if (IsTokenBad(token, out personId))
                    return new CFamilyAccessResult(true, false, false, null);

                IDataSupplier dataSupplier = new CDataSupplierProxy();
                CFamily family = dataSupplier.GetFamilyByPersonId(personId);
                if (family == null)
                    return new CFamilyAccessResult(false, true, false, null);

                if (!family.FamilyMembers.Select(m => m.PersonID).Contains(personId))
                    return new CFamilyAccessResult(false, false, true, null);

                return new CFamilyAccessResult(false, false, false, family);
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure in CBllFacadeForUI.GetFamily. Message: {0}", ex.Message);
                return null;
            }
        }

        public Boolean CreateFamily(String token, String familyName, Int32 budget)
        {
            try
            {
                log.Trace("Entered CreateFamily");

                Int32 personId;
                if (IsTokenBad(token, out personId))
                {
                    log.Info("Token is invalid. Can't create family. Token: {0}", token);
                    return false;
                }

                IDataRecorder recorder = new CDataRecorderProxy();
                return recorder.CreateFamily(personId, familyName, budget);
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure in CBllFacadeForUI.CreateFamily. Message: {0}", ex.Message);
                return false;
            }
        }

        public Boolean JoinFamily(String token, Int32 familyId)
        {
            try
            {
                log.Trace("Entered JoinFamily");

                Int32 personId;
                if (IsTokenBad(token, out personId))
                {
                    log.Info("Token is invalid. Can't create family. Token: {0}", token);
                    return false;
                }

                IDataRecorder recorder = new CDataRecorderProxy();
                return recorder.JoinFamily(personId, familyId);
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure in CBllFacadeForUI.CreateFamily. Message: {0}", ex.Message);
                return false;
            }
        }

        public Boolean LeaveFamily(String token)
        {
            try
            {
                log.Trace("Entered LeaveFamily");

                Int32 personId;
                if (IsTokenBad(token, out personId))
                {
                    log.Info("Token is invalid. Can't create family. Token: {0}", token);
                    return false;
                }

                IDataRecorder recorder = new CDataRecorderProxy();
                return recorder.LeaveFamily(personId);
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure in CBllFacadeForUI.leaveFamily. Message: {0}", ex.Message);
                return false;
            }
        }


        public Int32 AddPayment(String token, DateTime date, String category, String subCategory, Decimal spended)
        {
            try
            {
                log.Trace("Entered AddPayment");

                Int32 personId;
                if (IsTokenBad(token, out personId))
                {
                    log.Info("Token is invalid. Can't add Payment. Token: {0}", token);
                    return 0;
                }

                IDataRecorder recorder = new CDataRecorderProxy();
                return recorder.AddPayment(personId, date, category, subCategory, spended);
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure in CBllFacadeForUI.AddPayment (date = {1}, category = {2}, subCategory = {3}, sum = {4}). Message: {0}",
                          ex.Message, date, category, subCategory, spended);
                return 0;
            }
        }

        public Boolean EditPayment(String token, Int32 paymentId, DateTime newDate, String newCategoryTitle, String newSubCategoryTitle, Decimal newSum)
        {
            try
            {
                log.Trace("Entered EditPayment");

                Int32 personId;
                if (IsTokenBad(token, out personId))
                {
                    log.Info("Token is invalid. Can't edit Payment (id = {1}). Token: {0}", token, paymentId);
                    return false;
                }

                IDataRecorder recorder = new CDataRecorderProxy();
                return recorder.EditPayment(personId, paymentId, newDate, newCategoryTitle, newSubCategoryTitle, newSum);
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure in CBllFacadeForUI.EditPayment " +
                          "(paymentId = {1}, newDate = {2}, newCategory = {3}, newSubCategory = {4} newSum = {5}). Message: {0}",
                          ex.Message, paymentId, newDate, newCategoryTitle, newSubCategoryTitle, newSum);
                return false;
            }
        }

        public Boolean DeletePayment(String token, Int32 paymentId)
        {
            try
            {
                log.Trace("Entered DeletePayment");

                Int32 personId;
                if (IsTokenBad(token, out personId))
                {
                    log.Info("Token is invalid. Can't delete Payment (id = {1}). Token: {0}", token, paymentId);
                    return false;
                }

                IDataRecorder recorder = new CDataRecorderProxy();
                return recorder.DeletePayment(personId, paymentId);
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure in CBllFacadeForUI.DeletePayment (paymentId = {1}). Message: {0}", ex.Message, paymentId);
                return false;
            }
        }



        public Int32 AddSubCategory(String token, String categoryTitle, String subCategoryTitle, String subCategoryDescription)
        {
            try
            {
                log.Trace("Entered AddSubCategory");

                Int32 personId;
                if (IsTokenBad(token, out personId))
                {
                    log.Info("Token is invalid. Can't add Payment. Token: {0}", token);
                    return 0;
                }

                IDataRecorder recorder = new CDataRecorderProxy();
                return recorder.AddSubCategory(personId, categoryTitle, subCategoryTitle, subCategoryDescription);
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure in CBllFacadeForUI.AddSubCategory (categoryTitle = {1}, subCategoryTitle = {2}, subCategoryDescription = {3}). Message: {0}",
                          ex.Message, categoryTitle, subCategoryTitle, subCategoryDescription);
                return 0;
            }
        }

        public Boolean EditSubCategory(String token, Int32 subCategoryId, String newCategoryTitle, String newSubCategoryTitle, String newSubCategoryDescription)
        {
            try
            {
                log.Trace("Entered EditSubCategory");

                Int32 personId;
                if (IsTokenBad(token, out personId))
                {
                    log.Info("Token is invalid. Can't edit subCategory (id = {1}). Token: {0}", token, subCategoryId);
                    return false;
                }

                IDataRecorder recorder = new CDataRecorderProxy();
                return recorder.EditSubCategory(personId, subCategoryId, newCategoryTitle, newSubCategoryTitle, newSubCategoryDescription);
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure in CBllFacadeForUI.EditSubCategory " +
                          "(subCategoryId = {1}, newCategoryTitle = {2}, newSubCategoryTitle = {3}, newSubCategoryDescription = {4}). Message: {0}",
                          ex.Message, subCategoryId, newCategoryTitle, newSubCategoryTitle, newSubCategoryDescription);
                return false;
            }
        }

        public Boolean DeleteSubCategory(String token, Int32 subCategoryId)
        {
            try
            {
                log.Trace("Entered DeleteSubCategory");

                Int32 personId;
                if (IsTokenBad(token, out personId))
                {
                    log.Info("Token is invalid. Can't delete subCategory (id = {1}). Token: {0}", token, subCategoryId);
                    return false;
                }

                IDataRecorder recorder = new CDataRecorderProxy();
                return recorder.DeleteSubCategory(personId, subCategoryId);
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure in CBllFacadeForUI.DeleteSubCategory (subCategoryId = {1}). Message: {0}", ex.Message, subCategoryId);
                return false;
            }
        }



        public CLoginVerificationResult VerifyPassword(String username, String plainPassword)
        {
            try
            {
                log.Trace("Entered VerifyPassword");

                ILoginBllFacade loginProxy = new CLoginBllFacadeProxy();
                LoginService.CLoginVerificationResult loginServiceResponce = loginProxy.VerifyPassword(username, plainPassword);
                return CSLoginToBllConverter.ConverLoginVerification(loginServiceResponce);
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure in CBllFacadeForUI.VerifyPassword. Message: {0}", ex.Message);
                return null;
            }
        }

        public CRegistationResult RegisterPerson(String name, String username, String email, String plainPassword, String plainPasswordConfirmation)
        {
            try
            {
                log.Trace("Entered RegisterPerson");

                ILoginBllFacade loginProxy = new CLoginBllFacadeProxy();
                LoginService.CRegistationResult loginServiceResponce = loginProxy.RegisterPerson(name, username, email, plainPassword, plainPasswordConfirmation);
                return CSLoginToBllConverter.ConvertRegistationResult(loginServiceResponce);
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure in CBllFacadeForUI.RegisterPerson. Message: {0}", ex.Message);
                return null;
            }
        }

        public Boolean LogOut(String token)
        {
            try
            {
                log.Trace("Entered LogOut");

                ILoginBllFacade loginProxy = new CLoginBllFacadeProxy();
                return loginProxy.LogOut(token);
            }
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure in CBllFacadeForUI.LogOut. Message: {0}", ex.Message);
                return false;
            }
        }




        private Boolean IsTokenBad(String token, out Int32 personId)
        {
            log.Trace("Entered IsTokenBad");

            ILoginBllFacade loginProxy = new CLoginBllFacadeProxy();
            personId = loginProxy.CheckToken(token);

            return (personId == 0);
        }
    }
}
