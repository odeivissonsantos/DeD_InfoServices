using DeD_InfoServices.DTOs;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeD_InfoServices.Helpers
{
    public class SessionHelper
    {
        private readonly IHttpContextAccessor _httpContext;
        public SessionHelper(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public UsuarioDTO BuscarSessaoDoUsuario()
        {
            string sessaoUsuario = _httpContext.HttpContext.Session.GetString("sessaoUsuarioLogado");
            if (string.IsNullOrEmpty(sessaoUsuario)) return null;
            return JsonConvert.DeserializeObject<UsuarioDTO>(sessaoUsuario);
        }

        public void CriarSessaoDoUsuario(UsuarioDTO ususario)
        {
            string valor = JsonConvert.SerializeObject(ususario);
            _httpContext.HttpContext.Session.SetString("sessaoUsuarioLogado", valor);
        }

        public void RemoveSessaoDoUsuario()
        {
            _httpContext.HttpContext.Session.Remove("sessaoUsuarioLogado");
        }

    }
}
