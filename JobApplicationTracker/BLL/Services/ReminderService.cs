using AutoMapper;
using BLL.DTO;
using DAL.EF.TableModels;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ReminderService
    {
        static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ReminderDTO, Reminder>();
                cfg.CreateMap<Reminder, ReminderDTO>();
                cfg.CreateMap<JobApplication, ApplicationDTO>();
            });
            return new Mapper(config);
        }
        public static ReminderDTO Create(ReminderDTO obj)
        {
            var data = GetMapper().Map<Reminder>(obj);
            var reminder = DataAccess.ReminderData().Create(data);
            return GetMapper().Map<ReminderDTO>(reminder);
        }
        public static ReminderDTO Update(ReminderDTO obj)
        {
            var data = GetMapper().Map<Reminder>(obj);
            var reminder = DataAccess.ReminderData().Update(data);
            return GetMapper().Map<ReminderDTO>(reminder);
        }
        public static List<ReminderDTO> Get()
        {
            var data = DataAccess.ReminderData().Get();
            return GetMapper().Map<List<ReminderDTO>>(data);
        }
        public static List<ReminderDTO> DeadlineReminder()
        {
            var data = DataAccess.DeadlineData().DeadlineReminder();
            return GetMapper().Map<List<ReminderDTO>>(data);
        }
        public static ReminderDTO Get(int id)
        {
            var data = DataAccess.ReminderData().Get(id);
            return GetMapper().Map<ReminderDTO>(data);
        }
        public static bool Delete(int id)
        {
            return DataAccess.ReminderData().Delete(id);
        }
    }
}
