//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PMS.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderProduct
    {
        public int Id { get; set; }
        public int PId { get; set; }
        public int OId { get; set; }
        public int Qty { get; set; }
        public decimal UnitPrice { get; set; }
    
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
