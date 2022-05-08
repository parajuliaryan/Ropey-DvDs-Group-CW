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
    public class DVDCategoryController : Controller
    {
        private readonly ApplicationDBContext _context;

        public DVDCategoryController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: DVDCategory
        public async Task<IActionResult> Index()
        {
            return View(await _context.DVDCategoryModel.ToListAsync());
        }

        // GET: DVDCategory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dVDCategoryModel = await _context.DVDCategoryModel
                .FirstOrDefaultAsync(m => m.CategoryNumber == id);
            if (dVDCategoryModel == null)
            {
                return NotFound();
            }

            return View(dVDCategoryModel);
        }

        // GET: DVDCategory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DVDCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryNumber,CategoryDescription,AgeRestricted")] DVDCategoryModel dVDCategoryModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dVDCategoryModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dVDCategoryModel);
        }

        // GET: DVDCategory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dVDCategoryModel = await _context.DVDCategoryModel.FindAsync(id);
            if (dVDCategoryModel == null)
            {
                return NotFound();
            }
            return View(dVDCategoryModel);
        }

        // POST: DVDCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryNumber,CategoryDescription,AgeRestricted")] DVDCategoryModel dVDCategoryModel)
        {
            if (id != dVDCategoryModel.CategoryNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dVDCategoryModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DVDCategoryModelExists(dVDCategoryModel.CategoryNumber))
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
            return View(dVDCategoryModel);
        }

        // GET: DVDCategory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dVDCategoryModel = await _context.DVDCategoryModel
                .FirstOrDefaultAsync(m => m.CategoryNumber == id);
            if (dVDCategoryModel == null)
            {
                return NotFound();
            }

            return View(dVDCategoryModel);
        }

        // POST: DVDCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dVDCategoryModel = await _context.DVDCategoryModel.FindAsync(id);
            _context.DVDCategoryModel.Remove(dVDCategoryModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DVDCategoryModelExists(int id)
        {
            return _context.DVDCategoryModel.Any(e => e.CategoryNumber == id);
        }
    }
}
