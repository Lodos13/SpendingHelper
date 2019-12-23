using System;
using System.Collections.Generic;
using System.Security;

namespace BusinessLogicLayer
{
    public interface IBllFacadeForUI
    {
        CPerson GetPerson(String token);

        ICollection<CCategoryType> GetAllRegisterdCategories();

        CFamilyAccessResult GetFamily(String token);
        Boolean CreateFamily(String token, String familyName, Int32 budget);
        Boolean JoinFamily(String token, Int32 familyId);
        Boolean LeaveFamily(String token);


        Int32 AddPayment(String token, DateTime date, String category, String subCategory, Decimal spended);
        Boolean EditPayment(String token, Int32 paymentId, DateTime newDate, String newCategoryTitle, String newSubCategoryTitle, Decimal newSum);
        Boolean DeletePayment(String token, Int32 paymentId);


        Int32 AddSubCategory(String token, String categoryTitle, String subCategoryTitle, String subCategoryDescription);
        Boolean EditSubCategory(String token, Int32 subCategoryId, String newCategoryTitle, String newSubCategoryTitle, String newSubCategoryDescription);
        Boolean DeleteSubCategory(String token, Int32 subCategoryId);

        CLoginVerificationResult VerifyPassword(String username, String plainPassword);
        CRegistationResult RegisterPerson(String name, String username, String email, String plainPassword, String plainPasswordConfirmation);
        Boolean LogOut(String token);
    }
}