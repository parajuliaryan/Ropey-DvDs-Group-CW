#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ropey_DvDs_Group_CW.DBContext;
using Ropey_DvDs_Group_CW.Models;

namespace Ropey_DvDs_Group_CW.Controllers
{
    public class ActorsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public ActorsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Actors
        public async Task<IActionResult> Index()
        {
            return View(await _context.ActorModel.ToListAsync());
        }

        // GET: Actors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actorModel = await _context.ActorModel
                .FirstOrDefaultAsync(m => m.ActorNumber == id);
            if (actorModel == null)
            {
                return NotFound();
            }

            return View(actorModel);
        }

        // GET: Actors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Actors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActorNumber,ActorFirstName,ActorSurname")] ActorModel actorModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(actorModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(actorModel);
        }

        // GET: Actors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actorModel = await _context.ActorModel.FindAsync(id);
            if (actorModel == null)
            {
                return NotFound();
            }
            return View(actorModel);
        }

        // POST: Actors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ActorNumber,ActorFirstName,ActorSurname")] ActorModel actorModel)
        {
            if (id != actorModel.ActorNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(actorModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActorModelExists(actorModel.ActorNumber))
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
            return View(actorModel);
        }

        // GET: Actors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actorModel = await _context.ActorModel
                .FirstOrDefaultAsync(m => m.ActorNumber == id);
            if (actorModel == null)
            {
                return NotFound();
            }

            return View(actorModel);
        }

        // POST: Actors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actorModel = await _context.ActorModel.FindAsync(id);
            _context.ActorModel.Remove(actorModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActorModelExists(int id)
        {
            return _context.ActorModel.Any(e => e.ActorNumber == id);
        }
    }
}
