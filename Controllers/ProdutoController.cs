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
    [UsuarioPadrao]
    public class ProdutoController : Controller
    {
        private readonly ProdutoService _produtoService;
        private readonly SessionHelper _sessionHelper;

        public ProdutoController(ProdutoService produtoService, SessionHelper sessionHelper)
        {
            _produtoService = produtoService;
            _sessionHelper = sessionHelper;
        }

        public IActionResult Index()
        {
            var usuario = _sessionHelper.BuscarSessaoDoUsuario();

            if (usuario != null)
            {
                ViewBag.NomeUsuario = usuario.Nome;
            } 

            return View();
        }

        [HttpPost]
        public virtual IActionResult ProdutoPagination(string sEcho, int iDisplayStart, int iColumns, int iDisplayLength, string sSearch,
                                                       string codigo_interno)
        {
            IQueryable<ProdutoModel> query = _produtoService.BuscarTodos().Where(x => x.Sts_Excluido == false);

            if (!string.IsNullOrEmpty(codigo_interno)) query = query.Where(x => x.Codigo_Interno == codigo_interno);

            if (!string.IsNullOrEmpty(sSearch)) query = query.Where(x => x.Codigo_Interno.ToLower()
                .Contains(Utilities.RemoveSpecialCharacters(sSearch).ToLower())).AsQueryable();

            int recordsTotal = query.Count();

            List<ProdutoDTO> aList = query.OrderBy(x => x.Nome).Skip(iDisplayStart).Take(iDisplayLength)
                .Select(x => new ProdutoDTO(x)).ToList();

            var data = aList.Select(x => new
            {
                ide_produto = x.Ide_Produto,
                nome = x.Nome,
                codigo_interno = x.Codigo_Interno,
                cor = x.Cor,
                valor_unitario = x.Valor_Unitario,
                dtc_cadastro = x.Dtc_Cadastro.ToString("dd/MM/yyyy"),
                editar = $"<a href='{Url.Action("Cadastrar", "Produto")}?codigo_interno={x.Codigo_Interno}'>Editar</a>",
                excluir = $"<a href='#' onclick='modalExcluir({x.Ide_Produto})'>Excluir</a>",
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

        public IActionResult Cadastrar(string codigo_interno)
        {
            var usuario = _sessionHelper.BuscarSessaoDoUsuario();

            if (usuario != null)
            {
                ViewBag.NomeUsuario = usuario.Nome;
            }

            ProdutoDTO produtoDTO = _produtoService.BuscarPorCodigoInterno(codigo_interno);
            if (produtoDTO == null) produtoDTO = new ProdutoDTO();

            return View(produtoDTO);
        }

        [HttpPost]
        public IActionResult CadastrarProduto(ProdutoDTO produtoDTO)
        {
            string error = string.Empty;
            bool is_action = false;

            try
            {
                _produtoService.SalvarProduto(produtoDTO);
                is_action = true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return Json(new { is_action, error });
        }

        [HttpPost]
        public IActionResult ExcluirProduto(int ide_produto)
        {

            string error = string.Empty;
            bool is_action = false;

            try
            {
                _produtoService.ExcluirProduto(ide_produto);
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
