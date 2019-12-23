using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

using NLog;

using BusinessLogicLayer;

namespace BlltoDalTransport
{
    public class CDataSupplierProxy : IDataSupplier
    {
        static private Logger log = LogManager.GetCurrentClassLogger();

        String _dataControllerAdress;
        HttpClient _client;

        public CDataSupplierProxy()
        {
            _dataControllerAdress = "http://localhost:9001/api/DataSupplier/";
            _client = new HttpClient();
        }

        public List<CCategoryType> GetCategoryTypes()
        {
            try
            {
                log.Trace("Entered GetCategoryTypes");

                String Uri = _dataControllerAdress + $"GetCategoryTypes";

                log.Trace("URI = {0}", Uri);

                HttpResponseMessage responce = _client.GetAsync(Uri).Result;

                log.Trace("Responce recived in GetCategoryTypes");

                return responce.Content.ReadAsAsync<List<CCategoryType>>().Result;
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in CDataSupplierProxy.GetCategoryTypes. Message: {0}", ex.Message);
                return null;
            }
        }

        public CPerson GetPersonById(Int32 personId)
        {
            try
            {
                log.Trace($"Entered GetPersonById personId = {personId}");

                String Uri = _dataControllerAdress + $"GetPersonById?personId={personId}";

                log.Trace("URI = {0}", Uri);

                HttpResponseMessage responce = _client.GetAsync(Uri).Result;
                CPerson res = responce.Content.ReadAsAsync<CPerson>().Result;

                log.Trace("Responce recived in GetPersonById");

                return res;
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in CDataSupplierProxy.GetPersonById (personId = {1}). Message: {0}", ex.Message, personId);
                return null;
            }
        }

        public CFamily GetFamilyByPersonId(Int32 personId)
        {
            try
            {
                log.Trace($"Entered GetFamilyByPersonId personId = {personId}");

                String Uri = _dataControllerAdress + $"GetFamilyByPersonId?personId={personId}";

                log.Trace("URI = {0}", Uri);

                HttpResponseMessage responce = _client.GetAsync(Uri).Result;

                log.Trace("Responce recived in GetFamilyByPersonId");

                return responce.Content.ReadAsAsync<CFamily>().Result;
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in CDataSupplierProxy.GetFamilyByPersonId (personId = {1}). Message: {0}", ex.Message, personId);
                return null;
            }
        }


        public CLoginData FindLoginByUsernameOrEmail(String username)
        {
            try
            {
                log.Trace($"Entered FindLoginByUsernameOrEmail. username = {username}");

                String Uri = _dataControllerAdress + $"FindLoginByUsernameOrEmail?username={username}";

                log.Trace($"URI = {Uri}");

                HttpResponseMessage responce = _client.GetAsync(Uri).Result;

                log.Trace($"Recived Respons in FindLoginByUsernameOrEmail");

                return responce.Content.ReadAsAsync<CLoginData>().Result;
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in CDataSupplierProxy.FindLoginByUsernameOrEmail (username = {1}). Message: {0}", ex.Message, username);
                return null;
            }
        }

        public Boolean IsUsernameExisted(String username)
        {
            try
            {
                log.Trace($"Entered IsUsernameExisted. username = {username}");

                String Uri = _dataControllerAdress + $"IsUsernameExisted?username={username}";

                log.Trace($"URI = {Uri}");

                HttpResponseMessage responce = _client.GetAsync(Uri).Result;
                Boolean res = responce.Content.ReadAsAsync<Boolean>().Result;

                log.Trace($"Recived Respons in IsUsernameExisted - {res}");

                return res;
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in CDataSupplierProxy.IsUsernameExisted (username = {1}). Message: {0}", ex.Message, username);
                return false;
            }
        }

        public Boolean IsEmailExisted(String email)
        {
            try
            {
                log.Trace($"Entered IsUsernameExisted. email = {email}");

                String Uri = _dataControllerAdress + $"IsEmailExisted?email={email}";

                log.Trace($"URI = {Uri}");

                HttpResponseMessage responce = _client.GetAsync(Uri).Result;
                Boolean res = responce.Content.ReadAsAsync<Boolean>().Result;

                log.Trace($"Recived Respons in IsEmailExisted - {res}");

                return res;
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in CDataSupplierProxy.IsEmailExisted (email = {1}). Message: {0}", ex.Message, email);
                return false;
            }
        }
    }
}
