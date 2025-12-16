using EspacoTech.Areas.Identity.Data;
using EspacoTech.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EspacoTech.Controllers
{
    [Authorize]
    public class ReservaoController : Controller
    {
        private readonly EspacoTechLoginContext _context;
        private readonly UserManager<Usuario> _userManager;

        public ReservaoController(EspacoTechLoginContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Reservao
        public async Task<IActionResult> Index()
        {
            var espacoTechLoginContext = _context.Reservas
                .Include(r => r.UsuarioPerfil)
                .Include(r => r.Sala)
                .ToListAsync();

            return View(await espacoTechLoginContext);
        }

        // GET: Reservao/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.Sala)
                .Include(r => r.UsuarioPerfil)
                .FirstOrDefaultAsync(m => m.IdReserva == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // GET: Reservao/Create
        public IActionResult Create()
        {
            ViewData["IdSala"] = new SelectList(_context.Salas, "IdSala", "Descricao");
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Reservao/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdReserva,Data,Horario,IdSala,UsuarioId")] Reserva reserva)
        {

            var user = await _userManager.GetUserAsync(User);
            reserva.UsuarioId = user.Id;

            bool temConflito = await _context.Reservas.AnyAsync
                (predicate: r => r.IdSala == reserva.IdSala && r.Data.Date == reserva.Data.Date && r.Horario.Trim() == reserva.Horario.Trim());
            
            if (temConflito)
            {
                ModelState.AddModelError("Horario", "Este horário já está ocupado por outra reserva.");

            }
            try
            {
                _context.Add(reserva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {

            }
            
            ViewData["IdSala"] = new SelectList(_context.Salas, "IdSala", "Descricao", reserva.IdSala);
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "Id", reserva.UsuarioId);

            return View(reserva);
        }

        // GET: Reservao/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }
            ViewData["IdSala"] = new SelectList(_context.Salas, "IdSala", "Descricao", reserva.IdSala);
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "Id", reserva.UsuarioId);
            return View(reserva);
        }

        // POST: Reservao/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdReserva,Data,Horario,IdSala,UsuarioId")] Reserva reserva)
        {
            if (id != reserva.IdReserva)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaExists(reserva.IdReserva))
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
            ViewData["IdSala"] = new SelectList(_context.Salas, "IdSala", "Descricao", reserva.IdSala);
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "Id", reserva.UsuarioId);
            return View(reserva);
        }

        // GET: Reservao/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reservas
                .Include(r => r.Sala)
                .Include(r => r.UsuarioPerfil)
                .FirstOrDefaultAsync(m => m.IdReserva == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // POST: Reservao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva != null)
            {
                _context.Reservas.Remove(reserva);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservaExists(int id)
        {
            return _context.Reservas.Any(e => e.IdReserva == id);
        }
    }
}
