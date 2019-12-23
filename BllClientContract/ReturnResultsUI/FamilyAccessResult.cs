using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class CFamilyAccessResult
    {
        public Boolean IsTokenExpired { get; }
        public Boolean IsNotExisted { get; }
        public Boolean IsAccessDenied { get; }
        public CFamily Family { get; }

        public CFamilyAccessResult(Boolean isTokenExpired, Boolean isNotExisted, Boolean isAccessDenied, CFamily family)
        {
            IsTokenExpired = isTokenExpired;
            IsNotExisted = isNotExisted;
            IsAccessDenied = isAccessDenied;
            Family = family;
        }
    }
}
