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
    public class DVDsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public DVDsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: DVDs
        public async Task<IActionResult> Index()
        {
            var applicationDBContext = _context.DVDTitleModel.Include(d => d.DVDCategoryModel).Include(d => d.ProducerModel).Include(d => d.StudioModel);
            return View(await applicationDBContext.ToListAsync());
        }

        // GET: DVDs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dVDTitleModel = await _context.DVDTitleModel
                .Include(d => d.DVDCategoryModel)
                .Include(d => d.ProducerModel)
                .Include(d => d.StudioModel)
                .FirstOrDefaultAsync(m => m.DVDNumber == id);
            if (dVDTitleModel == null)
            {
                return NotFound();
            }

            return View(dVDTitleModel);
        }

        // GET: DVDs/Create
        public IActionResult Create()
        {
            //SelectList provides data using Iterable,Value and Description
            ViewData["CategoryNumber"] = new SelectList(_context.Set<DVDCategoryModel>(), "CategoryNumber", "CategoryDescription");
            ViewData["ProducerNumber"] = new SelectList(_context.Set<ProducerModel>(), "ProducerNumber", "ProducerName");
            ViewData["StudioNumber"] = new SelectList(_context.Set<StudioModel>(), "StudioNumber", "StudioName");
            return View();
        }

        // POST: DVDs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DVDNumber,CategoryNumber,StudioNumber,ProducerNumber,DVDTitle,DateReleased,StandardCharge,PenaltyCharge")] DVDTitleModel dVDTitleModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dVDTitleModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryNumber"] = new SelectList(_context.Set<DVDCategoryModel>(), "CategoryNumber", "CategoryNumber", dVDTitleModel.CategoryNumber);
            ViewData["ProducerNumber"] = new SelectList(_context.Set<ProducerModel>(), "ProducerNumber", "ProducerNumber", dVDTitleModel.ProducerNumber);
            ViewData["StudioNumber"] = new SelectList(_context.Set<StudioModel>(), "StudioNumber", "StudioNumber", dVDTitleModel.StudioNumber);
            return View(dVDTitleModel);
        }

        // GET: DVDs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dVDTitleModel = await _context.DVDTitleModel.FindAsync(id);
            if (dVDTitleModel == null)
            {
                return NotFound();
            }
            ViewData["CategoryNumber"] = new SelectList(_context.Set<DVDCategoryModel>(), "CategoryNumber", "CategoryNumber", dVDTitleModel.CategoryNumber);
            ViewData["ProducerNumber"] = new SelectList(_context.Set<ProducerModel>(), "ProducerNumber", "ProducerNumber", dVDTitleModel.ProducerNumber);
            ViewData["StudioNumber"] = new SelectList(_context.Set<StudioModel>(), "StudioNumber", "StudioNumber", dVDTitleModel.StudioNumber);
            return View(dVDTitleModel);
        }

        // POST: DVDs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DVDNumber,CategoryNumber,StudioNumber,ProducerNumber,DVDTitle,DateReleased,StandardCharge,PenaltyCharge")] DVDTitleModel dVDTitleModel)
        {
            if (id != dVDTitleModel.DVDNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dVDTitleModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DVDTitleModelExists(dVDTitleModel.DVDNumber))
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
            ViewData["CategoryNumber"] = new SelectList(_context.Set<DVDCategoryModel>(), "CategoryNumber", "CategoryNumber", dVDTitleModel.CategoryNumber);
            ViewData["ProducerNumber"] = new SelectList(_context.Set<ProducerModel>(), "ProducerNumber", "ProducerNumber", dVDTitleModel.ProducerNumber);
            ViewData["StudioNumber"] = new SelectList(_context.Set<StudioModel>(), "StudioNumber", "StudioNumber", dVDTitleModel.StudioNumber);
            return View(dVDTitleModel);
        }

        // GET: DVDs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dVDTitleModel = await _context.DVDTitleModel
                .Include(d => d.DVDCategoryModel)
                .Include(d => d.ProducerModel)
                .Include(d => d.StudioModel)
                .FirstOrDefaultAsync(m => m.DVDNumber == id);
            if (dVDTitleModel == null)
            {
                return NotFound();
            }

            return View(dVDTitleModel);
        }

        // POST: DVDs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dVDTitleModel = await _context.DVDTitleModel.FindAsync(id);
            _context.DVDTitleModel.Remove(dVDTitleModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DVDTitleModelExists(int id)
        {
            return _context.DVDTitleModel.Any(e => e.DVDNumber == id);
        }

        public async Task<IActionResult> DVDDetailsIndex()
        {
            var data = from dvdtitle in _context.DVDTitleModel
                       join dvdcategory in _context.DVDCategoryModel on dvdtitle.CategoryNumber equals dvdcategory.CategoryNumber
                       join studio in _context.StudioModel on dvdtitle.StudioNumber equals studio.StudioNumber
                       orderby dvdtitle.DateReleased
                       select new
                       {
                          Title = dvdtitle.DVDTitle,
                           Category = dvdcategory.CategoryDescription,
                           Studio = studio.StudioName,
                           Producer = dvdtitle.ProducerModel.ProducerName,
                           Cast = from casts in dvdtitle.CastMembers
                                  join actor in _context.ActorModel on casts.ActorNumber equals actor.ActorNumber                                  
                                  group actor by new { casts.DVDNumber } into g
                                  select
                                       String.Join(", ", g.OrderBy(c => c.ActorSurname).Select(x => (x.ActorFirstName + " " + x.ActorSurname))),
                           Release = dvdtitle.DateReleased.ToString("dd MMM yyyy"),
                       };
            data.OrderBy(c => c.Cast);
            return View(data);
        }
    }
}
