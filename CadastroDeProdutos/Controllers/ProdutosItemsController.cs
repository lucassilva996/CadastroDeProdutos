using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CadastroDeProdutos.Data;
using CadastroDeProdutos.Models;

namespace CadastroDeProdutos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosItemsController : ControllerBase
    {
        private readonly ProdutosContext _context;

        public ProdutosItemsController(ProdutosContext context)
        {
            _context = context;
        }

        // GET: api/ProdutosItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutosItemDTO>>> GetProdutosItems()
        {
            return await _context.ProdutosItems
                .Select(x => ProdutoToDTO(x))
                .ToListAsync();
        }

        // GET: api/ProdutosItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutosItemDTO>> GetProdutosItem(long id)
        {
            var produtoItem = await _context.ProdutosItems.FindAsync(id);

            if(produtoItem == null)
            {
                return NotFound();
            }

            return ProdutoToDTO(produtoItem);
        }

        // PUT: api/ProdutosItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProdutosItem(long id, ProdutosItemDTO produtosItemDTO)
        {
            if (id != produtosItemDTO.Id)
            {
                return BadRequest();
            }

            var produtosItem = await _context.ProdutosItems.FindAsync(id);
            if(produtosItem == null)
            {
                return NotFound();
            }

            produtosItem.nomeProduto = produtosItemDTO.nomeProduto;
            produtosItem.precoProduto = produtosItemDTO.precoProduto;
            produtosItem.produtoDisponivel = produtosItemDTO.produtoDisponivel;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ProdutosItemExists(id))
            {
                return NotFound();
            }
            
            return NoContent();
        }

        // POST: api/ProdutosItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ProdutosItem>> CreateProdutosItem(ProdutosItemDTO produtosItemDTO)
        {
            var produtosItem = new ProdutosItem
            {
                nomeProduto = produtosItemDTO.nomeProduto,
                descricaoProduto = produtosItemDTO.descricaoProduto,
                precoProduto = produtosItemDTO.precoProduto,
                produtoDisponivel = produtosItemDTO.produtoDisponivel
            };

            _context.ProdutosItems.Add(produtosItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetProdutosItem),
                new {id = produtosItem.Id },
                ProdutoToDTO(produtosItem));
        }

        // DELETE: api/ProdutosItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProdutosItem(long id)
        {
            var produtosItem = await _context.ProdutosItems.FindAsync(id);

            if(produtosItem == null)
            {
                return NotFound();
            }

            _context.ProdutosItems.Remove(produtosItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProdutosItemExists(long id)
        {
            return _context.ProdutosItems.Any(e => e.Id == id);
        }

        private static ProdutosItemDTO ProdutoToDTO(ProdutosItem produtosItem) =>
            new ProdutosItemDTO
            {
                Id = produtosItem.Id,
                nomeProduto = produtosItem.nomeProduto,
                descricaoProduto = produtosItem.descricaoProduto,
                precoProduto = produtosItem.precoProduto,
                produtoDisponivel = produtosItem.produtoDisponivel
            };
    }
}
