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
    public class CastMembersController : Controller
    {
        private readonly ApplicationDBContext _context;

        public CastMembersController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: CastMembers
        public async Task<IActionResult> Index()
        {
            var applicationDBContext = _context.CastMemberModel.Include(c => c.ActorModel).Include(c => c.DVDTitleModel);
            return View(await applicationDBContext.ToListAsync());
        }

        // GET: CastMembers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var castMemberModel = await _context.CastMemberModel
                .Include(c => c.ActorModel)
                .Include(c => c.DVDTitleModel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (castMemberModel == null)
            {
                return NotFound();
            }

            return View(castMemberModel);
        }

        // GET: CastMembers/Create
        public IActionResult Create()
        {
            ViewData["ActorNumber"] = new SelectList(_context.ActorModel, "ActorNumber", "ActorNumber");
            ViewData["DVDNumber"] = new SelectList(_context.DVDTitleModel, "DVDNumber", "DVDNumber");
            return View();
        }

        // POST: CastMembers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DVDNumber,ActorNumber")] CastMemberModel castMemberModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(castMemberModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActorNumber"] = new SelectList(_context.ActorModel, "ActorNumber", "ActorNumber", castMemberModel.ActorNumber);
            ViewData["DVDNumber"] = new SelectList(_context.DVDTitleModel, "DVDNumber", "DVDNumber", castMemberModel.DVDNumber);
            return View(castMemberModel);
        }

        // GET: CastMembers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var castMemberModel = await _context.CastMemberModel.FindAsync(id);
            if (castMemberModel == null)
            {
                return NotFound();
            }
            ViewData["ActorNumber"] = new SelectList(_context.ActorModel, "ActorNumber", "ActorNumber", castMemberModel.ActorNumber);
            ViewData["DVDNumber"] = new SelectList(_context.DVDTitleModel, "DVDNumber", "DVDNumber", castMemberModel.DVDNumber);
            return View(castMemberModel);
        }

        // POST: CastMembers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DVDNumber,ActorNumber")] CastMemberModel castMemberModel)
        {
            if (id != castMemberModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(castMemberModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CastMemberModelExists(castMemberModel.Id))
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
            ViewData["ActorNumber"] = new SelectList(_context.ActorModel, "ActorNumber", "ActorNumber", castMemberModel.ActorNumber);
            ViewData["DVDNumber"] = new SelectList(_context.DVDTitleModel, "DVDNumber", "DVDNumber", castMemberModel.DVDNumber);
            return View(castMemberModel);
        }

        // GET: CastMembers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var castMemberModel = await _context.CastMemberModel
                .Include(c => c.ActorModel)
                .Include(c => c.DVDTitleModel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (castMemberModel == null)
            {
                return NotFound();
            }

            return View(castMemberModel);
        }

        // POST: CastMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var castMemberModel = await _context.CastMemberModel.FindAsync(id);
            _context.CastMemberModel.Remove(castMemberModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CastMemberModelExists(int id)
        {
            return _context.CastMemberModel.Any(e => e.Id == id);
        }
    }
}
