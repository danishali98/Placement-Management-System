using WebProject.EF;
using WebProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebProject.Filters;

namespace WebProject.Controllers
{
    [CustomExceptionFilter]
    public class PlacementsController : Controller
    {
        PlacementsDBContext context;
        List<StudentRecord> allStudents;

        protected ApplicationDbContext ApplicationDbContext { get; set; }

        protected UserManager<ApplicationUser> UserManager { get; set; }


        public PlacementsController()
        {
            //context = new PlacementsDBContext();
            //allStudents = new List<StudentRecord>();

            this.ApplicationDbContext = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.ApplicationDbContext));
        }

        // GET: Placements
        //public ActionResult Index()
        //{
        //    //allStudents = context.StudentRecord.ToList<StudentRecord>();

        //    foreach (var item in allStudents)
        //    {
        //        string rollNum = item.rollNum;
        //        item.experienceList = new List<Experience>();
        //        bool flag = true;
        //        bool first = true;
        //        List<String> ids = new List<string>();
        //        while (flag)
        //        {
        //            Experience exp = new Experience();
        //            exp = context.Experience.FirstOrDefault(e => e.rollNum == rollNum && !ids.Contains(e.Id.ToString()));
        //            if (exp == null)
        //            {
        //                flag = false;
        //            }
        //            else
        //            {
        //                ids.Add(exp.Id.ToString());
        //            }
        //        }
        //    }
        //    return View(allStudents);
        //}
        
        public ActionResult Redirect()
        {
            try
            {
                var _user = User.Identity;
                var s = UserManager.GetRoles(_user.GetUserId());
                if (s[0].ToString() == "Admin")
                    return RedirectToAction("Index", "Admin");
                else
                    return RedirectToAction("Index", "Student");
            }
            catch(Exception ex){
                ViewBag.ErrorTitle = "Unauthorized Action";
                ViewBag.ErrorMessage = "Please login before access.";
                return View("Error");
            }
        }

        public ActionResult Authenticate()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Redirect");
            else
                return RedirectToAction("Login", "Account");
        }

        public ActionResult UnAuthorized()
        {
            return View();
        }

        //public ActionResult CustomError()
        //{
        //    return View();
        //}

    }
}