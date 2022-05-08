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
    public class StudioController : Controller
    {
        private readonly ApplicationDBContext _context;

        public StudioController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Studio
        public async Task<IActionResult> Index()
        {
            return View(await _context.StudioModel.ToListAsync());
        }

        // GET: Studio/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studioModel = await _context.StudioModel
                .FirstOrDefaultAsync(m => m.StudioNumber == id);
            if (studioModel == null)
            {
                return NotFound();
            }

            return View(studioModel);
        }

        // GET: Studio/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Studio/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudioNumber,StudioName")] StudioModel studioModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studioModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studioModel);
        }

        // GET: Studio/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studioModel = await _context.StudioModel.FindAsync(id);
            if (studioModel == null)
            {
                return NotFound();
            }
            return View(studioModel);
        }

        // POST: Studio/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudioNumber,StudioName")] StudioModel studioModel)
        {
            if (id != studioModel.StudioNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studioModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudioModelExists(studioModel.StudioNumber))
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
            return View(studioModel);
        }

        // GET: Studio/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studioModel = await _context.StudioModel
                .FirstOrDefaultAsync(m => m.StudioNumber == id);
            if (studioModel == null)
            {
                return NotFound();
            }

            return View(studioModel);
        }

        // POST: Studio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studioModel = await _context.StudioModel.FindAsync(id);
            _context.StudioModel.Remove(studioModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudioModelExists(int id)
        {
            return _context.StudioModel.Any(e => e.StudioNumber == id);
        }
    }
}
