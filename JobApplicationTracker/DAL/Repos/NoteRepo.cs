using DAL.EF.TableModels;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class NoteRepo : Repo, IRepo<ApplicationNote, int, ApplicationNote>
    {
        public ApplicationNote Create(ApplicationNote obj)
        {
            obj.CreatedAt = DateTime.Now;
            Db.ApplicationNotes.Add(obj);
            Db.SaveChanges();
            var note = Db.ApplicationNotes
                               .Include("JobApplication")
                               .FirstOrDefault(n => n.Id == obj.Id);

            return note;
        }

        public bool Delete(int id)
        {
            var exobj = Db.ApplicationNotes.Find(id);
            if (exobj != null) { 
                Db.ApplicationNotes.Remove(exobj);
                Db.SaveChanges();
                return true;
            }
            return false;
        }

        public List<ApplicationNote> Get()
        {
            return Db.ApplicationNotes.ToList();
        }

        public ApplicationNote Get(int id)
        {
            return Db.ApplicationNotes.Find(id);
        }

        public ApplicationNote Update(ApplicationNote obj)
        {
            var exobj = Db.ApplicationNotes.Find(obj.Id);
            if (exobj != null)
            {
                exobj.Id = exobj.Id;
                exobj.Note = obj.Note;
                exobj.ApplicationId = exobj.ApplicationId;
                exobj.CreatedAt = exobj.CreatedAt;
                exobj.UpdatedAt = DateTime.Now;

                Db.SaveChanges();
                return exobj;
            }
            return obj;
        }
    }
}
