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
    public class DVDCopyController : Controller
    {
        private readonly ApplicationDBContext _context;

        public DVDCopyController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: DVDCopy
        public async Task<IActionResult> Index()
        {
            var applicationDBContext = _context.DVDCopyModel.Include(d => d.DVDTitleModel);
            return View(await applicationDBContext.ToListAsync());
        }

        // GET: DVDCopy/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dVDCopyModel = await _context.DVDCopyModel
                .Include(d => d.DVDTitleModel)
                .FirstOrDefaultAsync(m => m.CopyNumber == id);
            if (dVDCopyModel == null)
            {
                return NotFound();
            }

            return View(dVDCopyModel);
        }

        // GET: DVDCopy/Create
        public IActionResult Create()
        {
            ViewData["DVDNumber"] = new SelectList(_context.DVDTitleModel, "DVDNumber", "DVDTitle");
            return View();
        }

        // POST: DVDCopy/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CopyNumber,DVDNumber,DatePurchased")] DVDCopyModel dVDCopyModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dVDCopyModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DVDNumber"] = new SelectList(_context.DVDTitleModel, "DVDNumber", "DVDNumber", dVDCopyModel.DVDNumber);
            return View(dVDCopyModel);
        }

        // GET: DVDCopy/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dVDCopyModel = await _context.DVDCopyModel.FindAsync(id);
            if (dVDCopyModel == null)
            {
                return NotFound();
            }
            ViewData["DVDNumber"] = new SelectList(_context.DVDTitleModel, "DVDNumber", "DVDNumber", dVDCopyModel.DVDNumber);
            return View(dVDCopyModel);
        }

        // POST: DVDCopy/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CopyNumber,DVDNumber,DatePurchased")] DVDCopyModel dVDCopyModel)
        {
            if (id != dVDCopyModel.CopyNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dVDCopyModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DVDCopyModelExists(dVDCopyModel.CopyNumber))
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
            ViewData["DVDNumber"] = new SelectList(_context.DVDTitleModel, "DVDNumber", "DVDNumber", dVDCopyModel.DVDNumber);
            return View(dVDCopyModel);
        }

        // GET: DVDCopy/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dVDCopyModel = await _context.DVDCopyModel
                .Include(d => d.DVDTitleModel)
                .FirstOrDefaultAsync(m => m.CopyNumber == id);
            if (dVDCopyModel == null)
            {
                return NotFound();
            }

            return View(dVDCopyModel);
        }

        // POST: DVDCopy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dVDCopyModel = await _context.DVDCopyModel.FindAsync(id);
            _context.DVDCopyModel.Remove(dVDCopyModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DVDCopyModelExists(int id)
        {
            return _context.DVDCopyModel.Any(e => e.CopyNumber == id);
        }
    }
}
