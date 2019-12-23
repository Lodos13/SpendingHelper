using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessLogicLayer;

namespace BlltoDalTransport
{
    public interface IDataSupplier
    {
        List<CCategoryType> GetCategoryTypes();
        CPerson GetPersonById(Int32 id);
        CFamily GetFamilyByPersonId(Int32 personId);

        CLoginData FindLoginByUsernameOrEmail(String username);
        Boolean IsUsernameExisted(String username);
        Boolean IsEmailExisted(String email);
    }
}
