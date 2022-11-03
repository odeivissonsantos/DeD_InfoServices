using DeD_InfoServices.Enums;
using DeD_InfoServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeD_InfoServices.DTOs
{
    public class UsuarioDTO
    {
        public int Ide_Usuario { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public DateTime Dtc_Cadastro { get; set; }
        public PerfilEnum Perfil { get; set; }
        public bool Sts_Excluido { get; set; }

        public UsuarioDTO()
        {
        }

        public UsuarioDTO(UsuarioModel item)
        {
            Ide_Usuario = item.Ide_Usuario;
            Nome = item.Nome;
            Sobrenome = item.Sobrenome;
            Email = item.Email;
            Dtc_Cadastro = item.Dtc_Cadastro;
            Perfil = item.Perfil;
            Sts_Excluido = item.Sts_Excluido;
        }
    }
}
