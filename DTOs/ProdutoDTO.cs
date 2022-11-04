using DeD_InfoServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeD_InfoServices.DTOs
{
    public class ProdutoDTO
    {
        public int Ide_Produto { get; set; }
        public string Nome { get; set; }
        public string Codigo_Interno { get; set; }
        public string Cor { get; set; }
        public string Valor_Unitario { get; set; }
        public DateTime Dtc_Cadastro { get; set; }
        public bool Sts_Excluido { get; set; }

        public ProdutoDTO()
        {
        }

        public ProdutoDTO(ProdutoModel item)
        {
            Ide_Produto = item.Ide_Produto;
            Nome = item.Nome;
            Codigo_Interno = item.Codigo_Interno;
            Cor = item.Cor;
            Valor_Unitario = item.Valor_Unitario;
            Dtc_Cadastro = item.Dtc_Cadastro;
            Sts_Excluido = item.Sts_Excluido;
        }
    }
}
