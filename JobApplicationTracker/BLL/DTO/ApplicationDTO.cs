using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class ApplicationDTO
    {
        public int Id { get; set; }
        public string Company { get; set; }
        public string Position { get; set; }
        public string Status { get; set; }
        public DateTime DateApplied { get; set; }
    }
}
