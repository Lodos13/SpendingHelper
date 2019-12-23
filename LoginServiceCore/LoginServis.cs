using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Security;
using System.Text;
using System.Threading;

using NLog;

using BlltoDalTransport;
using BusinessLogicLayer;
using Utils;

namespace LoginService
{
    static class CSLoginServis
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        private static Dictionary<String, CSession> _tokenDictionary;
        private static CMyReadWriterLock _dictionaryRWLock;

        static CSLoginServis()
        {
            _tokenDictionary = new Dictionary<String, CSession>();
            _dictionaryRWLock = new CMyReadWriterLock();
        }

        internal static CLoginVerificationResult VerifyPassword(String username, SecureString securePassword)
        {
            log.Trace($"Entered CSLoginServis.VerifyPassword. username = {username}");
            CLoginData loginData = CheckName(username);
            if (loginData == null)
                return new CLoginVerificationResult(false, "User with this name isn't registered", String.Empty);

            bool isCorrectPassword = CheckPasswordHash(securePassword, loginData);
            if (!isCorrectPassword)
                return new CLoginVerificationResult(false, "Wrong securePassword", String.Empty);

            String token = RegisterSession(loginData);
            return new CLoginVerificationResult(true, String.Empty, token);
        }
        internal static CRegistationResult RegisterPerson(String name, String username, String email, SecureString securePassword, SecureString securePasswordConfirmation)
        {
            CRegistationResult result = new CRegistationResult();
            String password = securePassword.ConvertToUnsecureString();
            String passwordConfirmation = securePasswordConfirmation.ConvertToUnsecureString();

            result.IsEmailValidated = IsMailValid(email);
            result.IsPasswordValidated = IsPasswordValid(password);
            result.IsPasswordConfirmed = password.Equals(passwordConfirmation);
            if(!result.IsEmailValidated || !result.IsPasswordValidated || !result.IsPasswordConfirmed)
            {
                result.IsRegistered = false;
                return result;
            }

            CDataSupplierProxy dataSupplier = new CDataSupplierProxy();
            result.IsUsernameFree = !dataSupplier.IsUsernameExisted(username);
            result.IsEmailFree = !dataSupplier.IsEmailExisted(email);
            if(!result.IsUsernameFree || !result.IsEmailFree)
            {
                result.IsRegistered = false;
                return result;
            }

            CDataRecorderProxy dataRecorder = new CDataRecorderProxy();
            String salt = GenerateRandomString(10);
            String passwordHash = CalculateHash(SaltPassword(password,salt));
            try
            {
                dataRecorder.RegisterPerson(name, username, email, salt, passwordHash);
            }
            //TODO: Find what exception throw when added new entity with existed primary Key or unique field dublicate
            catch (Exception ex)
            {
                log.Error(ex, "Some error occure while trying to register person in CSLoginServis. Message: {0}", ex.Message);
                result.IsRegistered = false;
                return result;
            }

            CLoginVerificationResult verRes = VerifyPassword(username, securePassword);
            if (verRes.IsVerified)
            {
                result.Token = verRes.Token;
                result.IsRegistered = true;
            }
            else
                result.IsRegistered = false;

            return result;
        }
        internal static Boolean LogOut(String token)
        {
            Boolean result;

            _dictionaryRWLock.EnterWriteLock();
            try
            {
                result = _tokenDictionary.Remove(token);
            }
            finally
            {
                _dictionaryRWLock.ExitWriteLock();
            }

            return result;
        }

        /// <summary>
        /// Return 0 if tokken is not registered. Otherwise return user's PersonID 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        internal static int CheckToken(String token)
        {
            CSession session;

            _dictionaryRWLock.EnterReadLock();
            try
            {
                _tokenDictionary.TryGetValue(token, out session);
                session?.UpdateActivity();
            }
            finally
            {
                _dictionaryRWLock.ExitReadLock();
            }

            return session?.PersonID ?? 0;
        }



        private static CLoginData CheckName(String userName)
        {
            log.Trace($"Entered CSLoginServis.CheckName. username = {userName}");
            CDataSupplierProxy dataSupplier = new CDataSupplierProxy();
            return dataSupplier.FindLoginByUsernameOrEmail(userName);
        }
        private static Boolean CheckPasswordHash(SecureString securePassword, CLoginData loginData)
        {
            String password = securePassword.ConvertToUnsecureString();
            String saltedPassword = SaltPassword(password, loginData.Salt);
            return BCrypt.Net.BCrypt.Verify(saltedPassword, loginData.Hash);
        }
        private static String SaltPassword(String password, String salt)
        {
            const String serverSalt = "ae3rv4d2ke";
            return password + salt + serverSalt;
        }
        private static String CalculateHash(String saltedPassword)
        {
           return BCrypt.Net.BCrypt.HashPassword(saltedPassword);
        }
        private static String RegisterSession(CLoginData loginData)
        {
            String token;

            _dictionaryRWLock.EnterWriteLock();
            try
            {
                token = GenerateRandomString(10);
                CSession session = new CSession(loginData.PersonID);
                _tokenDictionary.Add(token, session);
            }
            finally
            {
                _dictionaryRWLock.ExitWriteLock();
            }

            return token;
        }
        private static String GenerateRandomString(int length)
        {
            Random random = new Random();
            StringBuilder sBuilder = new StringBuilder();
            char[] chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();
            for(int i = 0; i < length; i++)
            {
                sBuilder.Append(chars[random.Next(0,chars.Length)]);
            }
            return sBuilder.ToString();
        }
        private static Boolean IsMailValid(String email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
                return true;
            }
            catch (FormatException)
                { return false; }
        }
        private static Boolean IsPasswordValid(String password)
        {
            Boolean isCorrectLength = password.Length > 5;
            if (!isCorrectLength)
                return false;

            Boolean hasUpperLetter = false;
            Boolean hasLoverLetter = false;
            Boolean hasNumber = false;

            foreach(char c in password.ToCharArray())
            {
                if (!hasUpperLetter && Char.IsUpper(c))
                    hasUpperLetter = true;
                if (!hasLoverLetter && Char.IsLower(c))
                    hasLoverLetter = true;
                if (!hasNumber && Char.IsNumber(c))
                    hasNumber = true;

                if (hasUpperLetter && hasLoverLetter && hasNumber)
                    break;
            }

            return hasUpperLetter && hasLoverLetter && hasNumber;
        }




        
    }


}
