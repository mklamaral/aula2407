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
        public async Task<IActionResult> BuscaProduto(int pagina = 1)
        {
            var QtdeTProdutos = 2;

            var items = await _context.Produtos.OrderBy(c => c.Nome).ToListAsync();
            //var pagedItems = items.Skip((pagina - 1) * QtdeTProdutos)
            //                      .Take(QtdeTProdutos).ToList();

            //Passando os dados e informações de paginação para a view
            ViewBag.QtdePaginas = (int)Math.Ceiling((double)items.Count() / QtdeTProdutos);
            ViewBag.PaginaAtual = pagina;
            ViewBag.QtdeTProdutos = QtdeTProdutos;

            return View(items);
            //return View(await _context.Produtos.ToListAsync());
        }

        //Detalhes Produtos
        public async Task<IActionResult> DetalhesProduto(int Id)
        {
            return View(await _context.Produtos.FindAsync(Id));
        }


        // Cadastro de Clientes
        public async Task<IActionResult> CadastroProduto(int? Id)
        {
            if (Id == null)
            {
                return View();
            }
            else
            {
                return View(await _context.Produtos.FindAsync(Id));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CadastroProduto([Bind("Nome,Marca,Qtde,ValorUnit")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                if (produto.Id != 0)
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                    TempData["msg"] = "2";
                }
                else
                {
                    _context.Add(produto);
                    await _context.SaveChangesAsync();
                    TempData["msg"] = "1";
                }
                return RedirectToAction("BuscaProduto");
            }
            return View(produto);
        }
        public async Task<IActionResult> DeletarProduto(int Id)
        {
            if (Id != 0)
            {
                var produto = await _context.Produtos.FindAsync(Id);

                if (produto != null)
                {
                    _context.Remove<Produto>(produto);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("BuscaProduto");
            }
            return RedirectToAction("BuscaProduto");
        }
    }
}
