using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroDeProdutos.Models;

namespace CadastroDeProdutos.Data
{
    public class ProdutosContext : DbContext
    {
        public ProdutosContext(DbContextOptions<ProdutosContext> options)
            :base(options)
        {
        }
        public DbSet<ProdutosItem> ProdutosItems { get; set; }
    }
}
