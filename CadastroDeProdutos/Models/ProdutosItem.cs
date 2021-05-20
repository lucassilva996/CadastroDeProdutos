using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroDeProdutos.Models
{
    public class ProdutosItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string nomeProduto { get; set; }
        public string descricaoProduto { get; set; }
        public double precoProduto { get; set; }
        public bool produtoDisponivel { get; set; }
        public string secret { get; set; }

    }
}
