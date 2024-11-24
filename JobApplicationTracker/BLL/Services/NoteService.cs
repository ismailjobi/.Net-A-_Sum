using AutoMapper;
using BLL.DTO;
using DAL;
using DAL.EF.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class NoteService
    {
        static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<JobApplication, ApplicationDTO>();
                cfg.CreateMap<NoteDTO, ApplicationNote>();
                cfg.CreateMap<ApplicationNote, NoteDTO>();
                cfg.CreateMap<ApplicationNote, NoteApplicationDTO>();
                cfg.CreateMap<NoteApplicationDTO, ApplicationNote>();
            });
            return new Mapper(config);
        }
        public static NoteApplicationDTO Create(NoteDTO obj,int applicationid)
        {
            var data = GetMapper().Map<ApplicationNote>(obj);
            data.ApplicationId = applicationid;
            var note = DataAccess.ApplicationNoteData().Create(data);
            return GetMapper().Map<NoteApplicationDTO>(note);   
        }
        public static NoteApplicationDTO Update(NoteDTO obj)
        {
            var data = GetMapper().Map<ApplicationNote>(obj);
            var note = DataAccess.ApplicationNoteData().Update(data);
            return GetMapper().Map<NoteApplicationDTO>(note);
        }
        public static List<NoteApplicationDTO> Get()
        {
            var data = DataAccess.ApplicationNoteData().Get();
            return GetMapper().Map<List<NoteApplicationDTO>>(data);
        }
        public static NoteApplicationDTO Get(int id)
        {
            var data = DataAccess.ApplicationNoteData().Get(id);
            return GetMapper().Map<NoteApplicationDTO>(data);
        }
        public static bool Delete(int id)
        {
            return DataAccess.ApplicationNoteData().Delete(id);
        }
    }
}
