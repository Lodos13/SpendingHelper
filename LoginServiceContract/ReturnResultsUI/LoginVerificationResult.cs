using System;

namespace LoginService
{
    public class CLoginVerificationResult
    {
        public Boolean IsVerified { get; }
        public String Error { get; }
        public String Token { get; }

        public CLoginVerificationResult(Boolean isVerified, String error, String token)
        {
            IsVerified = isVerified;
            Error = error;
            Token = token;
        }
    }
}
