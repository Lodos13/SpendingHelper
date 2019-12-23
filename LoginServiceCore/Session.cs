using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginService
{
    internal class CSession
    {
        internal int PersonID { get; }
        internal DateTime LastActivity { get; set; }

        internal CSession(int personID)
        {
            PersonID = personID;
            LastActivity = DateTime.Now;
        }

        internal void UpdateActivity()
        {
            LastActivity = DateTime.Now;
        }
    }
}
