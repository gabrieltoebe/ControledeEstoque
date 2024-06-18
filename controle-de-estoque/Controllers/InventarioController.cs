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
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
//using Control_Estoque.Data.Migrations;
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
    public class InventarioController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ApplicationDbContext _context;
        private string AtEstoq;

        public InventarioController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: PDF Inventario
        [Authorize] // solo usuarios autenticados pueden crear productos
        public async Task<IActionResult> GetPdf()
        {
            var produtos = _context.Inventario.Include(p => p.Estoque).ToList();
            var username = User.Identity?.Name;
            var currentUser = _context.Users.FirstOrDefault(u => u.Email == username);

            var viewFolderPath = Path.Combine(_environment.ContentRootPath, "wwwroot/images");
            var path = Path.Combine(viewFolderPath, "Inventarios.pdf");



            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));


                    page.Header()

                       .Text("Relatorio de Inventários ").SemiBold().FontSize(20).FontColor(Colors.Blue.Medium).AlignCenter();

                    page.Content().
                     Table(table =>
                     {
                         table.ColumnsDefinition(columns =>
                         {
                             columns.RelativeColumn();
                             columns.RelativeColumn();
                             columns.RelativeColumn();
                             
                         });

                         table.Cell().ColumnSpan(3).Text("------------------------------").AlignCenter();
                         table.Cell().ColumnSpan(3).AlignCenter();
                         //nome das colunas
                         table.Cell().Background(Colors.Grey.Lighten3).Text("Data Inventário").AlignCenter();
                         table.Cell().Background(Colors.Grey.Lighten3).Text("Estoque").AlignCenter();
                         table.Cell().Background(Colors.Grey.Lighten3).Text("Responsável").AlignCenter();
                         


                         foreach (var p in produtos)
                         {
                             //table.Cell().ColumnSpan(4).Text("Total width: 300px");
                             table.Cell().Text(p.DataMov).FontSize(8);
                             table.Cell().Text(p.Estoque.NomeEstoque).AlignCenter().FontSize(8);
                             table.Cell().AlignCenter().Text(p.Cpf).FontSize(8);

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
            return Redirect("../images/Inventarios.pdf");
            //Response.Headers.Add("Content-Disposition", "attachment;  filename="+path);
            //  Response.ContentType = "application/pdf";


        }






        // GET: Inventario
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Inventario.Include(p => p.Estoque);
            ViewData["IdEstoque"] = new SelectList(_context.Estoque, "IdEstoque", "IdEstoque");
            return View(await applicationDbContext.ToListAsync());
            //return View(await _context.Inventario.ToListAsync());
        }

        // GET: Inventario/Details/5 
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventario = await _context.Inventario
                .FirstOrDefaultAsync(m => m.IdInv == id);
            if (inventario == null)
            {
                return NotFound();
            }

            return View(inventario);
        }

        // GET: Inventario/Create 
        [Authorize]
        public IActionResult Create()
        {
            ViewData["IdEstoque"] = new SelectList(_context.Estoque, "IdEstoque", "NomeEstoque");
            ViewData["TipoMov"] = Enum.GetValues(typeof(TipoMov));
            return View();
        }

        // POST: Inventario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("IdInv,Cpf,IdEstoque,DataMov")] Inventario inventario)
        {
            ModelState.Remove("IdInv");
            ModelState.Remove("Cpf");

            if (ModelState.IsValid)
            {
                var username = User.Identity?.Name;
                var currentUser = _context.Users.FirstOrDefault(u => u.Email == username);
                inventario.Cpf = currentUser ?? new();
                //inventario.IdEstoque = IdEstoque;

                _context.Add(inventario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inventario);
        }

        // GET: Inventario/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventario = await _context.Inventario.FindAsync(id);
            if (inventario == null)
            {
                return NotFound();
            }
            return View(inventario);
        }

        // POST: Inventario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("IdInv,IdMovimento,IdEstoque,DataMov")] Inventario inventario)
        {
            if (id != inventario.IdInv)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventarioExists(inventario.IdInv))
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
            return View(inventario);
        }

        // GET: Inventario/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventario = await _context.Inventario
                .FirstOrDefaultAsync(m => m.IdInv == id);
            if (inventario == null)
            {
                return NotFound();
            }

            return View(inventario);
        }

        // POST: Inventario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inventario = await _context.Inventario.FindAsync(id);
            if (inventario != null)
            {
                _context.Inventario.Remove(inventario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventarioExists(int id)
        {
            return _context.Inventario.Any(e => e.IdInv == id);
        }
    }
}
