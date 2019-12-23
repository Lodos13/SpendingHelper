
namespace BusinessLogicLayer
{
    public static class CSLoginToBllConverter
    {
        public static BusinessLogicLayer.CLoginVerificationResult ConverLoginVerification(LoginService.CLoginVerificationResult source)
        {
            return new BusinessLogicLayer.CLoginVerificationResult(source.IsVerified, source.Error, source.Token);
        }

        public static BusinessLogicLayer.CRegistationResult ConvertRegistationResult(LoginService.CRegistationResult source)
        {
            BusinessLogicLayer.CRegistationResult result = new BusinessLogicLayer.CRegistationResult();

            result.IsEmailValidated = source.IsEmailValidated;
            result.IsPasswordValidated = source.IsPasswordValidated;
            result.IsPasswordConfirmed = source.IsPasswordConfirmed;
            result.IsUsernameFree = source.IsUsernameFree;
            result.IsEmailFree = source.IsEmailFree;
            result.Token = source.Token;
            result.IsRegistered = source.IsRegistered;

            return result;
        }
    }
}
