using DeD_InfoServices.DTOs;
using DeD_InfoServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeD_InfoServices.Services
{
    public class UsuarioService
    {
        private DeDContext _context;

        public UsuarioService(DeDContext context)
        {
            _context = context;
        }

        // Busca um ID no banco
        public UsuarioDTO BuscarPorId(long? ide_usuario)
        {
            return _context.Usuario.Where(x => x.Ide_Usuario == ide_usuario).Select(a => new UsuarioDTO(a)).FirstOrDefault();
        }


        //Trás uma lista com todos os tipos de exames cadastrados no Banco de dados
        public IQueryable<UsuarioModel> BuscarTodos()
        {
            return _context.Usuario;
        }

        // Salva um novo exame no banco
        public void SalvarUsuario(UsuarioDTO usuarioDTO)
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
                UsuarioModel usuario = _context.Usuario.Where(x => x.Ide_Usuario == usuarioDTO.Ide_Usuario && x.Sts_Excluido == false).FirstOrDefault();

                if (usuario == null)
                {
                    if (_context.Usuario.Where(x => x.Email == usuarioDTO.Email && !x.Sts_Excluido).Any())
                        throw new Exception("Já existe um usuário com o mesmo email cadastrado!");

                    usuario = new UsuarioModel();
                    usuario.Sts_Excluido = false;

                    usuario.Nome = usuarioDTO.Nome;
                    usuario.Sobrenome = usuarioDTO.Sobrenome;
                    usuario.Email = usuarioDTO.Email;
                    usuario.Celular = usuarioDTO.Celular;
                    usuario.Perfil = usuarioDTO.Perfil;
                    usuario.Dtc_Cadastro = DateTime.Now;

                    novo = true;
                }
                else
                {
                    usuario.Nome = usuarioDTO.Nome;
                    usuario.Sobrenome = usuarioDTO.Sobrenome;
                    usuario.Email = usuarioDTO.Email;
                    usuario.Celular = usuarioDTO.Celular;
                    usuario.Perfil = usuarioDTO.Perfil;
                }


                if (novo)
                {
                    _context.Usuario.Add(usuario);
                }

                _context.SaveChanges();
            }
            catch (Exception e)
            {
                _context.Dispose();
                 error = e.Message;
            }
        }

        public void ExcluirUsuario(int ide_usuario)
        {
            string error = "";

            try
            {
                UsuarioModel usuario = _context.Usuario.Where(x => x.Ide_Usuario == ide_usuario).FirstOrDefault();
                if (usuario == null) throw new Exception("Usuário não encontrado");

                usuario.Sts_Excluido = true;

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
