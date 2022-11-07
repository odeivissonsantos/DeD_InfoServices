using DeD_InfoServices.DTOs;
using DeD_InfoServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeD_InfoServices.Services
{
    public class ProdutoService
    {
        private DeDContext _context;

        public ProdutoService(DeDContext context)
        {
            _context = context;
        }

        // Busca um ID no banco
        public ProdutoDTO BuscarPorCodigoInterno(string codigo_interno)
        {
            return _context.Produto.Where(x => x.Codigo_Interno == codigo_interno).Select(a => new ProdutoDTO(a)).FirstOrDefault();
        }


        //Trás uma lista com todos os tipos de exames cadastrados no Banco de dados
        public IQueryable<ProdutoModel> BuscarTodos()
        {
            return _context.Produto;
        }

        // Salva um novo exame no banco
        public void SalvarProduto(ProdutoDTO produtoDTO)
        {
            bool novo = false;
            string error = "";

            //if (string.IsNullOrEmpty(exameDTO.Nome.Trim())) throw new Exception("Nome do exame é obrigatório!");
            //if (exameDTO.Nome.Length < 5) throw new Exception("Nome do exame deve ter no mínimo 5 caracteres!");
            //if (exameDTO.DuracaoMinima <= 0) throw new Exception("Tempo de duração do exame é obrigatório!");
            //if (exameDTO.NumMinQuestoes <= 0 || exameDTO.NumMinQuestoes > 150) throw new Exception("Número mínimo de acertos inválido!");
            //if (exameDTO.NumSequencia < 1 || exameDTO.NumSequencia > 3) throw new Exception("Número da sequência inválido!");
            //if (exameDTO.CodExameRenach < 1) throw new Exception("Código exame renach inválido!");

            try
            {
                ProdutoModel produto = _context.Produto.Where(x => x.Codigo_Interno == produtoDTO.Codigo_Interno && x.Sts_Excluido == false).FirstOrDefault();

                if (produto == null)
                {
                    if (_context.Produto.Where(x => x.Codigo_Interno == produtoDTO.Codigo_Interno && !x.Sts_Excluido).Any())
                        throw new Exception("Já existe um produto com o esse codigo interno");

                    produto = new ProdutoModel();

                    produto.Sts_Excluido = false;
                    produto.Nome = produtoDTO.Nome;
                    produto.Codigo_Interno = Guid.NewGuid().ToString().Substring(0, 6);
                    produto.Cor = produtoDTO.Cor;
                    produto.Valor_Unitario = produtoDTO.Valor_Unitario;
                    produto.Dtc_Cadastro = DateTime.Now;

                    novo = true;
                }
                else
                {
                    produto.Nome = produtoDTO.Nome;
                    produto.Codigo_Interno = produtoDTO.Codigo_Interno;
                    produto.Cor = produtoDTO.Cor;
                    produto.Valor_Unitario = produtoDTO.Valor_Unitario;

                }


                if (novo)
                {
                    _context.Produto.Add(produto);
                }

                _context.SaveChanges();
            }
            catch (Exception e)
            {
                _context.Dispose();
                 error = e.Message;
            }
        }

        public void ExcluirProduto(int ide_produto)
        {
            string error = "";

            try
            {
                ProdutoModel produto = _context.Produto.Where(x => x.Ide_Produto == ide_produto && x.Sts_Excluido == false).FirstOrDefault();
                if (produto == null) throw new Exception("Não foi encontrado o produto com este id");

                produto.Sts_Excluido = true;

                _context.SaveChanges();
            }
            catch (Exception e)
            {
                _context.Dispose();
                error = e.Message;
            }
        }

    }
}
