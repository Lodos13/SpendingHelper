using System;
using System.Security;

using NLog;

using Utils;

namespace LoginService
{
    public class CLoginBllFacade : ILoginBllFacade
    {
        static private Logger log = LogManager.GetCurrentClassLogger();


        public int CheckToken(String token)
        {
            return CSLoginServis.CheckToken(token);
        }


        public CLoginVerificationResult VerifyPassword(String username, String plainPassword)
        {
            log.Trace($"Entered CLoginBllFacade.VerifyPassword. username = {username}, password = {plainPassword}");
            SecureString securePassword = plainPassword.ToSecureString();
            return CSLoginServis.VerifyPassword(username, securePassword);
        }
        public CRegistationResult RegisterPerson(String name, String username, String email, String plainPassword, String plainPasswordConfirmation)
        {
            SecureString securePassword = plainPassword.ToSecureString();
            SecureString securePasswordConfirmation = plainPasswordConfirmation.ToSecureString();
            return CSLoginServis.RegisterPerson(name, username, email, securePassword, securePasswordConfirmation);
        }
        public Boolean LogOut(String token)
        {
            return CSLoginServis.LogOut(token);
        }
    }
}
