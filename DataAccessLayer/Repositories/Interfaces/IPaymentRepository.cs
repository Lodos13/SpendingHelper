using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IPaymentRepository : IRepository<CPaymentDto,Int32>
    {
        Int32 AddPayment(Int32 personId, DateTime date, String category, String subCategory, Decimal spended);
        Boolean EditPayment(Int32 personId, Int32 paymentId, DateTime newDate, String newCategoryTitle, String newSubCategoryTitle, Decimal newSum);
        Boolean DeletePayment(Int32 personId, Int32 paymentId);
        ICollection<CPaymentDto> FindBySubCategoryId(Int32 subCategoryId);
    }
}
