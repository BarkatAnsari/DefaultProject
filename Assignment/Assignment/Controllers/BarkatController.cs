using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    public class BarkatController : Controller
    {
        private ThetaProjectContext _ORM=null;
        private IHostingEnvironment _ENV = null;
        public BarkatController(ThetaProjectContext ORM, IHostingEnvironment ENV)
        {
            _ORM = ORM;
            _ENV = ENV;
        }
    }
}