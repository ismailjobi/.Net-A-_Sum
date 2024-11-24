using AutoMapper;
using BLL.DTO;
using DAL;
using DAL.EF.TableModels;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AuthService
    {
        static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<UserDTO, User>();
            });
            return new Mapper(config);
        }

        public static UserDTO Authenticate(string UserName, string Password)
        {
            var data = DataAccess.AuthData().Authenticate(UserName, Password);
            
            return GetMapper().Map<UserDTO>(data);
            
        }
    }
}
