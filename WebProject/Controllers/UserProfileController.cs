using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebProject.EF;
using WebProject.Filters;
using WebProject.Models;

namespace WebProject.Controllers
{
    [CustomExceptionFilter]
    [CustomAuthenticationFilter]
    [Authorize]
    [CustomAuthorize("Admin", "Student")]
    public class UserProfileController : Controller
    {
        protected ApplicationDbContext ApplicationDbContext { get; set; }
        protected UserManager<ApplicationUser> UserManager { get; set; }
        PlacementsDBContext context;

        public UserProfileController()
        {
            this.ApplicationDbContext = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.ApplicationDbContext));
            context = new PlacementsDBContext();
        }

        public ActionResult ViewUserProfile()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            UserProfile temp = new UserProfile();

            temp.email = user.Email;
            temp.userName = user.UserName;
            temp.role = user.Roles.ToString();
            temp.name = user.Name;

            var _user = User.Identity;
            var s = UserManager.GetRoles(_user.GetUserId());
            if (s[0].ToString() == "Admin")
            {
                ViewBag.Role = "Admin";
            }
            else
            {
                StudentRecord std = context.StudentRecord.FirstOrDefault<StudentRecord>(t => t.rollNum == temp.userName);
                if (std != null)
                {
                    ViewBag.Discipline = std.discipline;
                    ViewBag.Batch = std.batch;
                    ViewBag.Role = "Student";
                }
            }
            return View(temp);
        }
    }
}