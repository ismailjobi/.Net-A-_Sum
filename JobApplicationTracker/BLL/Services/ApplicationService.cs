using BLL.DTO;
using DAL.EF.TableModels;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using static System.Net.Mime.MediaTypeNames;

namespace BLL.Services
{
    public class ApplicationService
    {
        static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<JobApplication, ApplicationNoteDTO>();
                cfg.CreateMap<ApplicationNoteDTO, JobApplication>();
                cfg.CreateMap<JobApplication, ApplicationDTO>();
                cfg.CreateMap<ApplicationDTO, JobApplication>();
                cfg.CreateMap<NoteDTO, ApplicationNote>();
                cfg.CreateMap<ApplicationNote, NoteDTO>();
                cfg.CreateMap<JobApplication, ApplicationStatusDTO>();
                cfg.CreateMap<ApplicationStatusDTO, JobApplication>();
                cfg.CreateMap<ApplicationStatusHistory, StatusDTO>();
                cfg.CreateMap<StatusDTO, ApplicationStatusHistory>();
            });
            return new Mapper(config);
        }
        public static ApplicationNoteDTO Create(ApplicationNoteDTO obj,int userId)
        {
            var data = GetMapper().Map<JobApplication>(obj);
            data.UserId = userId;
            var application = DataAccess.ApplicationData().Create(data);
            return GetMapper().Map<ApplicationNoteDTO>(application);
        }
        public static ApplicationDTO Update(ApplicationDTO obj)
        {
            var data = GetMapper().Map<JobApplication>(obj);
            var application = DataAccess.ApplicationData().Update(data);
            return GetMapper().Map<ApplicationDTO>(application);
        }
        public static List<ApplicationDTO> Get()
        {
            var data = DataAccess.ApplicationData().Get();
            return GetMapper().Map<List<ApplicationDTO>>(data);
        }
        public static List<ApplicationDTO> FilterApplication(string status)
        {
            var data = DataAccess.StatusData().FilterApplication(status);
            return GetMapper().Map<List<ApplicationDTO>>(data);
        }
        public static ApplicationStatusDTO StatusTrack(int id)
        {
            var data = DataAccess.StatusData().StatusTrack(id);
            return GetMapper().Map<ApplicationStatusDTO>(data);
        }
        public static ApplicationDTO Get(int id)
        {
            var data = DataAccess.ApplicationData().Get(id);
            return GetMapper().Map<ApplicationDTO>(data);
        }
        public static bool Delete(int id)
        {
            return DataAccess.ApplicationData().Delete(id);
        }
    }
}
