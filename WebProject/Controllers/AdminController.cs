using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    [CustomAuthorize("Admin")]
    public class AdminController : Controller
    {
        private List<StudentRecord> records;
        private List<Message> messages;
        private List<Experience> experience;

        protected ApplicationDbContext ApplicationDbContext { get; set; }
        protected UserManager<ApplicationUser> UserManager { get; set; }

        PlacementsDBContext context;

        public AdminController()
        {
            context = new PlacementsDBContext();
            records = new List<StudentRecord>();
            messages = new List<Message>();

            this.ApplicationDbContext = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.ApplicationDbContext));
            context = new PlacementsDBContext();

            string read = "Sent";
            messages = context.Message.Where(t => t.status == read).ToList<Message>();

            ViewBag.Unread = messages.Count;
        }

        //Landing page for Admin
        public ActionResult Index()
        {
            records = context.StudentRecord.ToList<StudentRecord>();
            messages = context.Message.ToList<Message>();
            experience = context.Experience.ToList<Experience>();

            ViewBag.totalRecords = records.Count();
            ViewBag.totalMessages = messages.Count();
            ViewBag.totalExperiences = experience.Count();

            return View();
        }

        //View all Student Records without Experience---------------------------------
        public ActionResult ViewRecords()
        {
            records = context.StudentRecord.OrderBy(t => t.rollNum).ToList<StudentRecord>();

            experience = context.Experience.ToList<Experience>();

            foreach (var item in records)
            {
                string rollNum = item.rollNum;
                item.experienceList = new List<Experience>();
                bool flag = true;
                List<String> ids = new List<string>();
                while (flag)
                {
                    Experience exp = new Experience();
                    exp = context.Experience.FirstOrDefault(e => e.rollNum == rollNum && !ids.Contains(e.Id.ToString()));
                    if (exp == null)
                    {
                        flag = false;
                    }
                    else
                    {
                        ids.Add(exp.Id.ToString());
                    }
                }
            }

            return View(records);
        }

        [HttpPost]
        public ActionResult ViewRecords(string tags)
        {
            records = context.StudentRecord.OrderBy(t => t.rollNum).Where(t => t.rollNum == tags).ToList<StudentRecord>();

            experience = context.Experience.ToList<Experience>();

            foreach (var item in records)
            {
                string rollNum = item.rollNum;
                item.experienceList = new List<Experience>();
                bool flag = true;
                List<String> ids = new List<string>();
                while (flag)
                {
                    Experience exp = new Experience();
                    exp = context.Experience.FirstOrDefault(e => e.rollNum == rollNum && !ids.Contains(e.Id.ToString()));
                    if (exp == null)
                    {
                        flag = false;
                    }
                    else
                    {
                        ids.Add(exp.Id.ToString());
                    }
                }
            }

            return View(records);
        }

        public JsonResult getRecords()
        {
            records = context.StudentRecord.OrderBy(t => t.rollNum).ToList<StudentRecord>();
            return Json(records, JsonRequestBehavior.AllowGet);
        }

        //Create Student Record---------------------------------
        [HttpGet]
        public ActionResult CreateStudentRecord()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateStudentRecord(FormCollection collection)
        {
            StudentRecord std = new StudentRecord();
            std.rollNum = collection["rollNum"];
            std.name = collection["name"];
            std.discipline = collection["discipline"];
            std.cgpa = collection["cgpa"];
            std.batch = collection["batch"];

            context.StudentRecord.Add(std);
            context.SaveChanges();

            return RedirectToAction("ViewRecords");
        }

        //View Student Record---------------------------------
        public ActionResult StudentRecordDetails(string id)
        {
            StudentRecord item = new StudentRecord();
            item = context.StudentRecord.FirstOrDefault<StudentRecord>(t => t.rollNum == id);
            item.experienceList = new List<Experience>();
            bool flag = true;
            List<String> ids = new List<string>();
            while (flag)
            {
                Experience exp = new Experience();
                exp = context.Experience.FirstOrDefault(e => e.rollNum == id && !ids.Contains(e.Id.ToString()));
                if (exp == null)
                {
                    flag = false;
                }
                else
                {
                    ids.Add(exp.Id.ToString());
                }
            }
            return View(item);
        }

        //Edit Student Record---------------------------------
        [HttpGet]
        public ActionResult EditStudentRecord(string id)
        {
            StudentRecord std = new StudentRecord();
            std = context.StudentRecord.FirstOrDefault<StudentRecord>(t => t.rollNum == id);
            return View(std);
        }

        [HttpPost]
        public ActionResult EditStudentRecord(string id, FormCollection collection)
        {
            StudentRecord std = new StudentRecord();
            std = context.StudentRecord.FirstOrDefault<StudentRecord>(t => t.rollNum == id);

            //std.rollNum = collection["rollNum"];
            std.name = collection["name"];
            std.discipline = collection["discipline"];
            std.cgpa = collection["cgpa"];
            std.batch = collection["batch"];

            context.SaveChanges();

            return RedirectToAction("ViewRecords");
        }

        //Delete Student Record---------------------------------
        [HttpGet]
        public ActionResult DeleteStudentRecord(string id)
        {
            StudentRecord std = new StudentRecord();
            std = context.StudentRecord.FirstOrDefault<StudentRecord>(t => t.rollNum == id);
            return View(std);
        }

        [HttpPost]
        public ActionResult DeleteStudentRecord(string id, FormCollection collection)
        {
            StudentRecord std = new StudentRecord();
            std = context.StudentRecord.FirstOrDefault<StudentRecord>(t => t.rollNum == id);
            context.StudentRecord.Remove(std);
            context.SaveChanges();
            return RedirectToAction("ViewRecords");
        }

        //Add Experience
        [HttpGet]
        public ActionResult AddExperience(string id)
        {
            Experience exp = new Experience();
            exp.rollNum = id;
            return View(exp);
        }

        [HttpPost]
        public ActionResult AddExperience(FormCollection collection)
        {
            try
            {
                Experience exp = new Experience();
                exp.rollNum = collection["rollNum"];
                exp.organization = collection["organization"];
                exp.position = collection["position"];
                exp.location = collection["location"];
                exp.startDate = collection["startDate"];
                exp.endDate = collection["endDate"];
                exp.stipend = Int32.Parse(collection["stipend"]);
                exp.details = collection["details"];

                context.Experience.Add(exp);
                context.SaveChanges();

                return RedirectToAction("ViewRecords");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorTitle = "Roll Number not in record.";
                ViewBag.ErrorMessage = "Please check the roll number field again. A record against this roll number is not present in the database.";
                return View("Error");
            }
        }

        //Edit Experiece
        [HttpGet]
        public ActionResult EditExperience(int id)
        {
            Experience exp = new Experience();
            exp = context.Experience.FirstOrDefault<Experience>(t => t.Id == id);
            return View(exp);
        }

        [HttpPost]
        public ActionResult EditExperience(int id, FormCollection collection)
        {
            try
            {
                Experience exp = new Experience();
                exp = context.Experience.FirstOrDefault<Experience>(t => t.Id == id);

                exp.rollNum = collection["rollNum"];
                exp.organization = collection["organization"];
                exp.position = collection["position"];
                exp.location = collection["location"];
                exp.startDate = collection["startDate"];
                exp.endDate = collection["endDate"];
                exp.stipend = Int32.Parse(collection["stipend"]);
                exp.details = collection["details"];

                context.SaveChanges();

                return RedirectToAction("ViewRecords");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorTitle = "Roll Number not in record.";
                ViewBag.ErrorMessage = "Please check the roll number field again. A record against this roll number is not present in the database.";
                return View("Error");
            }
        }

        //Delete Experience
        [HttpGet]
        public ActionResult DeleteExperience(int id)
        {
            Experience exp = new Experience();
            exp = context.Experience.FirstOrDefault<Experience>(t => t.Id == id);
            return View(exp);
        }

        [HttpPost]
        public ActionResult DeleteExperience(int id, FormCollection collection)
        {
            Experience exp = new Experience();
            exp = context.Experience.FirstOrDefault<Experience>(t => t.Id == id);

            context.Experience.Remove(exp);
            context.SaveChanges();

            return RedirectToAction("ViewRecords");
        }

        //View all Messages---------------------------------
        public ActionResult ViewMessages()
        {
            messages = context.Message.ToList<Message>();
            return View(messages);
        }

        //Create Message
        [HttpGet]
        public ActionResult CreateMessage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateMessage(FormCollection collection)
        {
            try
            {
                Message msg = new Message();
                msg.rollNum = collection["rollNum"];
                msg.subject = collection["subject"];
                msg.text = collection["text"];
                msg.status = "Completed";
                msg.time = System.DateTime.Now;

                context.Message.Add(msg);

                context.SaveChanges();

                return RedirectToAction("ViewMessages");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorTitle = "Roll Number not in record.";
                ViewBag.ErrorMessage = "Please check the roll number field again. A record against this roll number is not present in the database.";
                return View("Error");
            }
        }

        //View Message Details
        public ActionResult MessageDetails(int id)
        {
            Message msg = new Message();
            msg = context.Message.FirstOrDefault<Message>(t => t.Id == id);
            return View(msg);
        }

        //Edit Message Status
        [HttpGet]
        public ActionResult EditMessage(int id)
        {
            Message msg = new Message();
            msg = context.Message.FirstOrDefault<Message>(t => t.Id == id);

            ViewBag.Status = new SelectList(new List<SelectListItem>
                        {
                            new SelectListItem { Text = "Sent"},
                            new SelectListItem { Text = "Read"},
                            new SelectListItem { Text = "In Progress"},
                            new SelectListItem { Text = "Completed"}
                        }, "Text", "Text");
            return View(msg);
        }

        [HttpPost]
        public ActionResult EditMessage(int id, FormCollection collection)
        {
            Message msg = new Message();
            msg = context.Message.FirstOrDefault<Message>(t => t.Id == id);

            msg.status = collection["status"];

            context.SaveChanges();

            return RedirectToAction("ViewMessages");
        }

        //Delete Message
        [HttpGet]
        public ActionResult DeleteMessage(int id)
        {
            Message msg = new Message();
            msg = context.Message.FirstOrDefault<Message>(t => t.Id == id);
            return View(msg);
        }

        [HttpPost]
        public ActionResult DeleteMessage(int id, FormCollection collection)
        {
            Message msg = new Message();
            msg = context.Message.FirstOrDefault<Message>(t => t.Id == id);

            context.Message.Remove(msg);
            context.SaveChanges();

            return RedirectToAction("ViewMessages");
        }

        [HttpGet]
        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> ResetPassword(string email)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            UserStore<ApplicationUser> store = new UserStore<ApplicationUser>(context);
            UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(store);
            var user = UserManager.FindByEmail(email);
            string userId = user.Id;//"<YourLogicAssignsRequestedUserId>";
            string newPassword = "test@123"; //"<PasswordAsTypedByUser>";
            string hashedNewPassword = UserManager.PasswordHasher.HashPassword(newPassword);
            ApplicationUser cUser = await store.FindByIdAsync(userId);
            await store.SetPasswordHashAsync(cUser, hashedNewPassword);
            await store.UpdateAsync(cUser);
            return RedirectToAction("Index");
        }

        //[HttpGet]
        //public ActionResult RegisterUser()
        //{
        //    ViewBag.Name = new SelectList(ApplicationDbContext.Roles.ToList(), "Name", "Name");
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult RegisterUser(RegisterViewModel model)
        //{
        //    return RedirectToAction("Register", "Account", model);
        //}

    }
}