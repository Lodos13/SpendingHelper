using System;
using System.Net.Http;
using System.Net.Http.Headers;

using NLog;

using Utils;

namespace BlltoDalTransport
{
    public class CDataRecorderProxy : IDataRecorder
    {
        static private Logger log = LogManager.GetCurrentClassLogger();

        String _dataControllerAdress;
        HttpClient _client;

        public CDataRecorderProxy()
        {
            _dataControllerAdress = "http://localhost:9001/api/DataRecorder/";
            _client = new HttpClient();
        }

        public void RegisterPerson(String name, String username, String email, String salt, String passwordHash)
        {
            try
            {
                log.Trace("Entered RegisterPerson");

                String Uri = _dataControllerAdress + $"RegisterPerson?name={name}" +
                                                                   $"&username={username}" +
                                                                   $"&email={email}" +
                                                                   $"&salt={salt}" +
                                                                   $"&passwordHash={passwordHash}";

                log.Trace("URI = {0}", Uri);

                HttpResponseMessage responce = _client.PostAsync(Uri, null).Result;

                log.Trace("Responce recived in RegisterPerson");
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in CDataRecorderProxy.RegisterPerson. Message: {0}", ex.Message);
                return;
            }
        }

        public Int32 AddPayment(Int32 personId, DateTime date, String category, String subCategory, Decimal sum)
        {
            try
            {
                log.Trace("Entered AddPayment");

                String dateStr = CSDateTimeHelper.ConvertDateToString(date);
                String Uri = _dataControllerAdress + $"AddPayment?personId={personId}" +
                                                               $"&dateStr={dateStr}" +
                                                               $"&category={category}" +
                                                               $"&subCategory={subCategory}" +
                                                               $"&spended={sum}";

                log.Trace("URI = {0}", Uri);

                HttpResponseMessage responce = _client.PostAsync(Uri, null).Result;
                Int32 res = responce.Content.ReadAsAsync<Int32>().Result;

                log.Trace($"Responce recived in AddPayment res = {res}");

                return res;
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in CDataRecorderProxy.AddPayment. Message: {0}", ex.Message);
                return 0;
            }
        }

        public Boolean EditPayment(Int32 personId, Int32 paymentId, DateTime newDate, String newCategoryTitle, String newSubCategoryTitle, Decimal newSum)
        {
            try
            {
                log.Trace("Entered EditPayment");

                String newDateStr = CSDateTimeHelper.ConvertDateToString(newDate);
                String Uri = _dataControllerAdress + $"EditPayment?personId={personId}" +
                                                                $"&paymentId={paymentId}" +
                                                                $"&newDateStr={newDateStr}" +
                                                                $"&newCategoryTitle={newCategoryTitle}" +
                                                                $"&newSubCategoryTitle={newSubCategoryTitle}" +
                                                                $"&newSum={newSum}";

                log.Trace("URI = {0}", Uri);

                HttpResponseMessage responce = _client.PutAsync(Uri, null).Result;
                Boolean res = responce.Content.ReadAsAsync<Boolean>().Result;

                log.Trace($"Responce recived in EditPayment res = {res}");

                return res;
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in CDataRecorderProxy.EditPayment. Message: {0}", ex.Message);
                return false;
            }
        }

        public Boolean DeletePayment(Int32 personId, Int32 paymentId)
        {
            try
            {
                log.Trace("Entered DeletePayment");

                String Uri = _dataControllerAdress + $"DeletePayment?personId={personId}" +
                                                                  $"&paymentId={paymentId}";

                log.Trace("URI = {0}", Uri);

                HttpResponseMessage responce = _client.DeleteAsync(Uri).Result;
                Boolean res = responce.Content.ReadAsAsync<Boolean>().Result;

                log.Trace($"Responce recived in EditPayment res = {res}");

                return res;
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in CDataRecorderProxy.DeletePayment. Message: {0}", ex.Message);
                return false;
            }
        }


