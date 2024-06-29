using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public System.DateTime OderDate { get; set; }
        public int UserId { get; set; }
    }
}