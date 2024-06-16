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
using NuGet.Packaging.Signing;

namespace Control_Estoque.Controllers
{
    public class ProdutoFornecedorRecebsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private int quant;

        public ProdutoFornecedorRecebsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProdutoFornecedorRecebs
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProdutoFornecedorReceb.Include(p => p.Fornecedor).Include(p => p.Produto);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProdutoFornecedorRecebs/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtoFornecedorReceb = await _context.ProdutoFornecedorReceb
                .Include(p => p.Fornecedor)
                .Include(p => p.Produto)
                .Include(p => p.Cpf)
                .FirstOrDefaultAsync(m => m.IdProdFornRec == id);
            if (produtoFornecedorReceb == null)
            {
                return NotFound();
            }

            return View(produtoFornecedorReceb);
        }

        // GET: ProdutoFornecedorRecebs/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["IdFornecedor"] = new SelectList(_context.Fornecedor, "IdFornecedor", "NomeFornecedor");
            ViewData["CodProduto"] = new SelectList(_context.Produto, "CodProduto", "NomeProduto");
            ViewData["IdEstoque"] = new SelectList(_context.Estoque, "IdEstoque", "NomeEstoque");
            return View();
        }

        // POST: ProdutoFornecedorRecebs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("IdFornecedor,CodProduto,IdEstoque,Qtde")] ProdutoFornecedorReceb produtoFornecedorReceb)
        {
            ModelState.Remove("Cpf");
            ModelState.Remove("DataRecebimento");
            ModelState.Remove("Produto");
            ModelState.Remove("Fornecedor");
            ModelState.Remove("Estoque");

            if (ModelState.IsValid)
            {
                //var username = User.Identity?.Name;
                //var currentUser = _context.Users.FirstOrDefault(u => u.Email == username);
                //var produtoIdEstoque = _context.Produto.FirstOrDefault(pid => pid.CodProduto == produtoFornecedorReceb.CodProduto);

                //produtoFornecedorReceb.Cpf = currentUser ?? new();
                produtoFornecedorReceb.DataRecebimento = DateTime.Now;

                var username = User.Identity?.Name;
                var currentUser = _context.Users.FirstOrDefault(u => u.Email == username);
                produtoFornecedorReceb.Cpf = currentUser ?? new();
                             

                var produto = await _context.Produto.FindAsync(produtoFornecedorReceb.CodProduto);
                produtoFornecedorReceb.Produto = produto;

                var fornecedor = await _context.Fornecedor.FindAsync(produtoFornecedorReceb.IdFornecedor);
                produtoFornecedorReceb.Fornecedor = fornecedor;


                var estoque = await _context.Estoque.FindAsync(produtoFornecedorReceb.IdEstoque);
                produtoFornecedorReceb.Estoque = estoque;
                EstoqueProduto produtoBuscado = _context.EstoqueProduto.Find(produtoFornecedorReceb.IdEstoque, produto.CodProduto);
                // var estoqueProduto = await _context.EstoqueProduto.FindAsync(inventario.Estoque, produto);

                //var quant = produtoBuscado.Qtde;
                // int soma = quant + produtoFornecedorReceb.Qtde;
                // produtoBuscado.Qtde = soma;

                //var produtoBuscado = _context.EstoqueProduto.Find(produtoFornecedorReceb.IdEstoque, produto.CodProduto);
                //var comparapord = produtoBuscado2.Equals(Empty);

                if (produtoBuscado == null)
                {
                    EstoqueProduto estoqueProduto = new EstoqueProduto()
                    {
                        Estoque=estoque,
                        Produto=produto,
                        CodProduto=produtoFornecedorReceb.CodProduto,
                        EstoqueId=produtoFornecedorReceb.IdEstoque,
                        Qtde=0
                    };

                    produtoBuscado = estoqueProduto;
                    _context.Add(estoqueProduto);
                    await _context.SaveChangesAsync();
                    
                 }
               

                   int quant = produtoBuscado.Qtde;
                   int soma = quant + produtoFornecedorReceb.Qtde;
                   produtoBuscado.Qtde = soma;

                   _context.Update(produtoBuscado);
                   await _context.SaveChangesAsync();                    
               
                            
                
                                
                /*
                var comparapord = produtoBuscado2.Qtde;

                if (comparapord != null)
                {
                    var quant = produtoBuscado2.Qtde;
                    int soma = quant + produtoFornecedorReceb.Qtde;
                    produtoBuscado2.Qtde = soma;
                }
                else
                {
                    // Caso a quantidade seja nula, não realizamos a soma
                    produtoBuscado2.Qtde = produtoFornecedorReceb.Qtde;
                }
                */
                
                  // produtoFornecedorReceb.IdEstoque = 10; //produtoIdEstoque.IdEstoque;

                _context.Add(produtoFornecedorReceb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdFornecedor"] = new SelectList(_context.Fornecedor, "IdFornecedor", "NomeFornecedor");
            ViewData["CodProduto"] = new SelectList(_context.Produto, "CodProduto", "NomeProduto");
            ViewData["IdEstoque"] = new SelectList(_context.Estoque, "IdEstoque", "NomeEstoque");
            return View(produtoFornecedorReceb);
        }

        // GET: ProdutoFornecedorRecebs/Edit/5
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
