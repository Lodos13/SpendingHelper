using System;

namespace BusinessLogicLayer
{
    public class CRegistationResult
    {
        public Boolean IsRegistered { get; set; }
        public Boolean IsEmailValidated { get; set; }
        public Boolean IsPasswordValidated { get; set; }
        public Boolean IsPasswordConfirmed { get; set; }
        public Boolean IsUsernameFree { get; set; }
        public Boolean IsEmailFree { get; set; }
        public String Token { get; set; }

    }
}
