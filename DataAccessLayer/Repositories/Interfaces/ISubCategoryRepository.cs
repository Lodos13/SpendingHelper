using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface ISubCategoryRepository : IRepository<CSubCategoryDto, Int32>
    {
        ICollection<CSubCategoryDto> FindByPersonId(Int32 personId);

        Int32 TryAddSubCategory(Int32 personId, String categoryTitle, String subCategoryTitle, String subCategoryDescription);
        Boolean TyrEditSubCategory(Int32 personId, Int32 subCategoryId, String newCategoryTitle, String newSubCategoryTitle, String newSubCategoryDescription);
        Boolean TryDeleteSubCategory(Int32 personId, Int32 subCategoryId);
    }
}
