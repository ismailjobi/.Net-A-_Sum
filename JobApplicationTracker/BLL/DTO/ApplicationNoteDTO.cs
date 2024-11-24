using DAL.EF.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class ApplicationNoteDTO : ApplicationDTO
    {
        public List<NoteDTO> ApplicationNotes { get; set; }

        public ApplicationNoteDTO()
        {
            ApplicationNotes = new List<NoteDTO>();
        }
    }
}
