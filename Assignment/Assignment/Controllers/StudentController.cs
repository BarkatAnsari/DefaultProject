using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private ThetaProjectContext _ORM=null;
        private IHostingEnvironment _ENV = null;
        public StudentController(ThetaProjectContext ORM, IHostingEnvironment ENV)
        {
            _ENV = ENV;
            _ORM = ORM;
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


            
            ViewBag.M = "The Student has been Added successfully";
            return View();
        }
        //CV Download code
        public FileResult GetCV(string S_CV)
        {
            if(string.IsNullOrEmpty(S_CV))
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
            IList<Student> AllStudents = _ORM.Student.Where(m => m.Name.Contains(SName)|| m.SubjectGroup.Contains(Department)|| m.RollNo.Contains(Roll)).ToList<Student>();

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