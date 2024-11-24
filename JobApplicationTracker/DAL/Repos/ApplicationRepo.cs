using DAL.EF.TableModels;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class ApplicationRepo : Repo, IRepo<JobApplication, int, JobApplication>, IStatus<JobApplication,int>
    {
        public JobApplication Create(JobApplication obj)
        {
            obj.Status = "Applied"; 
            Db.JobApplications.Add(obj);

            foreach (var note in obj.ApplicationNotes)
            {
                note.ApplicationId = obj.Id;  
                note.CreatedAt = DateTime.Now;
                Db.ApplicationNotes.Add(note);
            }

            Db.SaveChanges();
            return obj;
        }

        public bool Delete(int id)
        {
            var exobj = Db.JobApplications.Find(id);
            if (exobj == null)
            {
                return false; 
            }
            
            foreach (var note in exobj.ApplicationNotes.ToList())
            {
                Db.ApplicationNotes.Remove(note); 
            }
            Db.JobApplications.Remove(exobj);
            Db.SaveChanges();
            return true; 
        }

        public List<JobApplication> FilterApplication(string stauts)
        {
            var applications = Db.JobApplications
                          .Where(a => a.Status.Equals(stauts))
                          .ToList();

            return applications;
        }

        public List<JobApplication> Get()
        {
            return Db.JobApplications.ToList();
        }

        public JobApplication Get(int id)
        {
            return Db.JobApplications.Find(id);
        }

        public JobApplication StatusTrack(int id)
        {
            return Get(id);
        }

        public JobApplication Update(JobApplication obj)
        {
            var data = Db.JobApplications.Find(obj.Id);

            if (data != null)
            {
                // Update basic fields
                data.Company = obj.Company;
                data.Position = obj.Position;
                data.DateApplied = obj.DateApplied;

                // Check if status has changed and update history
                if (!data.Status.Equals(obj.Status))
                {
                    var history = new ApplicationStatusHistory
                    {
                        Status = data.Status,
                        ChangedAt = DateTime.Now,
                        ApplicationId = data.Id
                    };

                    Db.ApplicationStatusHistories.Add(history);
                    data.Status = obj.Status;
                }
                
                Db.SaveChanges();
            }
            var updateddata = Db.JobApplications.Find(data.Id);
            return updateddata;
        }
    }
}
