using DAL.EF.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class ReminderDTO
    {
        public int Id { get; set; }
        public DateTime ReminderDate { get; set; }
        public bool IsSent { get; set; }
        public int ApplicationId { get; set; }

        public virtual ApplicationDTO JobApplication { get; set; }
    }
}
