﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Control_Estoque.Data;
using Control_Estoque.Models;

namespace Control_Estoque.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProdutosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Produtos
        [Authorize] // solo usuarios autenticados pueden crear productos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Produto.ToListAsync());
        }

        // GET: Produtos/Details/5
        [Authorize] // solo usuarios autenticados pueden crear productos
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto
                .FirstOrDefaultAsync(m => m.CodProduto == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // GET: Produtos/Create
        [Authorize] // solo usuarios autenticados pueden crear productos
        public IActionResult Create()
        {
            return View();
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize] // solo usuarios autenticados pueden crear productos
        public async Task<IActionResult> Create([Bind("CodProduto,NomeProduto,Descrição")] Produto produto)
        {
            ModelState.Remove("Cpf");
            ModelState.Remove("DataCadastroProd");
            if (ModelState.IsValid)
            {
                var username = User.Identity.Name;
                var currentUser = _context.Users.FirstOrDefault(u => u.Email == username);
                produto.Cpf = currentUser;
                produto.DataCadastroProd = DateTime.Now;

                _context.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        // GET: Produtos/Edit/5
        [Authorize] // solo usuarios autenticados pueden crear productos
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize] // solo usuarios autenticados pueden crear productos
        public async Task<IActionResult> Edit(int id, [Bind("CodProduto,NomeProduto,Descrição")] Produto produto)
        {
            
            if (id != produto.CodProduto)
            {
                return NotFound();
            }

            ModelState.Remove("Cpf");
            ModelState.Remove("DataCadastroProd");
            if (ModelState.IsValid)
            {

                try
                {
                    var username = User.Identity.Name;
                    var currentUser = _context.Users.FirstOrDefault(u => u.Email == username);
                    produto.Cpf = currentUser;
                    produto.DataCadastroProd = DateTime.Now;

                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.CodProduto))
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
            return View(produto);
        }

        // GET: Produtos/Delete/5
        [Authorize] // solo usuarios autenticados pueden crear productos
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto
                .FirstOrDefaultAsync(m => m.CodProduto == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize] // solo usuarios autenticados pueden crear productos~
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = await _context.Produto.FindAsync(id);
            if (produto != null)
            {
                _context.Produto.Remove(produto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produto.Any(e => e.CodProduto == id);
        }
    }
}
