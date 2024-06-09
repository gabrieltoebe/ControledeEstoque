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
    public class ProdutoEstoqueController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProdutoEstoqueController(ApplicationDbContext context)
        {
            _context = context;
        }

        

        

        // GET: ProdutoEstoque
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.EstoqueProduto.Include(e => e.Estoque).Include(p => p.Produto);

            return View(await applicationDbContext.ToListAsync());


        }


        
    }
}
