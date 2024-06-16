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
using System.Threading.Tasks;
using QuestPDF;
using QuestPDF.Drawing;
using QuestPDF.Elements;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Web;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using QuestPDF.Previewer;

namespace Control_Estoque.Controllers
{
    public class EstoquesController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ApplicationDbContext _context;
        private string AtEstoq;

        public EstoquesController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
            
        }

        // GET: PDF Estoques
        [Authorize] // solo usuarios autenticados pueden crear productos
        public async Task<IActionResult> GetPdf()
        {
            var produtos = _context.Estoque.Include(p => p.EstoqueProdutos).ToList();
            var username = User.Identity?.Name;
            var currentUser = _context.Users.FirstOrDefault(u => u.Email == username);

            var viewFolderPath = Path.Combine(_environment.ContentRootPath, "wwwroot/images");
            var path = Path.Combine(viewFolderPath, "Estoques.pdf");



            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));


                    page.Header()

                       .Text("Relatorio de Estoques ").SemiBold().FontSize(20).FontColor(Colors.Blue.Medium).AlignCenter();

                    page.Content().
                     Table(table =>
                     {
                     table.ColumnsDefinition(columns =>
                     {
                         columns.RelativeColumn(3);
                         columns.RelativeColumn(2);
                         columns.RelativeColumn(1);

                     });

                     table.Cell().ColumnSpan(3).Text("------------------------------").AlignCenter();
                     table.Cell().ColumnSpan(3).AlignCenter();
                     table.Cell().Background(Colors.Grey.Lighten3).Text("Estoque").AlignCenter();
                     table.Cell().Background(Colors.Grey.Lighten3).Text("Status").AlignCenter();
                     table.Cell().Background(Colors.Grey.Lighten3).Text("Produtos no Estoque").AlignCenter();


                     foreach (var p in produtos)
                     {
                         //table.Cell().ColumnSpan(4).Text("Total width: 300px");
                         table.Cell().Text(p.NomeEstoque).FontSize(8);
                         if (p.AtivEstoque == true) { AtEstoq = "Ativo"; }else{ AtEstoq = "Desativado"; };
                             table.Cell().Text(AtEstoq).AlignCenter().FontSize(8);
                             table.Cell().AlignCenter().Text(p.EstoqueProdutos.Count).FontSize(8);
                             
                         }
                     });

       
                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Pagina ").FontSize(6);
                            x.CurrentPageNumber().FontSize(6);
                            x.Line("").FontSize(6);
                            x.Line(DateTime.Now.ToString()).FontSize(6);
                            x.Span("_Impreso por: ").FontSize(6);
                            x.Line(currentUser.UserName).FontSize(6);
                        });
                });
            }).GeneratePdf(path);
            return Redirect("../images/Estoques.pdf");
            //Response.Headers.Add("Content-Disposition", "attachment;  filename="+path);
            //  Response.ContentType = "application/pdf";


        }











        // GET: Estoques
        [Authorize] // solo usuarios autenticados pueden crear productos
        public async Task<IActionResult> Index()
        {

            //return View(await _context.Estoque.ToListAsync());

            var estoques = await _context.Estoque
                .Include(e => e.EstoqueProdutos) // Include EstoqueProduto collection
                .ToListAsync();

           
            return View(estoques);
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
