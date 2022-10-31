using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeD_InfoServices.Controllers
{
    public class Login : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
