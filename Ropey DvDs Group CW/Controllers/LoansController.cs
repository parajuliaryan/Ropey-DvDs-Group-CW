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
    public class LoansController : Controller
    {
        private readonly ApplicationDBContext _context;

        public LoansController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Loans
        public async Task<IActionResult> Index()
        {
            var applicationDBContext = _context.LoanModel.Include(l => l.DVDCopyModel).Include(l => l.LoanTypeModel).Include(l => l.MemberModel);
            return View(await applicationDBContext.ToListAsync());
        }

        // GET: Loans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loanModel = await _context.LoanModel
                .Include(l => l.DVDCopyModel)
                .Include(l => l.LoanTypeModel)
                .Include(l => l.MemberModel)
                .FirstOrDefaultAsync(m => m.LoanNumber == id);
            if (loanModel == null)
            {
                return NotFound();
            }

            return View(loanModel);
        }

        // GET: Loans/Create
        public IActionResult Create()
        {
            ViewData["CopyNumber"] = new SelectList(_context.Set<DVDCopyModel>(), "CopyNumber", "CopyNumber");
            ViewData["LoanTypeNumber"] = new SelectList(_context.LoanTypeModel, "LoanTypeNumber", "LoanType");
            ViewData["MemberNumber"] = new SelectList(_context.Set<MemberModel>(), "MemberNumber", "MemberFirstName");
            return View();
        }

        // POST: Loans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LoanNumber,LoanTypeNumber,CopyNumber,MemberNumber,DateOut,DateDue,DateReturned")] LoanModel loanModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loanModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CopyNumber"] = new SelectList(_context.Set<DVDCopyModel>(), "CopyNumber", "CopyNumber", loanModel.CopyNumber);
            ViewData["LoanTypeNumber"] = new SelectList(_context.LoanTypeModel, "LoanTypeNumber", "LoanTypeNumber", loanModel.LoanTypeNumber);
            ViewData["MemberNumber"] = new SelectList(_context.Set<MemberModel>(), "MemberNumber", "MemberNumber", loanModel.MemberNumber);
            return View(loanModel);
        }

        // GET: Loans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loanModel = await _context.LoanModel.FindAsync(id);
            if (loanModel == null)
            {
                return NotFound();
            }
            ViewData["CopyNumber"] = new SelectList(_context.Set<DVDCopyModel>(), "CopyNumber", "CopyNumber", loanModel.CopyNumber);
            ViewData["LoanTypeNumber"] = new SelectList(_context.LoanTypeModel, "LoanTypeNumber", "LoanTypeNumber", loanModel.LoanTypeNumber);
            ViewData["MemberNumber"] = new SelectList(_context.Set<MemberModel>(), "MemberNumber", "MemberNumber", loanModel.MemberNumber);
            return View(loanModel);
        }

        // POST: Loans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LoanNumber,LoanTypeNumber,CopyNumber,MemberNumber,DateOut,DateDue,DateReturned")] LoanModel loanModel)
        {
            if (id != loanModel.LoanNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loanModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoanModelExists(loanModel.LoanNumber))
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
            ViewData["CopyNumber"] = new SelectList(_context.Set<DVDCopyModel>(), "CopyNumber", "CopyNumber", loanModel.CopyNumber);
            ViewData["LoanTypeNumber"] = new SelectList(_context.LoanTypeModel, "LoanTypeNumber", "LoanTypeNumber", loanModel.LoanTypeNumber);
            ViewData["MemberNumber"] = new SelectList(_context.Set<MemberModel>(), "MemberNumber", "MemberNumber", loanModel.MemberNumber);
            return View(loanModel);
        }

        // GET: Loans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loanModel = await _context.LoanModel
                .Include(l => l.DVDCopyModel)
                .Include(l => l.LoanTypeModel)
                .Include(l => l.MemberModel)
                .FirstOrDefaultAsync(m => m.LoanNumber == id);
            if (loanModel == null)
            {
                return NotFound();
            }

            return View(loanModel);
        }

        // POST: Loans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loanModel = await _context.LoanModel.FindAsync(id);
            _context.LoanModel.Remove(loanModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoanModelExists(int id)
        {
            return _context.LoanModel.Any(e => e.LoanNumber == id);
        }
    }
}
