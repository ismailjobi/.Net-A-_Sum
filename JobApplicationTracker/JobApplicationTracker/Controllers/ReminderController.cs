using BLL.DTO;
using BLL.Services;
using JobApplicationTracker.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JobApplicationTracker.Controllers
{
    [Logged]
    [RoutePrefix("api/reminder")]
    public class ReminderController : ApiController
    {
        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create(ReminderDTO obj)
        {
            try
            {
                var data = ReminderService.Create(obj);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Msg = "Contact Support", Api = "api/reminder/create" });
            }
        }
        [HttpGet]
        [Route("all")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                var data = ReminderService.Get();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Msg = "Contact Support", Api = "api/reminder/all" });
            }
        }
        [HttpGet]
        [Route("deadline")]
        public HttpResponseMessage DeadlineReminder()
        {
            try
            {
                var data = ReminderService.DeadlineReminder();
                
                if (data != null )
                {
                    var messages = data.Select(item => $"Today You Have An Application Deadline In {item.JobApplication.Company}.").ToList();
                    return Request.CreateResponse(HttpStatusCode.OK, new { Msg = messages });
                }

                return Request.CreateResponse(HttpStatusCode.OK, new { Msg = "Today You Don't Have Any Application Deadline" });
               
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Msg = "Contact Support", Api = "api/reminder/deadline" });
            }
        }
        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage Get(int id)
        {

            try
            {
                var data = ReminderService.Get(id);
                if (data != null)
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                else return Request.CreateResponse(HttpStatusCode.OK, new { Msg = "Reminder not Found" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Msg = "Contact Support", Api = "api/reminder/" + id });
            }

        }
        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(ReminderDTO obj)
        {
            try
            {
                var data = ReminderService.Update(obj);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Msg = "Contact Support", Api = "api/reminder/update" });
            }
        }
        [HttpGet]
        [Route("delete/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var data = ReminderService.Delete(id);
                if(data) 
                    return Request.CreateResponse(HttpStatusCode.OK,data);
                else return Request.CreateResponse(HttpStatusCode.OK, new { Msg = "Reminder Unable To Delete" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Msg = "Contact Support", Api = "api/reminder/delete" + id });
            }
        }
    }
}
