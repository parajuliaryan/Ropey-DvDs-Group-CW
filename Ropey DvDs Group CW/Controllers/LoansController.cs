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
    //[Authorize(Roles = "Manager,Assistant", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
            ViewData["CopyNumber"] = new SelectList(from copy in _context.DVDCopyModel
                                                    join dvdtitle in _context.DVDTitleModel on copy.DVDNumber equals dvdtitle.DVDNumber
                                                    select new
                                                    {
                                                        CopyNumber = copy.CopyNumber,
                                                        Title = copy.CopyNumber + " - " + dvdtitle.DVDTitle,  
                                                    }, "CopyNumber", "Title");
            ViewData["LoanTypeNumber"] = new SelectList(_context.LoanTypeModel, "LoanTypeNumber", "LoanType");
            ViewData["MemberNumber"] = new SelectList(from member in _context.MemberModel
                                                      select new
                                                      {
                                                          MemberNumber = member.MemberNumber,
                                                          MemberName = member.MemberFirstName + " " + member.MemberLastName,
                                                      }, "MemberNumber", "MemberName");
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
                //Get Details of the Member
                var memberDOB = from member in _context.MemberModel
                                join membership in _context.MembershipCategoryModel on member.MembershipCategoryNumber equals membership.MembershipCategoryNumber
                                where member.MemberNumber == loanModel.MemberNumber
                                select new
                                {
                                    DateOfBirth = member.MemberDateOfBirth,
                                    totalLoans = membership.MembershipCategoryTotalLoan

                                };
                //Get the First Data from the Details
                var data1 = memberDOB.FirstOrDefault();

                //Get Age Restricted data of the DVD 
                var ageRestricted = from copy in _context.DVDCopyModel
                                    join title in _context.DVDTitleModel on copy.DVDNumber equals title.DVDNumber
                                    join category in _context.DVDCategoryModel on title.CategoryNumber equals category.CategoryNumber
                                    where copy.CopyNumber == loanModel.CopyNumber
                                    select new
                                    {
                                        restricted = category.AgeRestricted,
                                    };
                var data2 = ageRestricted.FirstOrDefault();

                //Get the Total Loans Taken by a Member
                var loansTaken = (from loans in _context.LoanModel
                             where loans.MemberNumber == loanModel.MemberNumber
                             select loans).Count();
                
                var dvdLoanTaken = (from loans in _context.LoanModel
                                    where loans.CopyNumber == loanModel.CopyNumber
                                    where loans.DateReturned == null
                                    select loans).Count();

                if(dvdLoanTaken == 0)
                {
                    //Check if DateDue is greater than DateOut
                    if (loanModel.DateDue > loanModel.DateOut)
                    {
                        //Check if Loans Taken is less than the Loans that can be allowed to be Taken
                        if (loansTaken < data1.totalLoans)
                        {
                            var dt = data1.DateOfBirth;
                            var today = DateTime.Today;
                            //Get the Age of the Member
                            var age = today.Year - dt.Year;

                            //Checks Leap Year and if it is a leap year, remove a year from age.
                            if (dt.Date > today.AddYears(-age)) age--;

                            //Check Age Restrictions
                            if (data2.restricted)
                            {
                                //Check if age is greater than 18
                                if (age >= 18)
                                {
                                    //Add Loan to the Database
                                    _context.Add(loanModel);
                                    await _context.SaveChangesAsync();
                                    return RedirectToAction(nameof(Index));
                                }
                                else
                                {
                                    //If age is less than 18, send an error alert.
                                    ViewData["DangerAlert"] = "This user is too young";
                                }
                            }
                            else
                            {
                                _context.Add(loanModel);
                                await _context.SaveChangesAsync();
                                return RedirectToAction(nameof(Index));
                            }
                        }
                        else
                        {
                            ViewData["DangerAlert"] = "This user can\'t take more loans";
                        }
                    }
                    else
                    {
                        ViewData["DangerAlert"] = "Date Due cannot be smaller than Date Out";
                    }
                }
                else
                {
                    ViewData["DangerAlert"] = "This Copy has already been loaned";
                }

            }
            ViewData["CopyNumber"] = new SelectList(from copy in _context.DVDCopyModel
                                                    join dvdtitle in _context.DVDTitleModel on copy.DVDNumber equals dvdtitle.DVDNumber
                                                    select new
                                                    {
                                                        CopyNumber = copy.CopyNumber,
                                                        Title = copy.CopyNumber + " - " + dvdtitle.DVDTitle,
                                                    }, "CopyNumber", "Title");
            ViewData["LoanTypeNumber"] = new SelectList(_context.LoanTypeModel, "LoanTypeNumber", "LoanType");
            ViewData["MemberNumber"] = new SelectList(from member in _context.MemberModel
                                                      select new
                                                      {
                                                          MemberNumber = member.MemberNumber,
                                                          MemberName = member.MemberFirstName + " " + member.MemberLastName,
                                                      }, "MemberNumber", "MemberName");
            return View();
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
            ViewData["CopyNumber"] = new SelectList(from copy in _context.DVDCopyModel
                                                    join dvdtitle in _context.DVDTitleModel on copy.DVDNumber equals dvdtitle.DVDNumber
                                                    select new
                                                    {
                                                        CopyNumber = copy.CopyNumber,
                                                        Title = copy.CopyNumber + " - " + dvdtitle.DVDTitle,
                                                    }, "CopyNumber", "Title");
            ViewData["LoanTypeNumber"] = new SelectList(_context.LoanTypeModel, "LoanTypeNumber", "LoanType");
            ViewData["MemberNumber"] = new SelectList(from member in _context.MemberModel
                                                      select new
                                                      {
                                                          MemberNumber = member.MemberNumber,
                                                          MemberName = member.MemberFirstName + " " + member.MemberLastName,
                                                      }, "MemberNumber", "MemberName");
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
                    //Check if DateReturned is greater than DateDue.
                    if((loanModel.DateReturned - loanModel.DateDue).Value.TotalDays >= 0)
                    {
                        _context.Update(loanModel);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Payment", loanModel);
                    }
                    else
                    {
                        ViewData["DangerAlert"] = "Return Date must not be less than Due Date";
                        ViewData["CopyNumber"] = new SelectList(from copy in _context.DVDCopyModel
                                                                join dvdtitle in _context.DVDTitleModel on copy.DVDNumber equals dvdtitle.DVDNumber
                                                                select new
                                                                {
                                                                    CopyNumber = copy.CopyNumber,
                                                                    Title = copy.CopyNumber + " - " + dvdtitle.DVDTitle,
                                                                }, "CopyNumber", "Title");
                        ViewData["LoanTypeNumber"] = new SelectList(_context.LoanTypeModel, "LoanTypeNumber", "LoanType");
                        ViewData["MemberNumber"] = new SelectList(from member in _context.MemberModel
                                                                  select new
                                                                  {
                                                                      MemberNumber = member.MemberNumber,
                                                                      MemberName = member.MemberFirstName + " " + member.MemberLastName,
                                                                  }, "MemberNumber", "MemberName");
                        return View(loanModel);
                    }
                    
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
                
            }
            ViewData["CopyNumber"] = new SelectList(from copy in _context.DVDCopyModel
                                                    join dvdtitle in _context.DVDTitleModel on copy.DVDNumber equals dvdtitle.DVDNumber
                                                    select new
                                                    {
                                                        CopyNumber = copy.CopyNumber,
                                                        Title = copy.CopyNumber + " - " + dvdtitle.DVDTitle,
                                                    }, "CopyNumber", "Title");
            ViewData["LoanTypeNumber"] = new SelectList(_context.LoanTypeModel, "LoanTypeNumber", "LoanType");
            ViewData["MemberNumber"] = new SelectList(from member in _context.MemberModel
                                                      select new
                                                      {
                                                          MemberNumber = member.MemberNumber,
                                                          MemberName = member.MemberFirstName + " " + member.MemberLastName,
                                                      }, "MemberNumber", "MemberName");
            return View(loanModel);
        }

        public async Task<IActionResult> Payment(LoanModel loanModel)
        {
            //Using LINQ to get Loan Details after returning the DVD Copy
            var loanDetails = (from loans in _context.LoanModel
                               join member in _context.MemberModel on loans.MemberNumber equals member.MemberNumber
                               join membership in _context.MembershipCategoryModel on member.MembershipCategoryNumber equals membership.MembershipCategoryNumber
                               join loanType in _context.LoanTypeModel on loans.LoanTypeNumber equals loanType.LoanTypeNumber
                               join copy in _context.DVDCopyModel on loans.CopyNumber equals copy.CopyNumber
                               join dvdtitle in _context.DVDTitleModel on copy.DVDNumber equals dvdtitle.DVDNumber
                               where loans.LoanNumber == loanModel.LoanNumber
                               select new
                               {
                                  LoanNumber = loanModel.LoanNumber,
                                  CopyNumber = loans.CopyNumber,
                                  DateOut = loans.DateOut,
                                  DateDue = loans.DateDue,
                                  DateReturned = loanModel.DateReturned,
                                  Member = member.MemberFirstName + " " + member.MemberLastName,
                                  Membership = membership.MembershipCategoryDescription,
                                  LoanType = loanType.LoanType,
                                  LoanDuration = loanType.LoanDuration,
                                  DVDTitle = dvdtitle.DVDTitle,
                                  StandardCharge = dvdtitle.StandardCharge,
                                  PenaltyCharge = dvdtitle.PenaltyCharge,

                                  
                               }).FirstOrDefault();
            return View(loanDetails);
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

        public void onChangeCopyDropDown()
        {
            Console.WriteLine();
        }
    }
}
