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
using System.Drawing.Text;

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
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.EstoqueProduto.Include(e => e.Estoque).Include(p => p.Produto);

            return View(await applicationDbContext.ToListAsync());


        }


        // GET: EstoqueProduto/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? Estoque, int? Produto)
        {
            var id_estoqueProduto = _context.EstoqueProduto.SingleOrDefault(e => e.EstoqueId == Estoque && e.CodProduto == Produto);

            if (Estoque == null || Produto == null)
            {
                return NotFound();
            }

            var estoqueProduto = await _context.EstoqueProduto
                .Include(e => e.Estoque)
                .Include(e => e.Produto)
                .FirstOrDefaultAsync(m => m.EstoqueId == Estoque && m.CodProduto == Produto);

            if (estoqueProduto == null)
            {
                return NotFound();
            }

            return View(estoqueProduto);
        }

        // GET: EstoqueProduto/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["EstoqueId"] = new SelectList(_context.Estoque, "IdEstoque", "NomeEstoque");
            ViewData["CodProduto"] = new SelectList(_context.Produto, "CodProduto", "NomeProduto");
            return View();
        }

        // POST: EstoqueProduto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("EstoqueId,CodProduto,Qtde")] EstoqueProduto estoqueProduto)
        {
            try
            {
                ViewData["EstoqueId"] = new SelectList(_context.Estoque, "IdEstoque", "NomeEstoque", estoqueProduto.EstoqueId);
                ViewData["CodProduto"] = new SelectList(_context.Produto, "CodProduto", "NomeProduto", estoqueProduto.CodProduto);

                ModelState.Remove("Estoque");
                ModelState.Remove("Produto");
                ModelState.Remove("Cpf");

                if (ModelState.IsValid)
                {
                    _context.Add(estoqueProduto);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }else
                {
                    return RedirectToAction(nameof(Index));
                }
                
            }catch(Exception ex)
            {
               Console.WriteLine(ex.ToString());
            }
                return View(estoqueProduto);
        }

        // GET: EstoqueProduto/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? Estoque, int? Produto)
        {
            if (Estoque == null || Produto == null)
            {
                return NotFound();
            }

            var estoqueProduto = await _context.EstoqueProduto.FindAsync(Estoque, Produto);

            if (estoqueProduto == null || Produto == null)
            {
                return NotFound();
            }
            ViewData["EstoqueId"] = new SelectList(_context.Estoque, "IdEstoque", "NomeEstoque", estoqueProduto.EstoqueId);
            ViewData["CodProduto"] = new SelectList(_context.Produto, "CodProduto", "NomeProduto", estoqueProduto.CodProduto);
            return View(estoqueProduto);
        }

        // POST: EstoqueProduto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int? EstoqueId, int? CodProduto, [Bind("EstoqueId,CodProduto,Qtde")] EstoqueProduto estoqueProduto)
        {
            //var id_estoqueProduto = _context.EstoqueProduto.SingleOrDefault(e => e.EstoqueId == Estoque && e.CodProduto == Produto);
            
            if (estoqueProduto.EstoqueId != EstoqueId || estoqueProduto.CodProduto != CodProduto)
            {
                return NotFound();
            }
            
            ModelState.Remove("Cpf");
            ModelState.Remove("Estoque");
            ModelState.Remove("Produto");

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
            ViewData["EstoqueId"] = new SelectList(_context.Estoque, "IdEstoque", "NomeEstoque", estoqueProduto.EstoqueId);
            ViewData["CodProduto"] = new SelectList(_context.Produto, "CodProduto", "NomeProduto", estoqueProduto.CodProduto);
         
            return View(estoqueProduto);
        }

        // GET: EstoqueProduto/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? Estoque, int? Produto)
        {
            if (Estoque == null || Produto == null)
            {
                return NotFound();
            }

            var estoqueProduto = await _context.EstoqueProduto
                .Include(e => e.Estoque)
                .Include(e => e.Produto)
                .FirstOrDefaultAsync(m => m.EstoqueId == Estoque && m.CodProduto == Produto);
            if (estoqueProduto == null)
            {
                return NotFound();
            }

            return View(estoqueProduto);
        }

        // POST: EstoqueProduto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
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
