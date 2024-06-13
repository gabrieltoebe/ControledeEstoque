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
    public class EstoquesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EstoquesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Estoques
        [Authorize] // solo usuarios autenticados pueden crear productos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Estoque.ToListAsync());
           // return View(estoqueComContagemDeProdutos);
        }

        // GET: Estoques/Details/5
        [Authorize] // solo usuarios autenticados pueden crear productos
        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            // Busca detalhes do estoque e os produtos associados
            var estoqueComProdutos = await _context.Estoque
                .Where(e => e.IdEstoque == Id)
                .Select(e => new EstoqueProdutosViewModel
                {
                    EstoqueId = e.IdEstoque,
                    NomeEstoque = e.NomeEstoque,
                    EstoqueAtivo = e.AtivEstoque,
                }).FirstOrDefaultAsync();

            if (estoqueComProdutos == null)
            {
                return NotFound();
            }

            return View(estoqueComProdutos);
        }

        // GET: Estoques/Create
        [Authorize] // solo usuarios autenticados pueden crear productos
        public IActionResult Create()
        {
            return View();
        }

        // POST: Estoques/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEstoque,NomeEstoque,TipoEstoque, AtivEstoque")] Estoque estoque)
        {
            ModelState.Remove("TipoEstoque");
            ModelState.Remove("Cpf");

            if (ModelState.IsValid)
            {
                estoque.NomeEstoque = estoque.NomeEstoque.ToUpper();
                estoque.TipoEstoque = 1;
                estoque.AtivEstoque = true;

                _context.Add(estoque);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Console.WriteLine("Erro ao gravar");
            }
            return View(estoque);
        }

        // GET: Estoques/Edit/5
        [Authorize] // solo usuarios autenticados pueden crear productos
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estoque = await _context.Estoque.FindAsync(id);
            if (estoque == null)
            {
                return NotFound();
            }
            return View(estoque);
        }

        // POST: Estoques/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize] // solo usuarios autenticados pueden crear productos
        public async Task<IActionResult> Edit(int id, [Bind("IdEstoque,NomeEstoque,TipoEstoque, AtivEstoque")] Estoque estoque)
        {
            ModelState.Remove("TipoEstoque");
            ModelState.Remove("Cpf");
            if (id != estoque.IdEstoque)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estoque);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstoqueExists(estoque.IdEstoque))
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
            return View(estoque);
        }

        // GET: Estoques/Delete/5
        [Authorize] // solo usuarios autenticados pueden crear productos
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estoque = await _context.Estoque
                .FirstOrDefaultAsync(m => m.IdEstoque == id);
            if (estoque == null)
            {
                return NotFound();
            }

            return View(estoque);
        }

        // POST: Estoques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize] // solo usuarios autenticados pueden crear productos
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estoque = await _context.Estoque.FindAsync(id);
            if (estoque != null && !estoque.AtivEstoque)
            {
                _context.Estoque.Remove(estoque);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
                TempData["ErrorMessage"] = $"O estoque {estoque.IdEstoque} está ativo ou contém produtos. Não será possível excluir.";
            return View(estoque);
        }

        private bool EstoqueExists(int id)
        {
            return _context.Estoque.Any(e => e.IdEstoque == id);
        }
    }
}
