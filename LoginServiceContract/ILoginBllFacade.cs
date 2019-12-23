using System;
using System.Security;

namespace LoginService
{
    public interface ILoginBllFacade
    {
        /// <summary>
        /// Return 0 if tokken is not registered. Otherwise return user's PersonID 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Int32 CheckToken(String token);

        CLoginVerificationResult VerifyPassword(String username, String plainPassword);
        CRegistationResult RegisterPerson(String name, String username, String email, String plainPassword, String plainPasswordConfirmation);
        Boolean LogOut(String token);
    }
}
