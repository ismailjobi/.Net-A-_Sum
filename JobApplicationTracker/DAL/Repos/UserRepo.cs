using DAL.EF.TableModels;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class UserRepo : Repo, IRepo<User, int, User>, IAuth
    {
        public User Authenticate(string uname, string pass)
        {
            var user = Db.Users.SingleOrDefault(
                    u => u.Username.Equals(uname) &&
                    u.Password.Equals(pass)
                );
            return user;
        }

        public User Create(User obj)
        {
            var role = Db.Roles.SingleOrDefault(u => u.RoleName.Equals("User"));
            obj.RoleId=role.Id;
            obj.CreatedAt = DateTime.Now;
            Db.Users.Add(obj);
            Db.SaveChanges();
            return obj;
        }

        public bool Delete(int id)
        {
            var exobj = Db.Users.Find(id);
            if (exobj != null)
            {
                Db.Users.Remove(exobj);
            }
            return Db.SaveChanges() > 0;
        }

        public List<User> Get()
        {
            return Db.Users.ToList();
        }

        public User Get(int id)
        {
            return Db.Users.Find(id);
        }

        public User Update(User obj)
        {
            var user = Db.Users.Find(obj.Id);
            if (user != null) {
                user.Id = user.Id;
                user.Name = obj.Name;
                user.Username = user.Username;
                user.Password = user.Password;
                user.Email = obj.Email;
                user.CreatedAt = user.CreatedAt;
                user.UpdatedAt = DateTime.Now;
                user.RoleId = user.RoleId;

            }
            Db.SaveChanges();
            return user;
        }
    }
}
