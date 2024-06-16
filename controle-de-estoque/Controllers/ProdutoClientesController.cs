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
        private int quant;

        public ProdutoClientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProdutoClientes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProdutoCliente.Include(p => p.Cliente).Include(p => p.Estoque).Include(p => p.Produto);
            ViewData["IdCliente"] = new SelectList(_context.Cliente, "IdCliente", "Cliente");
            ViewData["IdEstoque"] = new SelectList(_context.Estoque, "IdEstoque", "Estoque");
            ViewData["CodProduto"] = new SelectList(_context.Produto, "CodProduto", "Produto");
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
                .Include(p => p.Cpf)
                .Include(p => p.Produto)
                .Include(p => p.Estoque)
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            if (produtoCliente == null)
            {
                return NotFound();
            }

            ViewData["IdCliente"] = new SelectList(_context.Cliente, "IdCliente", "Cliente");
            ViewData["IdEstoque"] = new SelectList(_context.Estoque, "IdEstoque", "Estoque");
            ViewData["CodProduto"] = new SelectList(_context.Produto, "CodProduto", "Produto");
            return View(produtoCliente);
        }

        // GET: ProdutoClientes/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Cliente, "IdCliente", "NomeCliente");
            ViewData["IdEstoque"] = new SelectList(_context.Estoque, "IdEstoque", "NomeEstoque");
            ViewData["CodProduto"] = new SelectList(_context.Produto, "CodProduto", "NomeProduto");
            return View();
        }

        // POST: ProdutoClientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProdFornRec,IdCliente,CodProduto,IdEstoque,Qtde,DataRecebimento")] ProdutoCliente produtoCliente)
        {
            ModelState.Remove("Cpf");
            ModelState.Remove("DataRecebimento");
            ModelState.Remove("Produto");
            ModelState.Remove("Cliente");
            ModelState.Remove("Estoque");

            if (ModelState.IsValid)
            {
                //var username = User.Identity?.Name;
                //var currentUser = _context.Users.FirstOrDefault(u => u.Email == username);
                //var produtoIdEstoque = _context.Produto.FirstOrDefault(pid => pid.CodProduto ==produtoCliente.CodProduto);

                //produtoFornecedorReceb.Cpf = currentUser ?? new();
               produtoCliente.DataRecebimento = DateTime.Now;

                var username = User.Identity?.Name;
                var currentUser = _context.Users.FirstOrDefault(u => u.Email == username);
               produtoCliente.Cpf = currentUser ?? new();


                var produto = await _context.Produto.FindAsync(produtoCliente.CodProduto);
               produtoCliente.Produto = produto;

                var cliente = await _context.Cliente.FindAsync(produtoCliente.IdCliente);
               produtoCliente.Cliente = cliente;


                var estoque = await _context.Estoque.FindAsync(produtoCliente.IdEstoque);
               produtoCliente.Estoque = estoque;

                produtoCliente.DataRecebimento = System.DateTime.Now;
                EstoqueProduto produtoBuscado = _context.EstoqueProduto.Find(produtoCliente.IdEstoque, produto.CodProduto);
                // var estoqueProduto = await _context.EstoqueProduto.FindAsync(inventario.Estoque, produto);

                //var quant = produtoBuscado.Qtde;
                // int soma = quant +produtoCliente.Qtde;
                // produtoBuscado.Qtde = soma;

                //var produtoBuscado = _context.EstoqueProduto.Find(produtoFornecedorReceb.IdEstoque, produto.CodProduto);
                //var comparapord = produtoBuscado2.Equals(Empty);

                if(produtoBuscado==null)
                {
                    ModelState.AddModelError("IdEstoque", "Produto indisponível no estoque selecionado");
                    ViewData["IdCliente"] = new SelectList(_context.Cliente, "IdCliente", "IdCliente", produtoCliente.IdCliente);
                    ViewData["IdEstoque"] = new SelectList(_context.Estoque, "IdEstoque", "IdEstoque", produtoCliente.IdEstoque);
                    ViewData["CodProduto"] = new SelectList(_context.Produto, "CodProduto", "CodProduto", produtoCliente.CodProduto);
                    return View(produtoCliente);
                }


                int quant = produtoBuscado.Qtde;
                //int soma = quant + produtoCliente.Qtde;
                //produtoBuscado.Qtde = soma;


                if (produtoCliente.Qtde <= quant)
                {
                    int resta = quant - produtoCliente.Qtde;
                    produtoBuscado.Qtde = resta;
                    _context.Update(produtoBuscado);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    // ModelState.AddModelError()
                    ModelState.AddModelError("Quantidade", "Quantidade Indisponível para Saída");
                    ViewData["IdCliente"] = new SelectList(_context.Cliente, "IdCliente", "IdCliente");
                    ViewData["IdEstoque"] = new SelectList(_context.Estoque, "IdEstoque", "IdEstoque");
                    ViewData["CodProduto"] = new SelectList(_context.Produto, "CodProduto", "CodProduto");
                    return View(produtoCliente);
                }


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
