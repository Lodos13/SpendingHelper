using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlltoDalTransport
{
    public interface IDataRecorder
    {
        void RegisterPerson(String name, String username, String email, String salt, String passwordHash);

        Int32 AddPayment(Int32 personId, DateTime date, String category, String subCategory, Decimal spended);
        Boolean EditPayment(Int32 personId, Int32 paymentId, DateTime newDate, String newCategoryTitle, String newSubCategoryTitle, Decimal newSum);
        Boolean DeletePayment(Int32 personId, Int32 paymentId);

        Int32 AddSubCategory(Int32 personId, String categoryTitle, String subCategoryTitle, String subCategoryDescription);
        Boolean EditSubCategory(Int32 personId, Int32 subCategoryId, String newCategoryTitle, String newSubCategoryTitle, String newSubCategoryDescription);
        Boolean DeleteSubCategory(Int32 personId, Int32 subCategoryId);

        Boolean CreateFamily(Int32 personId, String familyName, Int32 budget);
        Boolean JoinFamily(Int32 personId, Int32 familyId);
        Boolean LeaveFamily(Int32 personId);
    }
}
