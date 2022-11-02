using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DALcrudInstituto.Models;

namespace crudInstituto.Controllers
{
    public class AlumnHasAsigsController : Controller
    {
        private readonly ejemploDBFirstContext _context;

        public AlumnHasAsigsController(ejemploDBFirstContext context)
        {
            _context = context;
        }

        // GET: AlumnHasAsigs
        public async Task<IActionResult> Index()
        {
            var ejemploDBFirstContext = _context.AlumnHasAsigs.Include(a => a.IdAlumnoNavigation).Include(a => a.IdAsignaturaNavigation);
            return View(await ejemploDBFirstContext.ToListAsync());
        }

        // GET: AlumnHasAsigs/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.AlumnHasAsigs == null)
            {
                return NotFound();
            }

            var alumnHasAsig = await _context.AlumnHasAsigs
                .Include(a => a.IdAlumnoNavigation)
                .Include(a => a.IdAsignaturaNavigation)
                .FirstOrDefaultAsync(m => m.IdRel == id);
            if (alumnHasAsig == null)
            {
                return NotFound();
            }

            return View(alumnHasAsig);
        }

        // GET: AlumnHasAsigs/Create
        public IActionResult Create()
        {
            ViewData["IdAlumno"] = new SelectList(_context.Alumnos, "IdAlumno", "Apellidos");
            ViewData["IdAsignatura"] = new SelectList(_context.Asignaturas, "IdAsignatura", "Nombre");
            return View();
        }

        // POST: AlumnHasAsigs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRel,IdAlumno,IdAsignatura")] AlumnHasAsig alumnHasAsig)
        {
            if (ModelState.IsValid)
            {
                _context.Add(alumnHasAsig);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAlumno"] = new SelectList(_context.Alumnos, "IdAlumno", "Apellidos", alumnHasAsig.IdAlumno);
            ViewData["IdAsignatura"] = new SelectList(_context.Asignaturas, "IdAsignatura", "Nombre", alumnHasAsig.IdAsignatura);
            return View(alumnHasAsig);
        }

        // GET: AlumnHasAsigs/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.AlumnHasAsigs == null)
            {
                return NotFound();
            }

            var alumnHasAsig = await _context.AlumnHasAsigs.FindAsync(id);
            if (alumnHasAsig == null)
            {
                return NotFound();
            }
            ViewData["IdAlumno"] = new SelectList(_context.Alumnos, "IdAlumno", "Apellidos", alumnHasAsig.IdAlumno);
            ViewData["IdAsignatura"] = new SelectList(_context.Asignaturas, "IdAsignatura", "Nombre", alumnHasAsig.IdAsignatura);
            return View(alumnHasAsig);
        }

        // POST: AlumnHasAsigs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("IdRel,IdAlumno,IdAsignatura")] AlumnHasAsig alumnHasAsig)
        {
            if (id != alumnHasAsig.IdRel)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alumnHasAsig);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlumnHasAsigExists(alumnHasAsig.IdRel))
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
            ViewData["IdAlumno"] = new SelectList(_context.Alumnos, "IdAlumno", "Apellidos", alumnHasAsig.IdAlumno);
            ViewData["IdAsignatura"] = new SelectList(_context.Asignaturas, "IdAsignatura", "Nombre", alumnHasAsig.IdAsignatura);
            return View(alumnHasAsig);
        }

        // GET: AlumnHasAsigs/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.AlumnHasAsigs == null)
            {
                return NotFound();
            }

            var alumnHasAsig = await _context.AlumnHasAsigs
                .Include(a => a.IdAlumnoNavigation)
                .Include(a => a.IdAsignaturaNavigation)
                .FirstOrDefaultAsync(m => m.IdRel == id);
            if (alumnHasAsig == null)
            {
                return NotFound();
            }

            return View(alumnHasAsig);
        }

        // POST: AlumnHasAsigs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.AlumnHasAsigs == null)
            {
                return Problem("Entity set 'ejemploDBFirstContext.AlumnHasAsigs'  is null.");
            }
            var alumnHasAsig = await _context.AlumnHasAsigs.FindAsync(id);
            if (alumnHasAsig != null)
            {
                _context.AlumnHasAsigs.Remove(alumnHasAsig);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlumnHasAsigExists(long id)
        {
          return _context.AlumnHasAsigs.Any(e => e.IdRel == id);
        }
    }
}
