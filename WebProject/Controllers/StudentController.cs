using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProject.EF;
using WebProject.Filters;
using WebProject.Models;

namespace WebProject.Controllers
{
    [CustomExceptionFilter]
    [CustomAuthenticationFilter]
    [CustomAuthorize("Student")]
    public class StudentController : Controller
    {
        protected ApplicationDbContext ApplicationDbContext { get; set; }
        protected UserManager<ApplicationUser> UserManager { get; set; }

        private List<Message> messages;
        private List<Experience> experience;

        PlacementsDBContext context;

        string userName;

        public StudentController()
        {
            this.ApplicationDbContext = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.ApplicationDbContext));

            messages = new List<Message>();
            experience = new List<Experience>();

            context = new PlacementsDBContext();

            string completed = "Completed";
            messages = context.Message.Where(t => t.status == completed).ToList<Message>();

            ViewBag.Unread = messages.Count;
        }

        public ActionResult Index()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            UserProfile temp = new UserProfile();
            temp.email = user.Email;
            temp.userName = user.UserName;
            userName = user.UserName;

            experience = context.Experience.Where(t => t.rollNum == temp.userName).ToList<Experience>();
            ViewBag.totalExperiences = experience.Count;

            messages = context.Message.Where(t => t.rollNum == temp.userName).ToList<Message>();
            ViewBag.totalMessages = messages.Count;

            return View();
        }

        public ActionResult ViewExperiences()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            UserProfile temp = new UserProfile();
            temp.email = user.Email;
            temp.userName = user.UserName;
            userName = user.UserName;

            experience = context.Experience.Where(t => t.rollNum == userName).ToList<Experience>();
            return View(experience);
        }

        public ActionResult ViewMessages()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            UserProfile temp = new UserProfile();
            temp.userName = user.UserName;
            userName = user.UserName;

            messages = context.Message.Where(t => t.rollNum == temp.userName).ToList<Message>();

            return View(messages);
        }

        [HttpGet]
        public ActionResult CreateMessage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateMessage(FormCollection collection)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            UserProfile temp = new UserProfile();
            temp.userName = user.UserName;
            userName = user.UserName;

            Message msg = new Message();

            msg.rollNum = userName;
            msg.subject = collection["subject"];
            msg.text = collection["text"];
            msg.time = System.DateTime.Now;
            msg.status = "Sent";

            context.Message.Add(msg);
            context.SaveChanges();

            return RedirectToAction("ViewMessages");
        }
    }
}