using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Control_Estoque.Data;
using Control_Estoque.Models;
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
    public class ProdutosController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ApplicationDbContext _context;

        public ProdutosController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Produtos
        [Authorize] // solo usuarios autenticados pueden crear productos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Produto.ToListAsync());

        }

        // GET: PDF Produtos
        [Authorize] // solo usuarios autenticados pueden crear productos
        public async Task<IActionResult> GetPdf()
        {
            var produtos = _context.Produto.Include(p => p.Cpf).ToList();
            var username = User.Identity?.Name;
            var currentUser = _context.Users.FirstOrDefault(u => u.Email == username);

            var viewFolderPath = Path.Combine(_environment.ContentRootPath, "wwwroot/images");
            var path = Path.Combine(viewFolderPath, "Produtos.pdf");
           


            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));
                    
                    
                    page.Header()
                    
                       .Text("Relatorio de Produtos ").SemiBold().FontSize(20).FontColor(Colors.Blue.Medium).AlignCenter();
                                       
                        page.Content().
                         Table(table =>
                         {
                             table.ColumnsDefinition(columns =>
                             {
                                 columns.RelativeColumn(2);
                                 columns.RelativeColumn(3);
                                 columns.RelativeColumn(2);
                                 columns.RelativeColumn(1);
                             });

                             table.Cell().ColumnSpan(4).Text("------------------------------").AlignCenter();
                             table.Cell().ColumnSpan(4).AlignCenter();
                             table.Cell().Background(Colors.Grey.Lighten3).Text("Produto").AlignCenter();
                             table.Cell().Background(Colors.Grey.Lighten3).Text("Descrição").AlignCenter();
                             table.Cell().Background(Colors.Grey.Lighten3).Text("Data Cadastro").AlignCenter();
                             table.Cell().Background(Colors.Grey.Lighten3).Text("UM").AlignCenter();

                             foreach (var p in produtos)
                            {
                                 //table.Cell().ColumnSpan(4).Text("Total width: 300px");
                                 table.Cell().Text(p.NomeProduto).FontSize(8);
                                 table.Cell().Text(p.Descrição).FontSize(8);
                                 table.Cell().Text(p.DataCadastroProd).FontSize(8);
                                 table.Cell().Text(p.UnidadeMedida).FontSize(8).AlignCenter();
                             }
                         });
                    

                    /*
                    page.Content().
                        Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(1);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(3);
                            columns.RelativeColumn(4);
                        });

                        table.Cell().ColumnSpan(4).Text("Total width: 300px");
                        table.Cell().Text("777");
                        table.Cell().Text("7777"); ;  
                        table.Cell().Text("7777"); ;
                        table.Cell().Text("7777"); ;
                    });

                   // separador

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(x =>
                        {
                            x.Spacing(12);
                            foreach (var p in produtos)
                            {
                                x.Item().Text(p.Produto.NomeProduto);
                            }
                           
                           // x.Item().Text(Placeholders.LoremIpsum());
                            x.Item().Image(Placeholders.Image(20, 10));
                        });
                        */
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
            return Redirect("../images/Produtos.pdf");
            //Response.Headers.Add("Content-Disposition", "attachment;  filename="+path);
          //  Response.ContentType = "application/pdf";


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
            //   ViewData["IdEstoque"] = new SelectList(_context.Estoque, "IdEstoque", "NomeEstoque");
            //  ViewData["NomeEstoque"] = new SelectList(_context.Estoque, "IdEstoque", "NomeEstoque");

            return View();
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize] // solo usuarios autenticados pueden crear productos
        public async Task<IActionResult> Create([Bind("CodProduto,NomeProduto,Descrição,EstoqueMinimo,EstoqueMaximo,ValidadeDias,UnidadeMedida")] Produto produto)
        {
            //var estoqueList = _context.Estoque.ToList();
            //ViewBag.IdEstoque = new SelectList(estoqueList,"NomeEstoque");

            ModelState.Remove("Cpf");
            ModelState.Remove("DataCadastroProd");
            if (ModelState.IsValid)
            {
                produto.CodProduto = produto.CodProduto;
                produto.NomeProduto = produto.NomeProduto.ToUpper();
                produto.Descrição = produto.Descrição.ToUpper();
                produto.UnidadeMedida = produto.UnidadeMedida.ToUpper();
                var username = User.Identity?.Name;
                var currentUser = _context.Users.FirstOrDefault(u => u.Email == username);
                produto.Cpf = currentUser ?? new();
                produto.DataCadastroProd = DateTime.Now;

                _context.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["EstoqueId"] = new SelectList(_context.Estoque, "IdEstoque", "NomeEstoque");
            return View(produto);
        }

        // GET: Produtos/Edit/5
        [Authorize] // solo usuarios autenticados pueden crear productos
        public async Task<IActionResult> Edit(int? id)
        {
            //ViewData["IdEstoque"] = new SelectList(_context.Estoque, "IdEstoque", "NomeEstoque");
            //  ViewData["NomeEstoque"] = new SelectList(_context.Estoque, "IdEstoque", "NomeEstoque");
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
        public async Task<IActionResult> Edit(int id, [Bind("CodProduto,NomeProduto,Descrição,EstoqueMinimo,EstoqueMaximo,ValidadeDias,UnidadeMedida")] Produto produto)
        {

            if (id != produto.CodProduto)
            {
                return NotFound();
            }

            ModelState.Remove("Cpf");
            ModelState.Remove("DataCadastroProd");
            ModelState.Remove("UnidadeMedida");

            if (ModelState.IsValid)
            {

                try
                {
                    produto.NomeProduto = produto.NomeProduto.ToUpper();
                    produto.Descrição = produto.Descrição.ToUpper();
                    produto.UnidadeMedida = produto.UnidadeMedida.ToUpper();
                    var username = User.Identity?.Name;
                    var currentUser = _context.Users.FirstOrDefault(u => u.Email == username);

                    produto.Cpf = currentUser ?? new();
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
