using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class CLoginData
    {
        public Int32 PersonID { get; set; }
        public String Email { get; set; }
        public String Username { get; set; }
        public String Hash { get; set; }
        public String Salt { get; set; }

        public CLoginData()
        { }

        public CLoginData(Int32 personID, String email, String username, String hash, String salt)
        {
            PersonID = personID;
            Email = email;
            Username = username;
            Hash = hash;
            Salt = salt;
        }
    }
}
