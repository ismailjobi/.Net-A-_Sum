using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class NoteApplicationDTO 
    {
        public int Id { get; set; }
        public string Note { get; set; }
        public virtual ApplicationDTO JobApplication { get; set; }
    }
}
