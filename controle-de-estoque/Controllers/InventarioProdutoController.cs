using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Control_Estoque.Data;
using Control_Estoque.Models;

namespace Control_Estoque.Controllers
{
    public class InventarioProdutoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InventarioProdutoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InventarioProduto
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.InventarioProduto.Include(i => i.Inventario).Include(i => i.Produto);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: InventarioProduto/Details/5
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
        public IActionResult Create()
        {
            ViewData["IdInv"] = new SelectList(_context.Inventario, "IdInv", "IdInv");
            ViewData["CodProduto"] = new SelectList(_context.Produto, "CodProduto", "NomeProduto");
            return View();
        }

        // POST: InventarioProduto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdInv,CodProduto,Quantidade")] InventarioProduto inventarioProduto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inventarioProduto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdInv"] = new SelectList(_context.Inventario, "IdInv", "IdInv", inventarioProduto.IdInv);
            ViewData["CodProduto"] = new SelectList(_context.Produto, "CodProduto", "CodProduto", inventarioProduto.CodProduto);
            return View(inventarioProduto);
        }

        // GET: InventarioProduto/Edit/5
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
            return View(inventarioProduto);
        }

        // POST: InventarioProduto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdInv,CodProduto,Quantidade")] InventarioProduto inventarioProduto)
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
            return View(inventarioProduto);
        }

        // GET: InventarioProduto/Delete/5
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
