using DeD_InfoServices.DTOs;
using DeD_InfoServices.Filters;
using DeD_InfoServices.Helpers;
using DeD_InfoServices.Models;
using DeD_InfoServices.Services;
using DeD_InfoServices.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeD_InfoServices.Controllers
{
    [UsuarioAdmin]
    public class UsuarioController : Controller
    {
        private readonly UsuarioService _usuarioService;
        private readonly SessionHelper _sessionHelper;

        public UsuarioController(UsuarioService usuarioService, SessionHelper sessionHelper)
        {
            _usuarioService = usuarioService;
            _sessionHelper = sessionHelper;
        }

        public IActionResult Index()
        {
            var usuario = _sessionHelper.BuscarSessaoDoUsuario();
            ViewBag.NomeUsuario = usuario.Nome;

            return View();
        }

        [HttpPost]
        public virtual IActionResult UsuarioPagination(string sEcho, int iDisplayStart, int iColumns, int iDisplayLength, string sSearch,
                                                        string email, string nome)
        {
            IQueryable<UsuarioModel> query = _usuarioService.BuscarTodos().Where(x => x.Sts_Excluido == false);

            if (!string.IsNullOrEmpty(email)) query = query.Where(x => x.Email == email);
            if (!string.IsNullOrEmpty(nome)) query = query.Where(x => x.Nome == nome);

            if (!string.IsNullOrEmpty(sSearch)) query = query.Where(x => x.Nome.ToLower()
                .Contains(Utilities.RemoveSpecialCharacters(sSearch).ToLower())).AsQueryable();

           

            int recordsTotal = query.Count();

            List<UsuarioDTO> aList = query.OrderBy(x => x.Nome).Skip(iDisplayStart).Take(iDisplayLength)
                .Select(x => new UsuarioDTO(x)).ToList();

            var data = aList.Select(x => new
            {
                ide_usuario = x.Ide_Usuario,
                nome = $"{x.Nome} {x.Sobrenome}",
                email = x.Email,
                celular = x.Celular,
                dtc_cadastro = x.Dtc_Cadastro.ToString("dd/MM/yyyy"),
                perfil = x.Perfil == Enums.PerfilEnum.Admin ? "Administrador" : "Padrão",
                editar = $"<a href='{Url.Action("Cadastrar", "Usuario")}?ide_usuario={x.Ide_Usuario}'>Editar</a>",
                excluir = $"<a href='#' onclick='modalExcluir({x.Ide_Usuario})'>Excluir</a>",
            }).ToArray();

            return Json(new
            {
                iDraw = 1,
                sEcho,
                iTotalRecords = recordsTotal,
                iTotalDisplayRecords = recordsTotal,
                aaData = data
            });

        }

        public IActionResult Cadastrar(int? ide_usuario)
        {
            var usuario = _sessionHelper.BuscarSessaoDoUsuario();
            ViewBag.NomeUsuario = usuario.Nome;

            UsuarioDTO usuarioDTO = _usuarioService.BuscarPorId(ide_usuario);
            if (usuarioDTO == null) usuarioDTO = new UsuarioDTO();

            return View(usuarioDTO);
        }

        [HttpPost]
        public ActionResult CadastrarUsuario(UsuarioDTO usuarioDTO)
        {
            string error = string.Empty;
            bool is_action = false;

            try
            {
                _usuarioService.SalvarUsuario(usuarioDTO);
                is_action = true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return Json(new { is_action, error, usuarioDTO });
        }

        [HttpPost]
        public ActionResult ExcluirUsuario(int ide_usuario)
        {

            string error = string.Empty;
            bool is_action = false;

            try
            {
                _usuarioService.ExcluirUsuario(ide_usuario);
                is_action = true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return Json(new { is_action, error });
        }
    }
}
