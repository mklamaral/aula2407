using AulaDeASPNet.Data;
using AulaDeASPNet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace AulaDeASPNet.Controllers
{
    public class ClienteController : Controller
    {
        private readonly AulaContext _context;

        public ClienteController(AulaContext context)
        {
            _context = context;
        }
        //Buscar Clientes
        public async Task<IActionResult> BuscaCliente()
        { 
            return View(await _context.Clientes.ToListAsync());
        }

        //Detalhes Clientes
        public async Task<IActionResult> DetalhesCliente(int Id)
        {
            return View(await _context.Clientes.FindAsync(Id));
        }

        //Alterar Clientes
        public async Task<IActionResult> AlterarCliente(int Id)
        {
            return View(await _context.Clientes.FindAsync(Id));
        }

        // Cadastro de Clientes
        public IActionResult CadastroCliente()
        {
                return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CadastroCliente([Bind("Id,Nome,RG,CPF,Usuario,Senha,CEP,UF,Cidade,Bairro,Rua,Numero,Complemento")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction("BuscaCliente");
            }
            return View(cliente);
        }
    }
}
