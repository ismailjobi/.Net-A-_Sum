using BLL.DTO;
using BLL.Services;
using JobApplicationTracker.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace JobApplicationTracker.Controllers
{
    [Logged]
    [RoutePrefix("api/jobapplication")]
    public class ApplicationController : ApiController
    {
        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create(ApplicationNoteDTO obj)
        {
            try
            {
                var user = HttpContext.Current.Session["user"] as UserDTO;
                var data = ApplicationService.Create(obj,user.Id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Msg = "Contact Support", Api = "api/jobapplication/create" });
            }
        }
        [HttpGet]
        [Route("all")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                var data = ApplicationService.Get();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Msg = "Contact Support", Api = "api/jobapplication/all" });
            }
        }
        [HttpGet]
        [Route("filter")]
        public HttpResponseMessage FilterApplication(string status)
        {
            try
            {
                var data = ApplicationService.FilterApplication(status);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Msg = "Contact Support", Api = "api/jobapplication/FilterApplication" });
            }
        }
        [HttpGet]
        [Route("statustrack/{id}")]
        public HttpResponseMessage StatusTrack(int id)
        {
            try
            {
                var data = ApplicationService.StatusTrack(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Msg = "Contact Support", Api = "api/jobapplication/statustrack" + id });
            }
        }
        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage Get(int id)
        {

            try
            {
                var data = ApplicationService.Get(id);
                if (data != null)
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                else return Request.CreateResponse(HttpStatusCode.OK, new { Msg = "Application not Found" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Msg = "Contact Support", Api = "api/jobapplication/" + id });
            }

        }
        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(ApplicationDTO obj)
        {
            try
            {
                var data = ApplicationService.Update(obj);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Msg = "Contact Support", Api = "api/jobapplication/update" });
            }
        }
        [HttpGet]
        [Route("delete/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var data = ApplicationService.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Msg = "Contact Support", Api = "api/jobapplication/delete" + id });
            }
        }
        [HttpGet]
        [Route("export")]
        public HttpResponseMessage ExportApplicationsToCSV()
        {
            try
            {
                var applications = ApplicationService.Get();

                if (applications == null || !applications.Any())
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No applications found.");
                }
                // Generate CSV
                var csvBuilder = new StringBuilder();
                csvBuilder.AppendLine("Id   Company           Position            DateApplied   Status");

                foreach (var app in applications)
                {
                    csvBuilder.AppendLine($"{app.Id,-3} {app.Company,-15} {app.Position,-23} {app.DateApplied:yyyy-MM-dd} {app.Status,-10}");
                }
                var result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(csvBuilder.ToString(), Encoding.UTF8, "text/plain")
                };
                
                result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = "JobApplications.csv" 
                };

                return result;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }


    }
}
