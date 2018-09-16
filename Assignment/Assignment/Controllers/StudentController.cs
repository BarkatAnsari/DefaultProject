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

    }
}