using DeD_InfoServices.DTOs;
using DeD_InfoServices.Helpers;
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
        private readonly SessionHelper _sessionHelper;

        public LoginController(DeDContext deDContext, SessionHelper sessionHelper)
        {
            _deDContext = deDContext;
            _sessionHelper = sessionHelper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Logar(LoginModel loginModel)
        {
            string error = "";
            bool is_action = true;

            UsuarioDTO usuariologado = new UsuarioDTO();

            try
            {
                var query = _deDContext.Login.Where(x => x.Email == loginModel.Email).FirstOrDefault();

                if (query == null) throw new Exception("Usuário não encontrado");

                if (query.Email == loginModel.Email && query.Senha == loginModel.Senha)
                {
                    //loginModel.Senha = Hash.SHA512(loginModel.Senha);
                    var dadosUsuario = _deDContext.Usuario.Where(x => x.Ide_Usuario == query.Ide_Usuario).FirstOrDefault();
                    if (dadosUsuario == null) throw new Exception("EmNão foi possível realizar login, tente novamente.");
                

                    usuariologado.Ide_Usuario = dadosUsuario.Ide_Usuario;
                    usuariologado.Nome = dadosUsuario.Nome;
                    usuariologado.Sobrenome = dadosUsuario.Sobrenome;
                    usuariologado.Email = dadosUsuario.Email;
                    usuariologado.Celular = dadosUsuario.Celular;
                    usuariologado.Perfil = dadosUsuario.Perfil;
                    usuariologado.Dtc_Cadastro = dadosUsuario.Dtc_Cadastro;
                    usuariologado.Sts_Excluido = dadosUsuario.Sts_Excluido;

                    _sessionHelper.CriarSessaoDoUsuario(usuariologado);
                }
                else
                {
                    throw new Exception("Email ou Senha inválido. Por favor, tente novamente.");
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                is_action = false;
            }

            return Json(new{ is_action, error, usuariologado });
        }

        public IActionResult Sair()
        {
            _sessionHelper.RemoveSessaoDoUsuario();
            return RedirectToAction("Index", "Login");
        }
    }
}
