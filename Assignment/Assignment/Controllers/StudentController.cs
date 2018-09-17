using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    public class StudentController : Controller
    {
        private ThetaProjectContext _ORM=null;
        public StudentController(ThetaProjectContext ORM)
        {
            _ORM = ORM;
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Student S)
        {
            _ORM.Student.Add(S);
            _ORM.SaveChanges();
            ViewBag.M = "The Student has been Added successfully";
            return View();
        }
        public IActionResult List()
        {
            IList<Student> I = _ORM.Student.ToList<Student>();
            return View(I);
        }
        [HttpGet]
        public IActionResult Detail(int rollno)
        {
            Student A = _ORM.Student.Where(m => m.RollNo == rollno).FirstOrDefault<Student>();
            return View(A);
        }
        [HttpPost]
        public IActionResult Detail(Student SA)
        {
            _ORM.Student.Update(SA);
            _ORM.SaveChanges();
            return RedirectToAction("LIST");
        }
    }
}