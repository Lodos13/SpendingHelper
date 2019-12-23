using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccessLayer;

namespace BlltoDalTransport
{
    public class CDataRecorderDB : IDataRecorder
    {
        public void RegisterPerson(String name, String username, String email, String salt, String passwordHash)
        {
            IPersonRepository personRepository = CSRepositoryFactory.GetPersonRepository();

            CLoginDataDto loginDto = new CLoginDataDto();
            loginDto.Username = username;
            loginDto.email = email;
            loginDto.Salt = salt;
            loginDto.PasswordHash = passwordHash;

            CPersonDto personDto = new CPersonDto();
            personDto.Name = name;
            personDto.LoginData = loginDto;

            personRepository.AddItem(personDto);
        }

        public Int32 AddPayment(Int32 personId, DateTime date, String category, String subCategory, Decimal spended)
        {
            IPaymentRepository paymentRep = CSRepositoryFactory.GetPaymentRepository();
            return paymentRep.AddPayment(personId, date, category, subCategory, spended);
        }

        public Boolean EditPayment(Int32 personId, Int32 paymentId, DateTime newDate, String newCategoryTitle, String newSubCategoryTitle, Decimal newSum)
        {
            IPaymentRepository paymentRep = CSRepositoryFactory.GetPaymentRepository();
            return paymentRep.EditPayment(personId, paymentId, newDate, newCategoryTitle, newSubCategoryTitle, newSum);
        }

        public Boolean DeletePayment(Int32 personId, Int32 paymentId)
        {
            IPaymentRepository paymentRep = CSRepositoryFactory.GetPaymentRepository();
            return paymentRep.DeletePayment(personId, paymentId);
        }


        public Int32 AddSubCategory(Int32 personId, String categoryTitle, String subCategoryTitle, String subCategoryDescription)
        {
            ISubCategoryRepository subCatRep = CSRepositoryFactory.getSubCategoryRepository();
            return subCatRep.TryAddSubCategory(personId, categoryTitle, subCategoryTitle, subCategoryDescription);
        }

        public Boolean EditSubCategory(Int32 personId, Int32 subCategoryId, String newCategoryTitle, String newSubCategoryTitle, String newSubCategoryDescription)
        {
            ISubCategoryRepository subCatRep = CSRepositoryFactory.getSubCategoryRepository();
            return subCatRep.TyrEditSubCategory(personId, subCategoryId, newCategoryTitle, newSubCategoryTitle, newSubCategoryDescription);
        }

        public Boolean DeleteSubCategory(Int32 personId, Int32 subCategoryId)
        {
            ISubCategoryRepository subCatRep = CSRepositoryFactory.getSubCategoryRepository();
            return subCatRep.TryDeleteSubCategory(personId, subCategoryId);
        }


        public Boolean CreateFamily(Int32 personId, String familyName, Int32 budget)
        {
            IFamilyRepository familyRep = CSRepositoryFactory.GetFamilyRepository();
            return familyRep.CreateFamily(personId, familyName, budget);
        }

        public Boolean JoinFamily(Int32 personId, Int32 familyId)
        {
            IFamilyRepository familyRep = CSRepositoryFactory.GetFamilyRepository();
            return familyRep.JoinFamily(personId, familyId);
        }

        public Boolean LeaveFamily(Int32 personId)
        {
            IFamilyRepository familyRep = CSRepositoryFactory.GetFamilyRepository();
            return familyRep.LeaveFamily(personId);
        }
    }
}
