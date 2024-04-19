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
    public class ProdutoFornecedorRecebsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProdutoFornecedorRecebsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProdutoFornecedorRecebs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProdutoFornecedorReceb.Include(p => p.Fornecedor).Include(p => p.Produto);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProdutoFornecedorRecebs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtoFornecedorReceb = await _context.ProdutoFornecedorReceb
                .Include(p => p.Fornecedor)
                .Include(p => p.Produto)
                .FirstOrDefaultAsync(m => m.IdFornecedor == id);
            if (produtoFornecedorReceb == null)
            {
                return NotFound();
            }

            return View(produtoFornecedorReceb);
        }

        // GET: ProdutoFornecedorRecebs/Create
        public IActionResult Create()
        {
            ViewData["IdFornecedor"] = new SelectList(_context.Fornecedor, "IdFornecedor", "IdFornecedor");
            ViewData["CodProduto"] = new SelectList(_context.Produto, "CodProduto", "CodProduto");
            return View();
        }

        // POST: ProdutoFornecedorRecebs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFornecedor,CodProduto,Qtde,DataRecebimento")] ProdutoFornecedorReceb produtoFornecedorReceb)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produtoFornecedorReceb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdFornecedor"] = new SelectList(_context.Fornecedor, "IdFornecedor", "IdFornecedor", produtoFornecedorReceb.IdFornecedor);
            ViewData["CodProduto"] = new SelectList(_context.Produto, "CodProduto", "CodProduto", produtoFornecedorReceb.CodProduto);
            return View(produtoFornecedorReceb);
        }

        // GET: ProdutoFornecedorRecebs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtoFornecedorReceb = await _context.ProdutoFornecedorReceb.FindAsync(id);
            if (produtoFornecedorReceb == null)
            {
                return NotFound();
            }
            ViewData["IdFornecedor"] = new SelectList(_context.Fornecedor, "IdFornecedor", "IdFornecedor", produtoFornecedorReceb.IdFornecedor);
            ViewData["CodProduto"] = new SelectList(_context.Produto, "CodProduto", "CodProduto", produtoFornecedorReceb.CodProduto);
            return View(produtoFornecedorReceb);
        }

        // POST: ProdutoFornecedorRecebs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdFornecedor,CodProduto,Qtde,DataRecebimento")] ProdutoFornecedorReceb produtoFornecedorReceb)
        {
            if (id != produtoFornecedorReceb.IdFornecedor)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produtoFornecedorReceb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoFornecedorRecebExists(produtoFornecedorReceb.IdFornecedor))
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
            ViewData["IdFornecedor"] = new SelectList(_context.Fornecedor, "IdFornecedor", "IdFornecedor", produtoFornecedorReceb.IdFornecedor);
            ViewData["CodProduto"] = new SelectList(_context.Produto, "CodProduto", "CodProduto", produtoFornecedorReceb.CodProduto);
            return View(produtoFornecedorReceb);
        }

        // GET: ProdutoFornecedorRecebs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtoFornecedorReceb = await _context.ProdutoFornecedorReceb
                .Include(p => p.Fornecedor)
                .Include(p => p.Produto)
                .FirstOrDefaultAsync(m => m.IdFornecedor == id);
            if (produtoFornecedorReceb == null)
            {
                return NotFound();
            }

            return View(produtoFornecedorReceb);
        }

        // POST: ProdutoFornecedorRecebs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produtoFornecedorReceb = await _context.ProdutoFornecedorReceb.FindAsync(id);
            if (produtoFornecedorReceb != null)
            {
                _context.ProdutoFornecedorReceb.Remove(produtoFornecedorReceb);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoFornecedorRecebExists(int id)
        {
            return _context.ProdutoFornecedorReceb.Any(e => e.IdFornecedor == id);
        }
    }
}
