using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using Assignment.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    public class StudentController : Controller
    {
        private ThetaProjectContext _ORM = null;
        private IHostingEnvironment _ENV = null;
        public StudentController(ThetaProjectContext ORM, IHostingEnvironment ENV)
        {
            _ENV = ENV;
            _ORM = ORM;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Signup(User S)
        {
            try
            {
                _ORM.User.Add(S);
                _ORM.SaveChanges();
                ViewBag.Show = "Resgistrtion Done";
            }
            catch(Exception ex)
            {

            }
            return View();
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Student S, IFormFile Cv)
        {
            string WWWROOT = _ENV.WebRootPath;

            string CVPath = "/Record/CV/" + Guid.NewGuid().ToString() + Path.GetExtension(Cv.FileName);
            Stream CV_Stream = new FileStream(WWWROOT + CVPath, FileMode.Create);
            Cv.CopyTo(CV_Stream);
            CV_Stream.Close();
            S.Cv = CVPath;
            _ORM.Student.Add(S);
            _ORM.SaveChanges();



            MailMessage oEmail = new MailMessage();
            oEmail.From = new MailAddress("bkansari786.ba@gmail.com");
            oEmail.To.Add(new MailAddress(S.Email));
            oEmail.CC.Add(new MailAddress("barkatansari360@gmail.com"));
            oEmail.Subject = "Welcome to ABC";
            oEmail.Body = "Dear " + S.Name + ",<br><br>" + "Thanks for registering with Al-Ansar Corporations, We are glad to have you in our system." + "<br><br>" + "<b>Regards</b>,<br>Al-Ansar Team";

            string APIURL = "http://bulksms.com.pk/api/sms.php?username=923006174740&password=1765&sender=BrandName&mobile=923316125207&message= Welcome to our website.";
            using (var APIClient = new HttpClient())
            {
                Task<HttpResponseMessage> RM = APIClient.GetAsync(APIURL);
                Task<string> FinalRespone = RM.Result.Content.ReadAsStringAsync();
            }
            ModelState.Clear();



            //smtp object
            SmtpClient SMTP = new SmtpClient();
            SMTP.Host = "smtp.gmail.com";
            SMTP.Port = 587; //465 //25
            SMTP.EnableSsl = true;
            SMTP.Credentials = new System.Net.NetworkCredential("bkansari786.ba@gmail.com", "GOOGLEGMAIL");
            oEmail.IsBodyHtml = true;
            if (!string.IsNullOrEmpty(S.Cv))
            {
                oEmail.Attachments.Add(new Attachment(WWWROOT + S.Cv));
            }

            try
            {
                SMTP.Send(oEmail);
            }
            catch (Exception)
            {
            }

            ViewBag.M = "The Student has been Added successfully";
            return View();
        }
        //CV Download code
        public FileResult GetCV(string S_CV)
        {
            if (string.IsNullOrEmpty(S_CV))
            {
                ViewBag.Error = "Invalid Path";
                return null;
            }
            return File(S_CV, new MimeSharp.Mime().Lookup(S_CV), DateTime.Now.ToString("ddMMyyyyhhmmss") + System.IO.Path.GetExtension(S_CV));
        }
        //End of code
        [HttpGet]
        public IActionResult List()
        {
            IList<Student> I = _ORM.Student.ToList<Student>();
            return View(I);
        }
        [HttpPost]
        public IActionResult List(string SName, string Department, string Roll)
        {
            string myString = Roll.ToString();
            IList<Student> AllStudents = _ORM.Student.Where(m => m.Name.Contains(SName) || m.SubjectGroup.Contains(Department) || m.RollNo.Contains(Roll)).ToList<Student>();

            return View(AllStudents);
        }
        [HttpGet]
        public IActionResult Detail(int ID)
        {
            Student A = _ORM.Student.Where(m => m.Id == ID).FirstOrDefault<Student>();
            return View(A);
        }
        [HttpPost]
        public IActionResult Detail(Student SA)
        {
            _ORM.Student.Update(SA);
            _ORM.SaveChanges();
            return RedirectToAction("List");
        }
        public IActionResult Delete(Student S)
        {
            _ORM.Student.Remove(S);
            _ORM.SaveChanges();
            return RedirectToAction("List");
        }
        public string deletestudentajax(Student S)
        {
            string result = "";
            try
            {
                _ORM.Student.Remove(S);
                _ORM.SaveChanges();
                result = "Yes";
            }
            catch (Exception)
            {
                result = "No";
            }
            return result;
        }


        public string ShowAd()
        {
            string Ad = "";
            Ad = "<img class='img img-responsive' src='http://lorempixel.com/400/200/nature/BarkatAnsari'/>";
            return Ad;
        }

        public string GetStudentsNames()
        {
            string Result = "";

            var r = Request;

            IList<Student> All = _ORM.Student.ToList<Student>();
            Result += "<div class='alert alert-success'><h3><span class='glyphicon glyphicon-list'></span> Total Students: " + All.Count + "</h3></div>";

            foreach (Student S in All)
            {
                Result += "<div><a class='btn btn-info btn-block' href='/Student/Detail?ID=" + S.Id + "'><span class='glyphicon glyphicon-user'></span> " + S.Name + "</a></div><br /><div><a class = 'btn btn-danger btn-block' href='/Student/Delete?Id=" + S.Id + "'><span class='glyphicon glyphicon-alert'></span> Delete</a></div><br />";
            }


            return Result;
        }
        [HttpGet]
        public IActionResult Edit(int ID)
        {
            Student E = _ORM.Student.Where(m => m.Id == ID).FirstOrDefault<Student>();
            return View(E);
        }
        [HttpPost]
        public IActionResult Edit(Student Ed, IFormFile Cv)
        {
            _ORM.Student.Update(Ed);
            _ORM.SaveChanges();
            return RedirectToAction("List");
        }
    }
}