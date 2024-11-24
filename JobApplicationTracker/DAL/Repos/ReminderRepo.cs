using DAL.EF.TableModels;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class ReminderRepo : Repo, IRepo<Reminder, int, Reminder>,IReminder
    {
        public Reminder Create(Reminder obj)
        {
            obj.IsSent=false;
            Db.Reminders.Add(obj);
            Db.SaveChanges();
            return obj;
        }

        public List<Reminder> DeadlineReminder()
        {
            var deadline = Db.Reminders.Where(d=>d.ReminderDate.Equals(DateTime.Today) && !d.IsSent).ToList();
            foreach (var reminder in deadline)
            {
                reminder.IsSent=true;
            }
            Db.SaveChanges() ;
            return deadline;
        }

        public bool Delete(int id)
        {
            var exobj = Db.Reminders.Find(id);
            if (exobj == null)
            {
                return false;
            }
            Db.Reminders.Remove(exobj);
            Db.SaveChanges();
            return true;
        }

        public List<Reminder> Get()
        {
            return Db.Reminders.ToList();
        }

        public Reminder Get(int id)
        {
            return Db.Reminders.Find(id);
        }

        public Reminder Update(Reminder obj)
        {
            var exobj = Db.Reminders.Find(obj.Id);
            if (exobj!=null)
            {
                exobj.Id=exobj.Id;
                exobj.ReminderDate= obj.ReminderDate;
                exobj.ApplicationId=exobj.ApplicationId;
                exobj.IsSent=exobj.IsSent;
                Db.SaveChanges();
                return exobj;
            }
            return obj;
        }
    }
}
