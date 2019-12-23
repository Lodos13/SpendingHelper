using System;
using System.Net.Http;
using System.Net.Http.Headers;

using NLog;

namespace LoginService
{
    public class CLoginBllFacadeProxy : ILoginBllFacade
    {
        static private Logger log = LogManager.GetCurrentClassLogger();

        String _dataControllerAdress;
        HttpClient _client;

        public CLoginBllFacadeProxy()
        {
            _dataControllerAdress = "http://localhost:9002/api/LoginBllFacade/";
            _client = new HttpClient();
        }



        public Int32 CheckToken(String token)
        {
            try
            {
                String Uri = _dataControllerAdress + $"CheckToken?token={token}";
                HttpResponseMessage responce = _client.GetAsync(Uri).Result;
                return responce.Content.ReadAsAsync<Int32>().Result;
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in CLoginBllFacadeProxy.CheckToken. Message: {0}", ex.Message);
                return 0;
            }
        }



        public CLoginVerificationResult VerifyPassword(String username, String plainPassword)
        {
            try
            {
                String Uri = _dataControllerAdress + $"VerifyPassword?username={username}" +
                                                                   $"&plainPassword={plainPassword}";
                HttpResponseMessage responce = _client.GetAsync(Uri).Result;
                return responce.Content.ReadAsAsync<CLoginVerificationResult>().Result;
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in CLoginBllFacadeProxy.VerifyPassword. Message: {0}", ex.Message);
                return null;
            }
        }

        public CRegistationResult RegisterPerson(String name, String username, String email, String plainPassword, String plainPasswordConfirmation)
        {
            try
            {
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
                log.Error("Some error occure in CLoginBllFacadeProxy.RegisterPerson. Message: {0}", ex.Message);
                return null;
            }
        }

        public Boolean LogOut(String token)
        {
            try
            {
                String Uri = _dataControllerAdress + $"LogOut?token={token}";
                HttpResponseMessage responce = _client.DeleteAsync(Uri).Result;
                return responce.Content.ReadAsAsync<Boolean>().Result;
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in CLoginBllFacadeProxy.LogOut. Message: {0}", ex.Message);
                return false;
            }
        }
    }
}
