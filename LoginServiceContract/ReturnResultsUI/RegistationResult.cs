using System;

namespace LoginService
{
    public class CRegistationResult
    {

        public Boolean IsEmailValidated { get; set; }
        public Boolean IsPasswordValidated { get; set; }
        public Boolean IsPasswordConfirmed { get; set; }
        public Boolean IsUsernameFree { get; set; }
        public Boolean IsEmailFree { get; set; }
        public String Token { get; set; }


        private Boolean _isRegistered;
        public Boolean IsRegistered
        {
            get => _isRegistered;
            set
            {
                if (value == false || !IsEmailValidated || !IsPasswordValidated || !IsPasswordConfirmed || !IsUsernameFree || !IsEmailFree)
                    _isRegistered = false;
                else
                    _isRegistered = true;
            }
        }
    }
}
