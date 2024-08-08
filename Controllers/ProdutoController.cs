using AulaDeASPNet.Data;
using AulaDeASPNet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AulaDeASPNet.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly AulaContext _context;

        public ProdutoController(AulaContext context)
        {
            _context = context;
        } 
        public async Task<IActionResult> BuscaProduto()
        {
            return View(await _context.Produtos.ToListAsync());
        }

        public IActionResult CadastroProduto()
        {
            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CadastroProduto([Bind("Nome,Marca,Qtde,ValorUnit")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction("BuscaProduto");
            }
            return View(produto);
        }
    }
}
