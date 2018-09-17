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
    }
}