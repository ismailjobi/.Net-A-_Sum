using AutoMapper;
using BlogProject.Auth;
using BlogProject.DTOs;
using BlogProject.EF;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.Mvc;

namespace BlogProject.Controllers
{
    [UserAccess]
    public class PostController : Controller
    {
        DemoTaskEntities1 db = new DemoTaskEntities1();
        // GET: Post
        public ActionResult Index()
        {
            var user =(User)Session["user"];
            var postdata = (from p in db.Posts
                            where p.UserId.Equals(user.Id)
                            select p).ToList();
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Post, PostDTO>();
                cfg.CreateMap<PostTag, PostTagDTO>();
                cfg.CreateMap<Tag, TagDTO>();
                cfg.CreateMap<Comment, CommentDTO>();
                cfg.CreateMap<User, UserDTO>();
            });
            var mapper = new Mapper(config);
            var data = mapper.Map<List<PostDTO>>(postdata);
           
            return View(data);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(PostDTO postdata)
        {
            var user =(User)Session["user"];
            if (ModelState.IsValid) {
                postdata.PostCreated = DateTime.Now;
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<PostDTO, Post>();
                    cfg.CreateMap<PostTagDTO, PostTag>();
                    cfg.CreateMap<TagDTO, Tag>();
                    cfg.CreateMap<Comment, CommentDTO>();
                    cfg.CreateMap<User, UserDTO>();
                });
                var mapper = new Mapper(config);
                var data = mapper.Map<Post>(postdata);
                data.UserId = user.Id;
                db.Posts.Add(data);
                if (string.IsNullOrEmpty(postdata.TagData)) {
                    TempData["Msg"] = "New Post Added.";
                    db.SaveChanges();
                    return RedirectToAction("Index", "Post");
                }
                var tag =(from t in db.Tags
                          where t.TagName.Equals(postdata.TagData)
                          select t).FirstOrDefault();
                var posttag = new PostTag();
                if (tag == null) {
                    var tagdata = new Tag { TagName = postdata.TagData};
                    db.Tags.Add(tagdata);

                    posttag.PostId = data.Id;
                    posttag.TagId = tagdata.Id;
                    
                    db.PostTags.Add(posttag);
                    TempData["Msg"] = "New Post Added.";
                    db.SaveChanges();
                    return RedirectToAction("Index","Post");
                }
                posttag.PostId = data.Id;
                posttag.TagId = tag.Id;
                db.PostTags.Add(posttag);
                TempData["Msg"] = "New Post Added.";
                db.SaveChanges();
                return RedirectToAction("Index", "Post");
            }
            return View(postdata);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var postdata = db.Posts.Find(id);
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Post, PostDTO>();
                cfg.CreateMap<PostTag, PostTagDTO>();
                cfg.CreateMap<Tag, TagDTO>();
                cfg.CreateMap<Comment, CommentDTO>();
                cfg.CreateMap<User, UserDTO>();
            });
            var mapper = new Mapper(config);
            var data = mapper.Map<PostDTO>(postdata);
            foreach (var t in data.PostTags)
            {
                data.TagData=t.Tag.TagName;
            }
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(PostDTO postdata) {
            
                var post = db.Posts.Find(postdata.Id);
                post.Id = post.Id;
                post.Title = postdata.Title;
                post.PostBody = postdata.PostBody;
                post.PostCreated = post.PostCreated;
                //foreach (var t in post.PostTags)
                //{
                //    if (!t.Tag.TagName.Equals(postdata.TagData))
                //    {
                //        var tagdata = new Tag { TagName = postdata.TagData };
                //        db.Tags.Add(tagdata);
                //    }
                //    t.TagId = t.TagId;
                //}
                db.SaveChanges();
                return RedirectToAction("Index");
            
            
        }
        [HttpGet]
        public ActionResult Delete(int id) {
            var react = (from r in db.Reactions
                         where r.PostId.Equals(id)
                         select r).ToList();
            if (react != null)
            {
                foreach (var r in react)
                {
                    db.Reactions.Remove(r);
                }
            }
            var commnt = (from c in db.Comments
                         where c.PostId.Equals(id)
                         select c).ToList();
            if (commnt != null)
            {
                foreach (var c in commnt)
                {
                    db.Comments.Remove(c);
                }
            }
                                            
            var tag = (from t in db.PostTags
                       where t.PostId.Equals(id)
                       select t).ToList();
            if (tag != null)
            {
                foreach (var t in tag)
                {
                    db.PostTags.Remove(t);
                }
            }
            db.Posts.Remove(db.Posts.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index", "Post");
        }
        public ActionResult Like(int id) {
            var user = (User)Session["user"];
            var data = db.Posts.Find(id);
            var exobj = (from r in db.Reactions
                         where r.UserId.Equals(user.Id) && r.PostId.Equals(id)
                         select r).FirstOrDefault();
            if (exobj != null)
            {
                if (exobj.Reaction1.Equals(0))
                {
                    exobj.Id = exobj.Id;
                    exobj.Reaction1 = 1;
                    exobj.UserId = user.Id;
                    exobj.PostId = id;
                    db.SaveChanges();
                    
                    return RedirectToAction("Index", "User");
                }
                db.Reactions.Remove(exobj);
                db.SaveChanges();
                return RedirectToAction("Index", "User");
            }
            var likedislike = new Reaction
            {
                Reaction1 = 1,
                UserId = user.Id,
                PostId = data.Id,
            };
            db.Reactions.Add(likedislike);
            db.SaveChanges();
            return RedirectToAction("Index","User");
        }
        public ActionResult DisLike(int id)
        {
            var user = (User)Session["user"];
            var exobj = (from r in db.Reactions
                         where r.UserId.Equals(user.Id)  && r.PostId.Equals(id)
                         select r).FirstOrDefault();
            
            var likedislike = new Reaction();
            if (exobj != null){
                if (exobj.Reaction1.Equals(1))
                {
                    exobj.Id = exobj.Id;
                    exobj.Reaction1 = 0;
                    exobj.UserId = user.Id;
                    exobj.PostId = id;
                    
                    db.SaveChanges();
                    return RedirectToAction("Index", "User");
                }
                db.Reactions.Remove(exobj);
                db.SaveChanges();
                return RedirectToAction("Index", "User");
            }

            likedislike.Reaction1 = 0;
            likedislike.UserId = user.Id;
            likedislike.PostId = id;

            db.Reactions.Add(likedislike);
            db.SaveChanges();
            return RedirectToAction("Index", "User");
        }
        public static int LikeCount(int id)
        {
            DemoTaskEntities1 db = new DemoTaskEntities1();
            var exobj = (from r in db.Reactions
                         where r.PostId.Equals(id) && r.Reaction1.Equals(1)
                         select r).ToList();
            return exobj.Count();
        }
        public static int DisLikeCount(int id)
        {
            DemoTaskEntities1 db = new DemoTaskEntities1();
            var exobj = (from r in db.Reactions
                         where r.PostId.Equals(id) && r.Reaction1.Equals(0)
                         select r).ToList();
            return exobj.Count();
        }
        [HttpGet]
        public ActionResult Comments(int id)
        {
            var data = db.Posts.Find(id);
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Post, PostDTO>();
                cfg.CreateMap<PostTag, PostTagDTO>();
                cfg.CreateMap<Tag, TagDTO>();
                cfg.CreateMap<Comment, CommentDTO>();
                cfg.CreateMap<User, UserDTO>();
            });
            var mapper = new Mapper(config);
            var post = mapper.Map<PostDTO>(data);
            return View(post);

        }
        [HttpPost]
        public ActionResult Comments(CommentDTO codata)
        {
            var user = (User)Session["user"];
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<CommentDTO, Comment>();
            });
            var mapper = new Mapper(config);
            var co = mapper.Map<Comment>(codata);
            co.UserId = user.Id;
            co.PostId=4;
            co.CommentTime=DateTime.Now;

            db.Comments.Add(co);
            db.SaveChanges();
            return RedirectToAction("Comments");

        }

    }
}