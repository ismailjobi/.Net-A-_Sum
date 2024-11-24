using DAL.EF.TableModels;
using DAL.Interfaces;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DataAccess
    {
        public static IRepo<User,int,User> UserData()
        {
            return new UserRepo();
        }
        public static IAuth AuthData()
        {
            return new UserRepo();
        }
        public static IRepo<JobApplication,int,JobApplication> ApplicationData()
        {
            return new ApplicationRepo();
        }
        public static IStatus<JobApplication,int> StatusData()
        {
            return new ApplicationRepo();
        }
        public static IRepo<ApplicationNote,int,ApplicationNote> ApplicationNoteData()
        {
            return new NoteRepo();
        }
        public static IRepo<Reminder,int,Reminder> ReminderData()
        {
            return new ReminderRepo();
        }
        public static IReminder DeadlineData()
        {
            return new ReminderRepo();
        }

    }
}
