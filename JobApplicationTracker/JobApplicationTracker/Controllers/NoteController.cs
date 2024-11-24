
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
    [RoutePrefix("api/note")]
    public class NoteController : ApiController
    {
        [HttpPost]
        [Route("create/{id}")]
        public HttpResponseMessage Create(NoteDTO obj,int id)
        {
            try
            {
                var data = NoteService.Create(obj,id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Msg = "Contact Support", Api = "api/note/create" });
            }
        }
        [HttpGet]
        [Route("all")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                var data = NoteService.Get();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Msg = "Contact Support", Api = "api/note/all" });
            }
        }
       
        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage Get(int id)
        {

            try
            {
                var data = NoteService.Get(id);
                if (data != null)
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                else return Request.CreateResponse(HttpStatusCode.OK, new { Msg = "Note not Found" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Msg = "Contact Support", Api = "api/note/" + id });
            }

        }
        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(NoteDTO obj)
        {
            try
            {
                var data = NoteService.Update(obj);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Msg = "Contact Support", Api = "api/note/update" });
            }
        }
        [HttpGet]
        [Route("delete/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var data = NoteService.Delete(id);
                if (data)
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                else return Request.CreateResponse(HttpStatusCode.OK, new { Msg = "Note Unable To Delete" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Msg = "Contact Support", Api = "api/note/delete" + id });
            }
        }
    }
}
