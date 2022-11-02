using DeD_InfoServices.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeD_InfoServices.Models
{
    [Table("Usuario")]
    public class UsuarioModel
    {
        [Key]
        [Column("ide_usuario")]
        public int Ide_Usuario { get; set; }

        [Column("nome")]
        public string Nome { get; set; }

        [Column("sobrenome")]
        public string Sobrenome { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("dtc_cadastro")]
        public DateTime Dtc_Cadastro { get; set; }

        [Column("perfil")]
        public PerfilEnum Perfil { get; set; }

        [Column("sts_excluido")]
        public bool Sts_Excluido { get; set; }
    }
}