        public Int32 AddSubCategory(Int32 personId, String categoryTitle, String subCategoryTitle, String subCategoryDescription)
        {
            try
            {
                log.Trace("Entered AddSubCategory");

                String Uri = _dataControllerAdress + $"AddSubCategory?personId={personId}" +
                                                               $"&categoryTitle={categoryTitle}" +
                                                               $"&subCategoryTitle={subCategoryTitle}" +
                                                               $"&subCategoryDescription={subCategoryDescription}";

                log.Trace("URI = {0}", Uri);

                HttpResponseMessage responce = _client.PostAsync(Uri, null).Result;
                Int32 res = responce.Content.ReadAsAsync<Int32>().Result;

                log.Trace($"Responce recived in AddSubCategory res = {res}");

                return res;
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in CDataRecorderProxy.AddSubCategory. Message: {0}", ex.Message);
                return 0;
            }
        }

        public Boolean EditSubCategory(Int32 personId, Int32 subCategoryId, String newCategoryTitle, String newSubCategoryTitle, String newSubCategoryDescription)
        {
            try
            {
                log.Trace("Entered EditSubCategory");
                String Uri = _dataControllerAdress + $"EditSubCategory?personId={personId}" +
                                                                    $"&subCategoryId={subCategoryId}" +
                                                                    $"&newCategoryTitle={newCategoryTitle}" +
                                                                    $"&newSubCategoryTitle={newSubCategoryTitle}" +
                                                                    $"&newSubCategoryDescription={newSubCategoryDescription}";
                log.Trace("URI = {0}", Uri);

                HttpResponseMessage responce = _client.PutAsync(Uri, null).Result;
                Boolean res = responce.Content.ReadAsAsync<Boolean>().Result;

                log.Trace($"Responce recived in EditPayment res = {res}");

                return res;
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in CDataRecorderProxy.EditSubCategory. Message: {0}", ex.Message);
                return false;
            }
        }

        public Boolean DeleteSubCategory(Int32 personId, Int32 subCategoryId)
        {
            try
            {
                log.Trace("Entered DeleteSubCategory");

                String Uri = _dataControllerAdress + $"DeleteSubCategory?personId={personId}" +
                                                                      $"&subCategoryId={subCategoryId}";

                log.Trace("URI = {0}", Uri);

                HttpResponseMessage responce = _client.DeleteAsync(Uri).Result;
                Boolean res = responce.Content.ReadAsAsync<Boolean>().Result;

                log.Trace($"Responce recived in EditPayment res = {res}");

                return res;
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in CDataRecorderProxy.DeleteSubCategory. Message: {0}", ex.Message);
                return false;
            }
        }


        public Boolean CreateFamily(Int32 personId, String familyName, Int32 budget)
        {
            try
            {
                log.Trace("Entered CreateFamily");

                String Uri = _dataControllerAdress + $"CreateFamily?personId={personId}" +
                                                                 $"&familyName={familyName}" +
                                                                 $"&budget={budget}";

                log.Trace("URI = {0}", Uri);

                HttpResponseMessage responce = _client.PostAsync(Uri, null).Result;
                Boolean res = responce.Content.ReadAsAsync<Boolean>().Result;

                log.Trace($"Responce recived in CreateFamily res = {res}");

                return res;
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in CDataRecorderProxy.CreateFamily. Message: {0}", ex.Message);
                return false;
            }
        }

        public Boolean JoinFamily(Int32 personId, Int32 familyId)
        {
            try
            {
                log.Trace("Entered JoinFamily");

                String Uri = _dataControllerAdress + $"JoinFamily?personId={personId}" +
                                                               $"&familyId={familyId}";

                log.Trace("URI = {0}", Uri);

                HttpResponseMessage responce = _client.PutAsync(Uri, null).Result;
                Boolean res = responce.Content.ReadAsAsync<Boolean>().Result;

                log.Trace($"Responce recived in CreateFamily res = {res}");

                return res;
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in CDataRecorderProxy.JoinFamily. Message: {0}", ex.Message);
                return false;
            }
        }

        public Boolean LeaveFamily(Int32 personId)
        {
            try
            {
                log.Trace("Entered LeaveFamily");

                String Uri = _dataControllerAdress + $"LeaveFamily?personId={personId}";

                log.Trace("URI = {0}", Uri);

                HttpResponseMessage responce = _client.PutAsync(Uri, null).Result;
                Boolean res = responce.Content.ReadAsAsync<Boolean>().Result;

                log.Trace($"Responce recived in LeaveFamily res = {res}");

                return res;
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in CDataRecorderProxy.LeaveFamily. Message: {0}", ex.Message);
                return false;
            }
        }
    }
}
