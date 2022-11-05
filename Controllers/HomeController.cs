using DeD_InfoServices.DTOs;
using DeD_InfoServices.Filters;
using DeD_InfoServices.Helpers;
using DeD_InfoServices.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DeD_InfoServices.Controllers
{
    [UsuarioPadrao]
    public class HomeController : Controller
    {
        private readonly DeDContext _deDContext;
        private readonly SessionHelper _sessionHelper;

        public HomeController(DeDContext deDContext, SessionHelper sessionHelper)
        {
            _deDContext = deDContext;
            _sessionHelper = sessionHelper;
        }

        public IActionResult Index()
        {
            var usuario = _sessionHelper.BuscarSessaoDoUsuario();

            if (usuario != null)
            {
                return View(usuario);
            }
            else
            {
                throw new Exception("Usuário ainda não está logado, efetue o login.");
                _ = RedirectToAction("Index", "Login");
            }
        }
    }
}
