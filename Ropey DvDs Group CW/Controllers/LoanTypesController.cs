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
    public class LoanTypesController : Controller
    {
        private readonly ApplicationDBContext _context;

        public LoanTypesController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: LoanTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.LoanTypeModel.ToListAsync());
        }

        // GET: LoanTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loanTypeModel = await _context.LoanTypeModel
                .FirstOrDefaultAsync(m => m.LoanTypeNumber == id);
            if (loanTypeModel == null)
            {
                return NotFound();
            }

            return View(loanTypeModel);
        }

        // GET: LoanTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LoanTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LoanTypeNumber,LoanType,LoanDuration")] LoanTypeModel loanTypeModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loanTypeModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loanTypeModel);
        }

        // GET: LoanTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loanTypeModel = await _context.LoanTypeModel.FindAsync(id);
            if (loanTypeModel == null)
            {
                return NotFound();
            }
            return View(loanTypeModel);
        }

        // POST: LoanTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LoanTypeNumber,LoanType,LoanDuration")] LoanTypeModel loanTypeModel)
        {
            if (id != loanTypeModel.LoanTypeNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loanTypeModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoanTypeModelExists(loanTypeModel.LoanTypeNumber))
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
            return View(loanTypeModel);
        }

        // GET: LoanTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loanTypeModel = await _context.LoanTypeModel
                .FirstOrDefaultAsync(m => m.LoanTypeNumber == id);
            if (loanTypeModel == null)
            {
                return NotFound();
            }

            return View(loanTypeModel);
        }

        // POST: LoanTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loanTypeModel = await _context.LoanTypeModel.FindAsync(id);
            _context.LoanTypeModel.Remove(loanTypeModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoanTypeModelExists(int id)
        {
            return _context.LoanTypeModel.Any(e => e.LoanTypeNumber == id);
        }
    }
}
