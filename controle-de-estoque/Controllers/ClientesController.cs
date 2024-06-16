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
    public class ClientesController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ApplicationDbContext _context;
        private string AtEstoq;

        public ClientesController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: PDF Clientes
        [Authorize] // solo usuarios autenticados pueden crear productos
        public async Task<IActionResult> GetPdf()
        {
            var produtos = _context.Cliente.Include(p => p.ProdutoCliente).ToList();
            var username = User.Identity?.Name;
            var currentUser = _context.Users.FirstOrDefault(u => u.Email == username);

            var viewFolderPath = Path.Combine(_environment.ContentRootPath, "wwwroot/images");
            var path = Path.Combine(viewFolderPath, "Clientes.pdf");



            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));


                    page.Header()

                       .Text("Relatorio de Clientes ").SemiBold().FontSize(20).FontColor(Colors.Blue.Medium).AlignCenter();

                    page.Content().
                     Table(table =>
                     {
                         table.ColumnsDefinition(columns =>
                         {
                             columns.RelativeColumn();
                             columns.RelativeColumn();
                             columns.RelativeColumn();
                             columns.RelativeColumn();

                         });

                         table.Cell().ColumnSpan(4).Text("------------------------------").AlignCenter();
                         table.Cell().ColumnSpan(4).AlignCenter();
                         //nome das colunas
                         table.Cell().Background(Colors.Grey.Lighten3).Text("Cliente").AlignCenter();
                         table.Cell().Background(Colors.Grey.Lighten3).Text("Estado").AlignCenter();
                         table.Cell().Background(Colors.Grey.Lighten3).Text("Telefone").AlignCenter();
                         table.Cell().Background(Colors.Grey.Lighten3).Text("Endereço").AlignCenter();


                         foreach (var p in produtos)
                         {
                             //table.Cell().ColumnSpan(4).Text("Total width: 300px");
                             table.Cell().Text(p.NomeCliente).FontSize(8);
                             table.Cell().Text(p.EstadoCliente).AlignCenter().FontSize(8);
                             table.Cell().AlignCenter().Text(p.TelefoneCliente).FontSize(8);
                             table.Cell().Text(p.EnderecoCliente).AlignCenter().FontSize(8);

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
            return Redirect("../images/Clientes.pdf");
            //Response.Headers.Add("Content-Disposition", "attachment;  filename="+path);
            //  Response.ContentType = "application/pdf";


        }




        // GET: Clientes
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cliente.ToListAsync());
        }

        // GET: Clientes/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCliente,NomeCliente,EstadoCliente,TelefoneCliente,EnderecoCliente")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCliente,NomeCliente,EstadoCliente,TelefoneCliente,EnderecoCliente")] Cliente cliente)
        {
            if (id != cliente.IdCliente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.IdCliente))
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
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente != null)
            {
                _context.Cliente.Remove(cliente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _context.Cliente.Any(e => e.IdCliente == id);
        }
    }
}
