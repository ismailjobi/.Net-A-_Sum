using AutoMapper;
using BlogProject.Auth;
using BlogProject.DTOs;
using BlogProject.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogProject.Controllers
{
    [UserAccess]
    public class UserController : Controller
    {
        DemoTaskEntities1 db = new DemoTaskEntities1();
        // GET: User
        public ActionResult Index()
        {
            var postdata = db.Posts.ToList();
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Post,PostDTO >();
                cfg.CreateMap<PostTag, PostTagDTO>();
                cfg.CreateMap<Tag, TagDTO>();
                cfg.CreateMap<Comment, CommentDTO>();
                cfg.CreateMap<User, UserDTO>();
            });
            var mapper = new Mapper(config);
            var data = mapper.Map<List<PostDTO>>(postdata);
            var rdata = (from d in db.Posts
                         where DbFunctions.TruncateTime(d.PostCreated) == DbFunctions.TruncateTime(DateTime.Today)
                         select d).ToList();
            var crdata = mapper.Map<List<PostDTO>>(rdata);
            ViewBag.redata=crdata;

            return View(data);
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult SignUp(RegistrationDTO UserData)
        {
            if (ModelState.IsValid)
            {
                var role = (from type in db.Roles
                            where type.RoleType.Equals("User")
                            select type).FirstOrDefault();
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<RegistrationDTO, User>();
                });
                var mapper = new Mapper(config);
                var data = mapper.Map<User>(UserData);
                data.Name = UserData.FirstName.Trim() + " " + UserData.LastName.Trim(); 
                data.RoleId = role.Id;
                db.Users.Add(data);
                db.SaveChanges();
                return RedirectToAction("Index", "Login");
            }
            return View(UserData);
        }

        [HttpPost]
        public ActionResult Search(string Search)
        {
            var datatitle = (from s in db.Posts
                        where s.Title.Contains(Search)
                        select s).ToList();
            //var datatag = (from s in db.PostTags
            //            where s.Tag.TagName.Contains(Search)
            //            select s).ToList();
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Post, PostDTO>();
                cfg.CreateMap<PostTag, PostTagDTO>();
                cfg.CreateMap<Tag, TagDTO>();
                cfg.CreateMap<Comment, CommentDTO>();
                cfg.CreateMap<User, UserDTO>();
            });
            
            var mapper = new Mapper(config);
            var Convertdata = mapper.Map<List<PostDTO>>(datatitle);

            return View("Index", Convertdata);
        }

    }
}