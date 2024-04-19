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
    public class EstoqueProdutoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EstoqueProdutoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EstoqueProduto
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.EstoqueProduto.Include(e => e.Estoque).Include(e => e.Produto);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: EstoqueProduto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estoqueProduto = await _context.EstoqueProduto
                .Include(e => e.Estoque)
                .Include(e => e.Produto)
                .FirstOrDefaultAsync(m => m.EstoqueId == id);
            if (estoqueProduto == null)
            {
                return NotFound();
            }

            return View(estoqueProduto);
        }

        // GET: EstoqueProduto/Create
        public IActionResult Create()
        {
            ViewData["EstoqueId"] = new SelectList(_context.Estoque, "IdEstoque", "IdEstoque");
            ViewData["CodProduto"] = new SelectList(_context.Produto, "CodProduto", "CodProduto");
            return View();
        }

        // POST: EstoqueProduto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EstoqueId,CodProduto,Qtde")] EstoqueProduto estoqueProduto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estoqueProduto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstoqueId"] = new SelectList(_context.Estoque, "IdEstoque", "IdEstoque", estoqueProduto.EstoqueId);
            ViewData["CodProduto"] = new SelectList(_context.Produto, "CodProduto", "CodProduto", estoqueProduto.CodProduto);
            return View(estoqueProduto);
        }

        // GET: EstoqueProduto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estoqueProduto = await _context.EstoqueProduto.FindAsync(id);
            if (estoqueProduto == null)
            {
                return NotFound();
            }
            ViewData["EstoqueId"] = new SelectList(_context.Estoque, "IdEstoque", "IdEstoque", estoqueProduto.EstoqueId);
            ViewData["CodProduto"] = new SelectList(_context.Produto, "CodProduto", "CodProduto", estoqueProduto.CodProduto);
            return View(estoqueProduto);
        }

        // POST: EstoqueProduto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EstoqueId,CodProduto,Qtde")] EstoqueProduto estoqueProduto)
        {
            if (id != estoqueProduto.EstoqueId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estoqueProduto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstoqueProdutoExists(estoqueProduto.EstoqueId))
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
            ViewData["EstoqueId"] = new SelectList(_context.Estoque, "IdEstoque", "IdEstoque", estoqueProduto.EstoqueId);
            ViewData["CodProduto"] = new SelectList(_context.Produto, "CodProduto", "CodProduto", estoqueProduto.CodProduto);
            return View(estoqueProduto);
        }

        // GET: EstoqueProduto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estoqueProduto = await _context.EstoqueProduto
                .Include(e => e.Estoque)
                .Include(e => e.Produto)
                .FirstOrDefaultAsync(m => m.EstoqueId == id);
            if (estoqueProduto == null)
            {
                return NotFound();
            }

            return View(estoqueProduto);
        }

        // POST: EstoqueProduto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estoqueProduto = await _context.EstoqueProduto.FindAsync(id);
            if (estoqueProduto != null)
            {
                _context.EstoqueProduto.Remove(estoqueProduto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstoqueProdutoExists(int id)
        {
            return _context.EstoqueProduto.Any(e => e.EstoqueId == id);
        }
    }
}
