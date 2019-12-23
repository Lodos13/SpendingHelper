using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

using NLog;

using LoginService;

namespace LoginHost
{
    public class LoginBllFacadeController : ApiController, ILoginBllFacade
    {
        static private Logger log = LogManager.GetCurrentClassLogger();

        ILoginBllFacade _facadeForBll = new CLoginBllFacade();

        [HttpGet]
        public int CheckToken(String token)
        {
            try
            {
                return _facadeForBll.CheckToken(token);
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in LoginBllFacadeController.CheckToken. Message: {0}", ex.Message);
                return 0;
            }
        }



        [HttpGet]
        public CLoginVerificationResult VerifyPassword(String username, String plainPassword)
        {
            try
            {
                log.Trace($"Entered VerifyPassword. Username = {username}, plainPassword = {plainPassword}");
                return _facadeForBll.VerifyPassword(username, plainPassword);
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in LoginBllFacadeController.VerifyPassword. Message: {0}", ex.Message);
                return null;
            }
        }

        [HttpPost]
        public CRegistationResult RegisterPerson(String name, String username, String email, String plainPassword, String plainPasswordConfirmation)
        {
            try
            {
                return _facadeForBll.RegisterPerson(name, username, email, plainPassword, plainPasswordConfirmation);
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in LoginBllFacadeController.RegisterPerson. Message: {0}", ex.Message);
                return null;
            }
        }

        [HttpDelete]
        public bool LogOut(String token)
        {
            try
            {
                return _facadeForBll.LogOut(token);
            }
            catch (Exception ex)
            {
                log.Error("Some error occure in LoginBllFacadeController.LogOut. Message: {0}", ex.Message);
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
