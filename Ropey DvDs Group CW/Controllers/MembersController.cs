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
    public class MembersController : Controller
    {
        private readonly ApplicationDBContext _context;

        public MembersController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Members
        public async Task<IActionResult> Index()
        {
            var applicationDBContext = _context.MemberModel.Include(m => m.membershipCategoryModel);
            return View(await applicationDBContext.ToListAsync());
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberModel = await _context.MemberModel
                .Include(m => m.membershipCategoryModel)
                .FirstOrDefaultAsync(m => m.MemberNumber == id);
            if (memberModel == null)
            {
                return NotFound();
            }

            return View(memberModel);
        }

        // GET: Members/Create
        public IActionResult Create()
        {
            ViewData["MembershipCategoryNumber"] = new SelectList(_context.Set<MembershipCategoryModel>(), "MembershipCategoryNumber", "MembershipCategoryNumber");
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberNumber,MembershipCategoryNumber,MemberLastName,MemberFirstName,MemberAddress,MemberDateOfBirth")] MemberModel memberModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(memberModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MembershipCategoryNumber"] = new SelectList(_context.Set<MembershipCategoryModel>(), "MembershipCategoryNumber", "MembershipCategoryNumber", memberModel.MembershipCategoryNumber);
            return View(memberModel);
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberModel = await _context.MemberModel.FindAsync(id);
            if (memberModel == null)
            {
                return NotFound();
            }
            ViewData["MembershipCategoryNumber"] = new SelectList(_context.Set<MembershipCategoryModel>(), "MembershipCategoryNumber", "MembershipCategoryNumber", memberModel.MembershipCategoryNumber);
            return View(memberModel);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemberNumber,MembershipCategoryNumber,MemberLastName,MemberFirstName,MemberAddress,MemberDateOfBirth")] MemberModel memberModel)
        {
            if (id != memberModel.MemberNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(memberModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberModelExists(memberModel.MemberNumber))
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
            ViewData["MembershipCategoryNumber"] = new SelectList(_context.Set<MembershipCategoryModel>(), "MembershipCategoryNumber", "MembershipCategoryNumber", memberModel.MembershipCategoryNumber);
            return View(memberModel);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberModel = await _context.MemberModel
                .Include(m => m.membershipCategoryModel)
                .FirstOrDefaultAsync(m => m.MemberNumber == id);
            if (memberModel == null)
            {
                return NotFound();
            }

            return View(memberModel);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var memberModel = await _context.MemberModel.FindAsync(id);
            _context.MemberModel.Remove(memberModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberModelExists(int id)
        {
            return _context.MemberModel.Any(e => e.MemberNumber == id);
        }
    }
}
