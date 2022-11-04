using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeD_InfoServices.Models
{
    [Table("Produto")]
    public class ProdutoModel
    {
        [Key]
        [Column("ide_produto")]
        public int Ide_Produto { get; set; }

        [Column("nome")]
        public string Nome { get; set; }

        [Column("codigo_interno")]
        public string Codigo_Interno { get; set; }

        [Column("cor")]
        public string Cor { get; set; }

        [Column("valor_unitario")]
        public string Valor_Unitario { get; set; }

        [Column("dtc_cadastro")]
        public DateTime Dtc_Cadastro { get; set; }

        [Column("sts_excluido")]
        public bool Sts_Excluido { get; set; }
    }
}
