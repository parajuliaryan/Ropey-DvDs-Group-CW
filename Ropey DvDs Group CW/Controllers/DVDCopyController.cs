﻿#nullable disable
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
            //Gets the Latest Loan Taken Details of the Selected DVD Copy
            var latestLoan = from loan in _context.LoanModel          
                             join member in _context.MemberModel on loan.MemberNumber equals member.MemberNumber
                                     where loan.CopyNumber == id
                                     orderby loan.DateOut descending                                     
                                     select new {
                                         MemberName = member.MemberFirstName + " " + member.MemberLastName,
                                         Loan = loan
                                        };
            //Select the first data from the database response.
            var data = latestLoan.FirstOrDefault();
            if(data != null)
            {
                //Returning the Loan and Member Details
                ViewData["loanData"] = data.Loan;
                ViewData["lastLoanMemberName"] = data.MemberName;
            }       
            //Returning DVD Copy Details
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

        public async Task<IActionResult> OlderCopyDVD()
        {

            //Get Distinct Copy Numbers of Loaned DVDs
            var loanedCopyDVD = (from loan in _context.LoanModel
                                where loan.DateReturned == null
                                select loan.CopyNumber).Distinct();

            //Get Data of Copies that have not been loaned
            var notloanedCopyDVD = (from copy in _context.DVDCopyModel
                                   join dvdtitle in _context.DVDTitleModel on copy.DVDNumber equals dvdtitle.DVDNumber
                                   where !(loanedCopyDVD).Contains(copy.CopyNumber)
                                   select new
                                   {
                                       CopyNumber = copy.CopyNumber,
                                       DVDTitle = dvdtitle.DVDTitle,
                                       DatePurchased = copy.DatePurchased
                                   });

            return View(await notloanedCopyDVD.ToListAsync());
        }

        public async Task<IActionResult> RemoveOldCopies()
        {
            //Get Distinct Copy Numbers of Loaned DVDs
            var loanedCopyDVD = (from loan in _context.LoanModel
                                 where loan.DateReturned == null
                                 select loan.CopyNumber).Distinct();

            //Get Data of Copies that have not been loaned
            var notloanedCopyDVD = (from copy in _context.DVDCopyModel
                                    join dvdtitle in _context.DVDTitleModel on copy.DVDNumber equals dvdtitle.DVDNumber
                                    where !(loanedCopyDVD).Contains(copy.CopyNumber)
                                    select new
                                    {
                                        CopyNumber = copy.CopyNumber,
                                        DVDTitle = dvdtitle.DVDTitle,
                                        DatePurchased = copy.DatePurchased
                                    });
            
            foreach(var copy in notloanedCopyDVD.ToList())
            {
                //Check if the Copy is older than one year.
                if(DateTime.Now.Subtract(copy.DatePurchased).Days > 365)
                {
                    //Remove the Copy from the Database if the Copy is older than one year.
                    var remove = (from removeCopy in _context.DVDCopyModel
                                  where removeCopy.CopyNumber == copy.CopyNumber
                                  select removeCopy).FirstOrDefault();
                    _context.DVDCopyModel.Remove(remove);
                   
                }
            }
            //Save the changes made to the database.
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> LoanedOutCopies()
        {
 
            //Check if any date has been selected in the view.
            if(Request.Form.Count() == 2 )
            {
                //Get the Selected Date from the View Form.
                ViewData["SelectedDate"] = Request.Form["SearchDate"].ToString();
                //Parse the Selected Date String to DateTime
                DateTime searchingDate = DateTime.Parse(Request.Form["SearchDate"].ToString());
                //Get all Loan data which matches Selected Date in DateOut
                var applicationDBContext = from loan in _context.LoanModel
                                           join copy in _context.DVDCopyModel on loan.CopyNumber equals copy.CopyNumber
                                           join dvdtitle in _context.DVDTitleModel on copy.DVDNumber equals dvdtitle.DVDNumber
                                           join member in _context.MemberModel on loan.MemberNumber equals member.MemberNumber
                                           orderby loan.DateOut
                                           where loan.DateOut.Date == searchingDate.Date
                                           select new
                                           {
                                               LoanNumber = loan.LoanNumber,
                                               DVDTitle = dvdtitle.DVDTitle,
                                               CopyNumber = copy.CopyNumber,
                                               Member = member.MemberFirstName + " " + member.MemberLastName,
                                               DateOut = loan.DateOut
                                           };
                //Count the total Loans of that Selected Date and send to the View as ViewData
                ViewData["TotalLoans"] = applicationDBContext.ToList().Count();

                return View(await applicationDBContext.ToListAsync());

            }
            else
            {
                //If no Date has been Selected, Use Today's DateTime
                ViewData["SelectedDate"] = DateTime.Today.ToString("yyyy-MM-dd");
                //Get all Loan data which matches Selected Date in DateOut
                var applicationDBContext = from loan in _context.LoanModel
                                           join copy in _context.DVDCopyModel on loan.CopyNumber equals copy.CopyNumber
                                           join dvdtitle in _context.DVDTitleModel on copy.DVDNumber equals dvdtitle.DVDNumber
                                           join member in _context.MemberModel on loan.MemberNumber equals member.MemberNumber
                                           orderby loan.DateOut                                          
                                           where loan.DateOut.Date == DateTime.Today.Date
                                           select new
                                           {
                                               LoanNumber = loan.LoanNumber,
                                               DVDTitle = dvdtitle.DVDTitle,
                                               CopyNumber = copy.CopyNumber,
                                               Member = member.MemberFirstName + " " + member.MemberLastName,
                                               DateOut = loan.DateOut
                                           };
                //Count the total Loans of that Selected Date and send to the View as ViewData
                ViewData["TotalLoans"] = applicationDBContext.ToList().Count();

                return View(await applicationDBContext.ToListAsync());
            }
            
        }
    }
}
