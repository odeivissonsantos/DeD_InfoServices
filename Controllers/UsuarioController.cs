using DeD_InfoServices.Models;
using DeD_InfoServices.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeD_InfoServices.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly DeDContext _deDContext;

        public UsuarioController(DeDContext deDContext)
        {
            _deDContext = deDContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public virtual IActionResult UsuarioPagination(string sEcho, int iDisplayStart, int iColumns, int iDisplayLength, string sSearch,
                                                        string email, string nome)
        {
            IQueryable<UsuarioModel> query = _deDContext.Usuario;

            if (!string.IsNullOrEmpty(email)) query = query.Where(x => x.Email == email);
            if (!string.IsNullOrEmpty(nome)) query = query.Where(x => x.Nome == nome);

            sSearch = Utilities.RemoveSpecialCharacters(sSearch).ToLower();

            int recordsTotal = query.Count();

            List<UsuarioModel> aList = query.OrderByDescending(x => x.Nome).Skip(iDisplayStart).Take(iDisplayLength)
                .ToList();

            var data = aList.Select(x => new
            {
                ide_usuario = x.Ide_Usuario,
                nome = $"{x.Nome} {x.Sobrenome}",
                email = x.Email,
                dtc_cadastro = x.Dtc_Cadastro,
                perfil = x.Perfil,
                editar =  $"<a href='{Url.Action("ExameCadastro", "SGPE")}?ide_usuario={x.Ide_Usuario}'>Editar</a>",
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
    }
}
