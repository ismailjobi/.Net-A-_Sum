using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class ApplicationStatusDTO : ApplicationDTO
    {
        public List<StatusDTO> ApplicationStatusHistories { get; set; }

        public ApplicationStatusDTO()
        {
            ApplicationStatusHistories = new List<StatusDTO>();
        }
    }
}
