//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class CPaymentDto
    {
        public int PaymentID { get; set; }
        public int SubCategoryID { get; set; }
        public System.DateTime Date { get; set; }
        public decimal Spended { get; set; }
    
        public virtual CSubCategoryDto SubCategory { get; set; }
    }
}
