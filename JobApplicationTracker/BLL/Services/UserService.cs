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
    public class UserService
    {
        static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<UserDTO, User>();
            });
            return new Mapper(config);
        }
        public static UserDTO Create(UserDTO obj)
        {
            var data = GetMapper().Map<User>(obj);
            var user = DataAccess.UserData().Create(data);
            return GetMapper().Map<UserDTO>(user);
        }
        public static List<UserDTO> Get()
        {
            var data = DataAccess.UserData().Get();
            return GetMapper().Map<List<UserDTO>>(data);
        }
        public static UserDTO Get(int id)
        {
            var data = DataAccess.UserData().Get(id);
            return GetMapper().Map<UserDTO>(data);
        }
        public static UserDTO Update(UserDTO obj)
        {
            var data = GetMapper().Map<User>(obj);
            var user = DataAccess.UserData().Update(data);
            return GetMapper().Map<UserDTO>(user);
        }
        public static bool Delete(int id)
        {
            return DataAccess.UserData().Delete(id);
        }
    }
}
