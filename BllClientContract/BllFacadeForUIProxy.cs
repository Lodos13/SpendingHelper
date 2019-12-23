using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

using NLog;

using Utils;

namespace BusinessLogicLayer
{
    public class CBllFacadeForUIProxy : IBllFacadeForUI
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        String _dataControllerAdress;
        HttpClient _client;


        public CBllFacadeForUIProxy()
        {
            _dataControllerAdress = "http://localhost:9003/api/BllFacadeForUI/";
            _client = new HttpClient();
        }


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

                String Uri = _dataControllerAdress + $"GetPerson?token={token}";
                HttpResponseMessage responce = _client.GetAsync(Uri).Result;
                return responce.Content.ReadAsAsync<CPerson>().Result;
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in CBllFacadeForUIProxy.GetPerson. Message: {0}", ex.Message);
                return null;
            }
        }


        public ICollection<CCategoryType> GetAllRegisterdCategories()
        {
            try
            {
                log.Trace("Entered GetAllRegisterdCategories");

                String Uri = _dataControllerAdress + $"GetAllRegisterdCategories";
                HttpResponseMessage responce = _client.GetAsync(Uri).Result;
                return responce.Content.ReadAsAsync<ICollection<CCategoryType>>().Result;
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in CBllFacadeForUIProxy.GetAllRegisterdCategories. Message: {0}", ex.Message);
                return null;
            }
        }


        public CFamilyAccessResult GetFamily(String token)
        {
            try
            {
                log.Trace("Entered GetFamily");

                String Uri = _dataControllerAdress + $"GetFamily?token={token}";
                HttpResponseMessage responce = _client.GetAsync(Uri).Result;
                return responce.Content.ReadAsAsync<CFamilyAccessResult>().Result;
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in CBllFacadeForUIProxy.GetFamily. Message: {0}", ex.Message);
                return null;
            }
        }

        public Boolean CreateFamily(String token, String familyName, Int32 budget)
        {
            try
            {
                log.Trace("Entered CreateFamily");

                String Uri = _dataControllerAdress + $"CreateFamily?token={token}" +
                                                                 $"&familyName={familyName}" +
                                                                 $"&budget={budget}";
                HttpResponseMessage responce = _client.PostAsync(Uri, null).Result;
                return responce.Content.ReadAsAsync<Boolean>().Result;
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in CBllFacadeForUIProxy.CreateFamily. Message: {0}", ex.Message);
                return false;
            }
        }

        public Boolean JoinFamily(String token, Int32 familyId)
        {
            try
            {
                log.Trace("Entered JoinFamily");

                String Uri = _dataControllerAdress + $"JoinFamily?token={token}" +
                                                               $"&familyId={familyId}";
                HttpResponseMessage responce = _client.PutAsync(Uri, null).Result;
                return responce.Content.ReadAsAsync<Boolean>().Result;
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in CBllFacadeForUIProxy.JoinFamily. Message: {0}", ex.Message);
                return false;
            }
        }

        public Boolean LeaveFamily(String token)
        {
            try
            {
                log.Trace("Entered LeaveFamily");

                String Uri = _dataControllerAdress + $"LeaveFamily?token={token}";
                HttpResponseMessage responce = _client.PutAsync(Uri, null).Result;
                return responce.Content.ReadAsAsync<Boolean>().Result;
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in CBllFacadeForUIProxy.LeaveFamily. Message: {0}", ex.Message);
                return false;
            }
        }



        public Int32 AddPayment(String token, DateTime date, String category, String subCategory, Decimal spended)
        {
            try
            {
                log.Trace("Entered AddPayment");

                String dateStr = CSDateTimeHelper.ConvertDateToString(date);
                String Uri = _dataControllerAdress + $"AddPayment?token={token}" +
                                                               $"&dateStr={dateStr}" +
                                                               $"&category={category}" +
                                                               $"&subCategory={subCategory}" +
                                                               $"&spended={spended}";
                HttpResponseMessage responce = _client.PostAsync(Uri, null).Result;
                return responce.Content.ReadAsAsync<Int32>().Result;
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in CBllFacadeForUIProxy.AddPayment. Message: {0}", ex.Message);
                return 0;
            }
        }

        public Boolean EditPayment(String token, Int32 paymentId, DateTime newDate, String newCategoryTitle, String newSubCategoryTitle, Decimal newSum)
        {
            try
            {
                log.Trace("Entered EditPayment");

                String newDateStr = CSDateTimeHelper.ConvertDateToString(newDate);
                String Uri = _dataControllerAdress + $"EditPayment?token={token}" +
                                                                $"&paymentId={paymentId}" +
                                                                $"&newDateStr={newDateStr}" +
                                                                $"&newCategoryTitle={newCategoryTitle}" +
                                                                $"&newSubCategoryTitle={newSubCategoryTitle}" +
                                                                $"&newSum={newSum}";
                HttpResponseMessage responce = _client.PutAsync(Uri, null).Result;
                return responce.Content.ReadAsAsync<Boolean>().Result;
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in CBllFacadeForUIProxy.EditPayment. Message: {0}", ex.Message);
                return false;
            }
        }

        public Boolean DeletePayment(String token, Int32 paymentId)
        {
            try
            {
                log.Trace("Entered DeletePayment");

                String Uri = _dataControllerAdress + $"DeletePayment?token={token}" +
                                                                  $"&paymentId={paymentId}";
                HttpResponseMessage responce = _client.DeleteAsync(Uri).Result;
                return responce.Content.ReadAsAsync<Boolean>().Result;
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in CBllFacadeForUIProxy.DeletePayment. Message: {0}", ex.Message);
                return false;
            }
        }



        public Int32 AddSubCategory(String token, String categoryTitle, String subCategoryTitle, String subCategoryDescription)
        {
            try
            {
                log.Trace("Entered AddSubCategory");

                String Uri = _dataControllerAdress + $"AddSubCategory?token={token}" +
                                                               $"&categoryTitle={categoryTitle}" +
                                                               $"&subCategoryTitle={subCategoryTitle}" +
                                                               $"&subCategoryDescription={subCategoryDescription}";

                HttpResponseMessage responce = _client.PostAsync(Uri, null).Result;
                return responce.Content.ReadAsAsync<Int32>().Result;
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in CBllFacadeForUIProxy.AddSubCategory. Message: {0}", ex.Message);
                return 0;
            }
        }

        public Boolean EditSubCategory(String token, Int32 subCategoryId, String newCategoryTitle, String newSubCategoryTitle, String newSubCategoryDescription)
        {
            try
            {
                log.Trace("Entered EditSubCategory");
                String Uri = _dataControllerAdress + $"EditSubCategory?token={token}" +
                                                                    $"&subCategoryId={subCategoryId}" +
                                                                    $"&newCategoryTitle={newCategoryTitle}" +
                                                                    $"&newSubCategoryTitle={newSubCategoryTitle}" +
                                                                    $"&newSubCategoryDescription={newSubCategoryDescription}";

                HttpResponseMessage responce = _client.PutAsync(Uri, null).Result;
                return responce.Content.ReadAsAsync<Boolean>().Result;
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in CBllFacadeForUIProxy.EditSubCategory. Message: {0}", ex.Message);
                return false;
            }
        }

        public Boolean DeleteSubCategory(String token, Int32 subCategoryId)
        {
            try
            {
                log.Trace("Entered DeleteSubCategory");

                String Uri = _dataControllerAdress + $"DeleteSubCategory?token={token}" +
                                                                      $"&subCategoryId={subCategoryId}";
                HttpResponseMessage responce = _client.DeleteAsync(Uri).Result;
                return responce.Content.ReadAsAsync<Boolean>().Result;
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in CBllFacadeForUIProxy.DeleteSubCategory. Message: {0}", ex.Message);
                return false;
            }
        }


        public CLoginVerificationResult VerifyPassword(String username, String plainPassword)
        {
            try
            {
                log.Trace("Entered VerifyPassword");

                String Uri = _dataControllerAdress + $"VerifyPassword?username={username}" +
                                                                   $"&plainPassword={plainPassword}";
                HttpResponseMessage responce = _client.GetAsync(Uri).Result;
                return responce.Content.ReadAsAsync<CLoginVerificationResult>().Result;
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in CBllFacadeForUIProxy.VerifyPassword. Message: {0}", ex.Message);
                return null;
            }
        }

        public CRegistationResult RegisterPerson(String name, String username, String email, String plainPassword, String plainPasswordConfirmation)
        {
            try
            {
                log.Trace("Entered RegisterPerson");

                String Uri = _dataControllerAdress + $"RegisterPerson?name={name}" +
                                                                   $"&username={username}" +
                                                                   $"&email={email}" +
                                                                   $"&plainPassword={plainPassword}" +
                                                                   $"&plainPasswordConfirmation={plainPasswordConfirmation}";
                HttpResponseMessage responce = _client.PostAsync(Uri, null).Result;
                return responce.Content.ReadAsAsync<CRegistationResult>().Result;
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in CBllFacadeForUIProxy.RegisterPerson. Message: {0}", ex.Message);
                return null;
            }
        }

        public Boolean LogOut(String token)
        {
            try
            {
                log.Trace("Entered LogOut");

                String Uri = _dataControllerAdress + $"LogOut?token={token}";
                HttpResponseMessage responce = _client.DeleteAsync(Uri).Result;
                return responce.Content.ReadAsAsync<Boolean>().Result;
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in CBllFacadeForUIProxy.LogOut. Message: {0}", ex.Message);
                return false;
            }
        }
    }
}
