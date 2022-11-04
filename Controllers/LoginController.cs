using DeD_InfoServices.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeD_InfoServices.Controllers
{
    public class LoginController : Controller
    {
        private readonly DeDContext _deDContext;

        public LoginController(DeDContext deDContext)
        {
            _deDContext = deDContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Logar(LoginModel loginModel)
        {
            string error = "";
            bool is_action = false;

            try
            {
                var query = _deDContext.Login.Where(x => x.Email == loginModel.Email).FirstOrDefault();

                if (query == null) throw new Exception("Usuário não encontrado");

                if (query.Email == loginModel.Email && query.Senha == loginModel.Senha)
                {
                    //loginModel.Senha = Hash.SHA512(loginModel.Senha);
                    is_action = true;
                }
                else
                {
                    throw new Exception("Email ou Senha inválido. Por favor, tente novamente.");
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return Json(new{ is_action, error });
        }
    }
}
