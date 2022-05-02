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
            ViewData["CategoryNumber"] = new SelectList(_context.Set<DVDCategoryModel>(), "CategoryNumber", "CategoryDescription", dVDTitleModel.CategoryNumber);
            ViewData["ProducerNumber"] = new SelectList(_context.Set<ProducerModel>(), "ProducerNumber", "ProducerName", dVDTitleModel.ProducerNumber);
            ViewData["StudioNumber"] = new SelectList(_context.Set<StudioModel>(), "StudioNumber", "StudioName", dVDTitleModel.StudioNumber);
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

        public async Task<IActionResult> SelectActors(ActorModel actorModel)
        {
            ViewData["ActorSurname"] = new SelectList(_context.Set<ActorModel>(), "ActorSurname", "ActorSurname", actorModel.ActorSurname);
            return View();
        }

        public async Task<IActionResult> ShowDVDsofActors()
        {
            string actorName = Request.Form["actorList"] .ToString();
            var data = from castmembers in _context.CastMemberModel
                       join actor in _context.ActorModel on castmembers.ActorNumber equals actor.ActorNumber
                       where actor.ActorSurname == actorName join dvdtitle in _context.DVDTitleModel
                       on castmembers.DVDNumber equals dvdtitle.DVDNumber
                       select new
                       {
                           Title = dvdtitle.DVDTitle,
                           Cast = from casts in dvdtitle.CastMembers
                                  join actor in _context.ActorModel on casts.ActorNumber equals actor.ActorNumber
                                  group actor by new { casts.DVDNumber } into g
                                  select
                                       String.Join(", ", g.OrderBy(c => c.ActorSurname).Select(x => (x.ActorFirstName + " " + x.ActorSurname))),
                       }
                       ;
            return View(data);
        }

        public async Task<IActionResult> ShowDVDCopiesofActors()
        {
            string actorName = Request.Form["actorList"].ToString();
            var data = from castmembers in _context.CastMemberModel
                       join actor in _context.ActorModel on castmembers.ActorNumber equals actor.ActorNumber
                       where actor.ActorSurname == actorName
                       join dvdtitle in _context.DVDTitleModel
                       on castmembers.DVDNumber equals dvdtitle.DVDNumber 
                       select new
                       {
                           Title = dvdtitle.DVDTitle,
                           Cast = from casts in dvdtitle.CastMembers
                                  join actor in _context.ActorModel on casts.ActorNumber equals actor.ActorNumber
                                  group actor by new { casts.DVDNumber } into g
                                  select
                                       String.Join(", ", g.OrderBy(c => c.ActorSurname).Select(x => (x.ActorFirstName + " " + x.ActorSurname))),
                           NumberOfCopies = (from dvdcopy in _context.DVDCopyModel 
                                           join dt in _context.DVDTitleModel on dvdcopy.DVDNumber equals dt.DVDNumber
                                           join cm in _context.CastMemberModel on dt.DVDNumber equals cm.DVDNumber
                                           join a in _context.ActorModel on cm.ActorNumber equals a.ActorNumber
                                           where a.ActorSurname == actorName
                                           where dvdcopy.DVDNumber == dvdtitle.DVDNumber
                                           join l in _context.LoanModel on dvdcopy.CopyNumber equals l.CopyNumber
                                           where l.DateReturned != null
                                           select dvdcopy).Count()
                       }
                       ;
            return View(data);
        }

        public async Task<IActionResult> DVDsNotLoaned()
        {
            var differenceDate = DateTime.Now.AddDays(-31);

            var loanedCopyIn31Days = (from loan in _context.LoanModel
                                 where loan.DateOut >= differenceDate
                                 select loan.CopyNumber).Distinct();

            var notloanedCopyDVD = (from copy in _context.DVDCopyModel
                                    join dvdtitle in _context.DVDTitleModel on copy.DVDNumber equals dvdtitle.DVDNumber
                                    join loan in _context.LoanModel on copy.CopyNumber equals loan.CopyNumber
                                    where !(loanedCopyIn31Days).Contains(copy.CopyNumber)
                                    select new
                                    {
                                        CopyNumber = copy.CopyNumber,
                                        Title = dvdtitle.DVDTitle,
                                        Loan = loan.DateOut,
                                    });

            return View(notloanedCopyDVD);
        }
    }
}
