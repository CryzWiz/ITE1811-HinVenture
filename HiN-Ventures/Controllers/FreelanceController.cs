using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HiN_Ventures.Controllers
{
    public class FreelanceController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
    }
}