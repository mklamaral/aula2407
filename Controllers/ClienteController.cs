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
        public async Task<IActionResult> CadastroCliente(int? Id)
        {
            if (Id == null)
            {
                return View();
            }
            else
            {
                return View(await _context.Clientes.FindAsync(Id));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CadastroCliente([Bind("Id,Nome,RG,CPF,Usuario,Senha,CEP,UF,Cidade,Bairro,Rua,Numero,Complemento")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                if (cliente.Id != 0)
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    _context.Add(cliente);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("BuscaCliente");
                }
                return RedirectToAction("BuscaCliente");
            }
            return View(cliente);
        }
    }
}
