using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SaveAlertBeta.Models;

namespace SaveAlertBeta.Controllers
{
    public class HomeController : Controller
    {
        UserContext db;
        CurrentUser currentUser;
        public HomeController(UserContext context) {
            db = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            
            return View();
        }
        [HttpGet]
        public ViewResult RegistrationForm() {
            return View();
        }
        [HttpPost]
        public IActionResult RegistrationForm(User user) {
            db.Users.Add(user);
            db.SaveChanges();
            return View("Index");
        }

        [HttpPost]
        public  IActionResult Authorization(string mail, string password) {

            User user = db.Users.FirstOrDefault(u => u.Email == mail && u.Password == password);
            if (user == null)
                return null;
            else currentUser = CurrentUser.getInstance(user);
            return View("LetterForm");
            
        }
        [HttpPost]
        public string LetterForm(string address) {

            MailAddress from = new MailAddress("savealert2@gmail.com", CurrentUser.getInstance(new User()).user.Name +
                CurrentUser.getInstance(new User()).user.Surname);
            MailAddress to = new MailAddress("i.d.stolyarchuk@gmail.com");
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Сообщение о правонарушении";
            m.Body = $"Здравствуйте, я Вам пишу по поводу фиксированого правонарушения по адресу {address}, прикрепляю вам файл с фикированным нарушением.\n" +
                $"С уважением,{CurrentUser.getInstance(new User()).user.Name} {CurrentUser.getInstance(new User()).user.Surname})";
           /* if (filePath != null)
            {
                var attach = new Attachment(filePath);
                m.Attachments.Add(attach);
            } */
            SmtpClient access = new SmtpClient("smtp.gmail.com", 587);
            access.UseDefaultCredentials = false;
            access.Credentials = new NetworkCredential("savealert2@gmail.com", "Lalka100");

            access.EnableSsl = true;
            access.Send(m);
            return m.Body;

        }

        }
    }


