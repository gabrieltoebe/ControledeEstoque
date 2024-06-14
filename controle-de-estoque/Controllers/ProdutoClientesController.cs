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
    public class ProdutoClientesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProdutoClientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProdutoClientes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProdutoCliente.Include(p => p.Cliente).Include(p => p.Estoque).Include(p => p.Produto);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProdutoClientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtoCliente = await _context.ProdutoCliente
                .Include(p => p.Cliente)
                .Include(p => p.Estoque)
                .Include(p => p.Produto)
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            if (produtoCliente == null)
            {
                return NotFound();
            }

            return View(produtoCliente);
        }

        // GET: ProdutoClientes/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Cliente, "IdCliente", "IdCliente");
            ViewData["IdEstoque"] = new SelectList(_context.Estoque, "IdEstoque", "IdEstoque");
            ViewData["CodProduto"] = new SelectList(_context.Produto, "CodProduto", "CodProduto");
            return View();
        }

        // POST: ProdutoClientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProdFornRec,IdCliente,CodProduto,IdEstoque,Qtde,DataRecebimento")] ProdutoCliente produtoCliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produtoCliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Cliente, "IdCliente", "IdCliente", produtoCliente.IdCliente);
            ViewData["IdEstoque"] = new SelectList(_context.Estoque, "IdEstoque", "IdEstoque", produtoCliente.IdEstoque);
            ViewData["CodProduto"] = new SelectList(_context.Produto, "CodProduto", "CodProduto", produtoCliente.CodProduto);
            return View(produtoCliente);
        }

        // GET: ProdutoClientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtoCliente = await _context.ProdutoCliente.FindAsync(id);
            if (produtoCliente == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Cliente, "IdCliente", "IdCliente", produtoCliente.IdCliente);
            ViewData["IdEstoque"] = new SelectList(_context.Estoque, "IdEstoque", "IdEstoque", produtoCliente.IdEstoque);
            ViewData["CodProduto"] = new SelectList(_context.Produto, "CodProduto", "CodProduto", produtoCliente.CodProduto);
            return View(produtoCliente);
        }

        // POST: ProdutoClientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProdFornRec,IdCliente,CodProduto,IdEstoque,Qtde,DataRecebimento")] ProdutoCliente produtoCliente)
        {
            if (id != produtoCliente.IdCliente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produtoCliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoClienteExists(produtoCliente.IdCliente))
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
            ViewData["IdCliente"] = new SelectList(_context.Cliente, "IdCliente", "IdCliente", produtoCliente.IdCliente);
            ViewData["IdEstoque"] = new SelectList(_context.Estoque, "IdEstoque", "IdEstoque", produtoCliente.IdEstoque);
            ViewData["CodProduto"] = new SelectList(_context.Produto, "CodProduto", "CodProduto", produtoCliente.CodProduto);
            return View(produtoCliente);
        }

        // GET: ProdutoClientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtoCliente = await _context.ProdutoCliente
                .Include(p => p.Cliente)
                .Include(p => p.Estoque)
                .Include(p => p.Produto)
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            if (produtoCliente == null)
            {
                return NotFound();
            }

            return View(produtoCliente);
        }

        // POST: ProdutoClientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produtoCliente = await _context.ProdutoCliente.FindAsync(id);
            if (produtoCliente != null)
            {
                _context.ProdutoCliente.Remove(produtoCliente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoClienteExists(int id)
        {
            return _context.ProdutoCliente.Any(e => e.IdCliente == id);
        }
    }
}
