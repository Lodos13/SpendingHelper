using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NLog;

using BusinessLogicLayer;
using DataAccessLayer;

namespace BlltoDalTransport
{
    public class CDataSupplierDB : IDataSupplier
    {
        static private Logger log = LogManager.GetCurrentClassLogger();

        public List<CCategoryType> GetCategoryTypes()
        {
            ICategoryRepository categoryRepository = CSRepositoryFactory.GetCategoryRepository();
            ICollection<CCategoryDto> categories = categoryRepository.GetAllItems();

            List<CCategoryType> Types = new List<CCategoryType>();

            foreach(CCategoryDto cat in categories)
            {
                Types.Add(new CCategoryType(cat.CategoryID, cat.Title, cat.Description));
            }

            return Types;
        }

        public CPerson GetPersonById(int personId)
        {
            IPersonRepository personRepository = CSRepositoryFactory.GetPersonRepository();
            CPersonDto personDal = personRepository.FindItemByKeyFullLoad(personId);

            CDalToBllConverter converter = new CDalToBllConverter();
            return converter.ConvertPerson(personDal);
        }

        public CFamily GetFamilyByPersonId(int personId)
        {
            IFamilyRepository familyRep = CSRepositoryFactory.GetFamilyRepository();
            CFamilyDto familyDto = familyRep.FindByPersonIdFullLoad(personId);

            CDalToBllConverter converter = new CDalToBllConverter();
            return converter.ConvertFamily(familyDto);
        }


        public CLoginData FindLoginByUsernameOrEmail(String username)
        {
            ILoginRepository loginRepository = CSRepositoryFactory.GetLoginRepository();

            CLoginDataDto loginDto = loginRepository.FindLoginDataByUsername(username);

            if(loginDto == null)
                loginDto = loginRepository.FindLoginDataByEmail(username);

            if (loginDto == null)
                return null;

            CDalToBllConverter converter = new CDalToBllConverter();
            return converter.ConvertLogin(loginDto);
        }

        public bool IsUsernameExisted(string username)
        {
            ILoginRepository loginRepository = CSRepositoryFactory.GetLoginRepository();
            CLoginDataDto loginDto = loginRepository.FindLoginDataByUsername(username);
            return loginDto != null;
        }

        public bool IsEmailExisted(string email)
        {
            ILoginRepository loginRepository = CSRepositoryFactory.GetLoginRepository();
            CLoginDataDto loginDto = loginRepository.FindLoginDataByEmail(email);
            return loginDto != null;
        }
    }
}
