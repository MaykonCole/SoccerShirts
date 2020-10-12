using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoccerShirts.Context;
using SoccerShirts.Models;

namespace SoccerShirts.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminCamisasController : Controller
    {
        private readonly AppDbContext _context;

        public AdminCamisasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminCamisas
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Camisas.Include(c => c.Categoria);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/AdminCamisas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var camisa = await _context.Camisas
                .Include(c => c.Categoria)
                .FirstOrDefaultAsync(m => m.CamisaId == id);
            if (camisa == null)
            {
                return NotFound();
            }

            return View(camisa);
        }

        // GET: Admin/AdminCamisas/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaId");
            return View();
        }

        // POST: Admin/AdminCamisas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CamisaId,Nome,DescricaoCurta,DescricaoDestalhada,Preco,ImagemUrl,ImagemMiniatura,IsCamisaMaisVendida,EmEstoque,CategoriaId")] Camisa camisa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(camisa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaId", camisa.CategoriaId);
            return View(camisa);
        }

        // GET: Admin/AdminCamisas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var camisa = await _context.Camisas.FindAsync(id);
            if (camisa == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaId", camisa.CategoriaId);
            return View(camisa);
        }

        // POST: Admin/AdminCamisas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CamisaId,Nome,DescricaoCurta,DescricaoDestalhada,Preco,ImagemUrl,ImagemMiniatura,IsCamisaMaisVendida,EmEstoque,CategoriaId")] Camisa camisa)
        {
            if (id != camisa.CamisaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(camisa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CamisaExists(camisa.CamisaId))
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
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaId", camisa.CategoriaId);
            return View(camisa);
        }

        // GET: Admin/AdminCamisas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var camisa = await _context.Camisas
                .Include(c => c.Categoria)
                .FirstOrDefaultAsync(m => m.CamisaId == id);
            if (camisa == null)
            {
                return NotFound();
            }

            return View(camisa);
        }

        // POST: Admin/AdminCamisas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var camisa = await _context.Camisas.FindAsync(id);
            _context.Camisas.Remove(camisa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CamisaExists(int id)
        {
            return _context.Camisas.Any(e => e.CamisaId == id);
        }
    }
}
