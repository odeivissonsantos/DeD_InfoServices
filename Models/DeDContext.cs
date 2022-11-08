using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeD_InfoServices.Models
{
    public class DeDContext : DbContext
    {
        public DeDContext(DbContextOptions<DeDContext> options)
            : base(options)
        {
            
        }

        public DbSet<UsuarioModel> Usuario { get; set; }
        public DbSet<LoginModel> Login { get; set; }
        public DbSet<ProdutoModel> Produto { get; set; }


    }
}
