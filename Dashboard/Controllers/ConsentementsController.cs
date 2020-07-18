using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Model;

namespace Dashboard.Controllers
{
    public class ConsentementsController : Controller
    {
        private readonly ReminderContext _context;

        public ConsentementsController(ReminderContext context)
        {
            _context = context;
        }

        // GET: Consentements
        public async Task<IActionResult> Index()
        {
            return View(await _context.Consentements.ToListAsync());
        }

        // GET: Consentements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consentement = await _context.Consentements
                .FirstOrDefaultAsync(m => m.ID == id);
            if (consentement == null)
            {
                return NotFound();
            }

            return View(consentement);
        }

        // GET: Consentements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Consentements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Matricule,Nom,Tel,Mail")] Consentement consentement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consentement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(consentement);
        }

        // GET: Consentements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consentement = await _context.Consentements.FindAsync(id);
            if (consentement == null)
            {
                return NotFound();
            }
            return View(consentement);
        }

        // POST: Consentements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Matricule,Nom,Tel,Mail")] Consentement consentement)
        {
            if (id != consentement.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consentement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsentementExists(consentement.ID))
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
            return View(consentement);
        }

        // GET: Consentements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consentement = await _context.Consentements
                .FirstOrDefaultAsync(m => m.ID == id);
            if (consentement == null)
            {
                return NotFound();
            }

            return View(consentement);
        }

        // POST: Consentements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consentement = await _context.Consentements.FindAsync(id);
            _context.Consentements.Remove(consentement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsentementExists(int id)
        {
            return _context.Consentements.Any(e => e.ID == id);
        }
    }
}
