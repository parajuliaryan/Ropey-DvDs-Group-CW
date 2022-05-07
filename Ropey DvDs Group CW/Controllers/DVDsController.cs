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
    [Authorize(Roles = "Manager", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
            //Using LINQ to get Actors data for ListBox
            var data = from actor in _context.ActorModel
                       orderby actor.ActorFirstName,actor.ActorSurname
                       select new
                       {
                           ActorNumber = actor.ActorNumber,
                           ActorName = actor.ActorFirstName + " " + actor.ActorSurname
                       };

            
            ViewData["ListBoxData"] = data;
            return View();
        }

        // POST: DVDs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DVDNumber,CategoryNumber,StudioNumber,ProducerNumber,DVDTitle,DateReleased,StandardCharge,PenaltyCharge")] DVDTitleModel dVDTitleModel, List<int> multipleSelect)
        {
            if (ModelState.IsValid)
            {
                //Save the DVDTitle Entry to Database
                _context.Add(dVDTitleModel);
                await _context.SaveChangesAsync();
                //Get the DVD Number from the latest entry
                var latestEntry = (from dvdtitle in _context.DVDTitleModel
                                  orderby dvdtitle.DVDNumber descending
                                  select dvdtitle.DVDNumber).FirstOrDefault();

                foreach(int id in multipleSelect)
                {
                    //Create a new object for CastMemberModel
                    CastMemberModel castMemberModel = new CastMemberModel();
                    castMemberModel.DVDNumber = latestEntry;
                    castMemberModel.ActorNumber = id;
                    //Save the object to Database
                    _context.Add(castMemberModel);
                    await _context.SaveChangesAsync();

                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryNumber"] = new SelectList(_context.Set<DVDCategoryModel>(), "CategoryNumber", "CategoryNumber", dVDTitleModel.CategoryNumber);
            ViewData["ProducerNumber"] = new SelectList(_context.Set<ProducerModel>(), "ProducerNumber", "ProducerNumber", dVDTitleModel.ProducerNumber);
            ViewData["StudioNumber"] = new SelectList(_context.Set<StudioModel>(), "StudioNumber", "StudioNumber", dVDTitleModel.StudioNumber);
            //Using LINQ to get Actors data for ListBox
            var data = from actor in _context.ActorModel
                       orderby actor.ActorFirstName, actor.ActorSurname
                       select new
                       {
                           ActorNumber = actor.ActorNumber,
                           ActorName = actor.ActorFirstName + " " + actor.ActorSurname
                       };


            ViewData["ListBoxData"] = data;
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
            //Using LINQ to get data of all DVDs 
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
            //Ordering the data by Cast
            data.OrderBy(c => c.Cast);
            return View(await data.ToListAsync());
        }

        public async Task<IActionResult> SelectActors(ActorModel actorModel)
        {
            ViewData["ActorSurname"] = new SelectList(_context.Set<ActorModel>(), "ActorSurname", "ActorSurname", actorModel.ActorSurname);
            return View();
        }

        public async Task<IActionResult> ShowDVDsofActors()
        {
            //Get the Selected Actor's name from the View
            string actorName = Request.Form["actorList"] .ToString();
            //Get the data of the Selected Actor
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
                       };
       
            return View(await data.ToListAsync());
        }

        public async Task<IActionResult> ShowDVDCopiesofActors()
        {
            //Get the Selected Actor's name from the View
            string actorName = Request.Form["actorList"].ToString();

            //Get Distinct Copy Numbers of Loaned DVDs
            var loanedCopies = (from loan in _context.LoanModel
                                where loan.DateReturned == null
                                select loan.CopyNumber).Distinct();

            //Get the Copies Data 
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
                           NumberOfCopies = (from copy in _context.DVDCopyModel
                                             where !(loanedCopies).Contains(copy.CopyNumber)
                                             where copy.DVDNumber == dvdtitle.DVDNumber
                                             select copy).Count()
                       };

            //Get Data of those DVD Copies that has more than 0 copies.
            data = data.Where(x => x.NumberOfCopies > 0);
            if(data.Count() == 0)
            {
                ViewData["NoData"] = "The Actor has no Copies on our Shelves";
            }
            return View(await data.ToListAsync());
        }

        public async Task<IActionResult> DVDsNotLoaned()
        {
            //Get DateTime of 31 Days Before Today's DateTime
            var differenceDate = DateTime.Now.AddDays(-31);

            //Get all data of Loaned Copies in 31 Days
            var loanedCopyIn31Days = (from loan in _context.LoanModel
                                 where loan.DateOut >= differenceDate
                                 select loan.CopyNumber).Distinct();

            //Get all data of Copies that have not been loaned.
            var notloanedCopyDVD = from copy in _context.DVDCopyModel
                                   join dvdtitle in _context.DVDTitleModel on copy.DVDNumber equals dvdtitle.DVDNumber
                                   where !(loanedCopyIn31Days).Contains(copy.CopyNumber)
                                   select new
                                   {
                                       CopyNumber = copy.CopyNumber,
                                       Title = dvdtitle.DVDTitle,
                                   };

            return View(await notloanedCopyDVD.ToListAsync());
        }
    }
}
