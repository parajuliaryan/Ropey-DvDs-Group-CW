#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ropey_DvDs_Group_CW.DBContext;
using Ropey_DvDs_Group_CW.Models;

namespace Ropey_DvDs_Group_CW.Controllers
{
    [Authorize(Roles = "Manager,Assistant", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MembershipCategoryController : Controller
    {
        private readonly ApplicationDBContext _context;

        public MembershipCategoryController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: MembershipCategory
        public async Task<IActionResult> Index()
        {
            return View(await _context.MembershipCategoryModel.ToListAsync());
        }

        // GET: MembershipCategory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipCategoryModel = await _context.MembershipCategoryModel
                .FirstOrDefaultAsync(m => m.MembershipCategoryNumber == id);
            if (membershipCategoryModel == null)
            {
                return NotFound();
            }

            return View(membershipCategoryModel);
        }

        // GET: MembershipCategory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MembershipCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MembershipCategoryNumber,MembershipCategoryDescription,MembershipCategoryTotalLoan")] MembershipCategoryModel membershipCategoryModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(membershipCategoryModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(membershipCategoryModel);
        }

        // GET: MembershipCategory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipCategoryModel = await _context.MembershipCategoryModel.FindAsync(id);
            if (membershipCategoryModel == null)
            {
                return NotFound();
            }
            return View(membershipCategoryModel);
        }

        // POST: MembershipCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MembershipCategoryNumber,MembershipCategoryDescription,MembershipCategoryTotalLoan")] MembershipCategoryModel membershipCategoryModel)
        {
            if (id != membershipCategoryModel.MembershipCategoryNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(membershipCategoryModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembershipCategoryModelExists(membershipCategoryModel.MembershipCategoryNumber))
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
            return View(membershipCategoryModel);
        }

        // GET: MembershipCategory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipCategoryModel = await _context.MembershipCategoryModel
                .FirstOrDefaultAsync(m => m.MembershipCategoryNumber == id);
            if (membershipCategoryModel == null)
            {
                return NotFound();
            }

            return View(membershipCategoryModel);
        }

        // POST: MembershipCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var membershipCategoryModel = await _context.MembershipCategoryModel.FindAsync(id);
            _context.MembershipCategoryModel.Remove(membershipCategoryModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MembershipCategoryModelExists(int id)
        {
            return _context.MembershipCategoryModel.Any(e => e.MembershipCategoryNumber == id);
        }
    }
}
