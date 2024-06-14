using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Control_Estoque.Data;
using Control_Estoque.Models;
using Microsoft.AspNetCore.Authorization;
using Control_Estoque.Data.Migrations;

namespace Control_Estoque.Controllers
{
    [Authorize]
    public class InventarioProdutoController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public InventarioProdutoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InventarioProduto
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.InventarioProduto.Include(i => i.Inventario).Include(i => i.Produto);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: InventarioProduto/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? Inventario, int? Produto)
        {
            if (Inventario == null || Produto == null)
            {
                return NotFound();
            }

            var inventarioProduto = await _context.InventarioProduto
                .Include(i => i.Inventario)
                .Include(i => i.Produto)
                .FirstOrDefaultAsync(m => m.IdInv == Inventario && m.CodProduto == Produto);

            if (inventarioProduto == null)
            {
                return NotFound();
            }

            return View(inventarioProduto);
        }

        // GET: InventarioProduto/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["IdInv"] = new SelectList(_context.Inventario, "IdInv", "IdInv");
            ViewData["CodProduto"] = new SelectList(_context.Produto, "CodProduto", "NomeProduto");
            ViewData["TipoMov"] = Enum.GetValues(typeof(TipoMov));
            return View();
        }

        // POST: InventarioProduto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("IdInv,CodProduto,Quantidade,TipoMov,Cpf")] InventarioProduto inventarioProduto)
        {
            ModelState.Remove("IdInv");
            ModelState.Remove("Cpf");
            ModelState.Remove("Produto");
            ModelState.Remove("Inventario");

            if (ModelState.IsValid)
            {
                var username = User.Identity?.Name;
                var currentUser = _context.Users.FirstOrDefault(u => u.Email == username);
                inventarioProduto.Cpf = currentUser ?? new();

                var inventario = await _context.Inventario.FindAsync(inventarioProduto.IdInv);
                inventarioProduto.Inventario = inventario;

                var produto = await _context.Produto.FindAsync(inventarioProduto.CodProduto);
                inventarioProduto.Produto = produto;


                EstoqueProduto produtoBuscado = _context.EstoqueProduto.Find(inventario.IdEstoque, produto.CodProduto);
               // var estoqueProduto = await _context.EstoqueProduto.FindAsync(inventario.Estoque, produto);
                var quant = produtoBuscado.Qtde;

                if (inventarioProduto.TipoMov.Equals(Models.TipoMov.Saida))
                {
                    //restar
                    if (inventarioProduto.Quantidade <= quant)
                    {
                        int resta = quant - inventarioProduto.Quantidade;
                        produtoBuscado.Qtde = resta;
                        _context.Update(produtoBuscado);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        // ModelState.AddModelError()
                        ModelState.AddModelError("Quantidade", "Quantidade Indisponível para Saída");
                        ViewData["IdInv"] = new SelectList(_context.Inventario, "IdInv", "IdInv", inventarioProduto.IdInv);
                        ViewData["CodProduto"] = new SelectList(_context.Produto, "CodProduto", "CodProduto", inventarioProduto.CodProduto);
                        ViewData["TipoMov"] = Enum.GetValues(typeof(TipoMov));
                        return View(inventarioProduto);
                    }

                }
                else
                {
                    //somar
                    int soma = quant + inventarioProduto.Quantidade;
                    produtoBuscado.Qtde = soma;
                    _context.Update(produtoBuscado);
                    await _context.SaveChangesAsync();
                }


                _context.Add(inventarioProduto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdInv"] = new SelectList(_context.Inventario, "IdInv", "IdInv", inventarioProduto.IdInv);
            ViewData["CodProduto"] = new SelectList(_context.Produto, "CodProduto", "CodProduto", inventarioProduto.CodProduto); 
            ViewData["TipoMov"] = Enum.GetValues(typeof(TipoMov));
            return View(inventarioProduto);
        }

        // GET: InventarioProduto/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventarioProduto = await _context.InventarioProduto.FindAsync(id);
            if (inventarioProduto == null)
            {
                return NotFound();
            }
            ViewData["IdInv"] = new SelectList(_context.Inventario, "IdInv", "IdInv", inventarioProduto.IdInv);
            ViewData["CodProduto"] = new SelectList(_context.Produto, "CodProduto", "CodProduto", inventarioProduto.CodProduto);
            ViewData["TipoMov"] = Enum.GetValues(typeof(TipoMov));
            return View(inventarioProduto);
        }

        // POST: InventarioProduto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("IdInv,CodProduto,Quantidade,TipoMov,Cpf")] InventarioProduto inventarioProduto)
        {
            if (id != inventarioProduto.IdInv)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventarioProduto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventarioProdutoExists(inventarioProduto.IdInv))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdInv"] = new SelectList(_context.Inventario, "IdInv", "IdInv", inventarioProduto.IdInv);
            ViewData["CodProduto"] = new SelectList(_context.Produto, "CodProduto", "CodProduto", inventarioProduto.CodProduto);
            ViewData["TipoMov"] = Enum.GetValues(typeof(TipoMov));
            return View(inventarioProduto);
        }

        // GET: InventarioProduto/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventarioProduto = await _context.InventarioProduto
                .Include(i => i.Inventario)
                .Include(i => i.Produto)
                .FirstOrDefaultAsync(m => m.IdInv == id);
            if (inventarioProduto == null)
            {
                return NotFound();
            }

            return View(inventarioProduto);
        }

        // POST: InventarioProduto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inventarioProduto = await _context.InventarioProduto.FindAsync(id);
            if (inventarioProduto != null)
            {
                _context.InventarioProduto.Remove(inventarioProduto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventarioProdutoExists(int id)
        {
            return _context.InventarioProduto.Any(e => e.IdInv == id);
        }
    }
}
