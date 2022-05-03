#nullable disable
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
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
            var applicationDBContext = from members in _context.MemberModel
                                       join membership in _context.MembershipCategoryModel on members.MembershipCategoryNumber equals membership.MembershipCategoryNumber
                                       select new
                                       {
                                           MemberNumber = members.MemberNumber,
                                           MemberFirstName = members.MemberFirstName,
                                           MemberLastName = members.MemberLastName,
                                           MemberAddress = members.MemberAddress,
                                           MemberDOB = members.MemberDateOfBirth,
                                           Membership = membership.MembershipCategoryDescription,
                                           TotalAcceptLoans = membership.MembershipCategoryTotalLoan,
                                           TotalCurrentLoans = (from loans in _context.LoanModel
                                                                where loans.DateReturned == null
                                                                where loans.MemberNumber == members.MemberNumber
                                                                select loans).Count(),
                                       };
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

            var differenceDate = DateTime.Now.AddDays(-31);
            var data = from member in _context.MemberModel join
                        loan in _context.LoanModel on member.MemberNumber equals loan.MemberNumber
                        where loan.DateOut >= differenceDate
                        where member.MemberNumber == id  
                        join dvdcopy in _context.DVDCopyModel on loan.CopyNumber equals dvdcopy.CopyNumber
                        join dvdtitle in _context.DVDTitleModel on dvdcopy.DVDNumber equals dvdtitle.DVDNumber
                        select new
                        {
                            Member = member.MemberFirstName + " " + member.MemberLastName,
                            Loan = loan.LoanNumber,
                            CopyNumber = dvdcopy.CopyNumber,
                            Title = dvdtitle.DVDTitle,
                        };

            if (memberModel == null)
            {
                return NotFound();
            }

            if (data == null )
            {
                return View(memberModel);
            }
            else
            {
                return View(data);
            }

            
        }

        // GET: Members/Create
        public IActionResult Create()
        {
            ViewData["MembershipCategoryNumber"] = new SelectList(_context.Set<MembershipCategoryModel>(), "MembershipCategoryDescription", "MembershipCategoryDescription");
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
            ViewData["MembershipCategoryNumber"] = new SelectList(_context.Set<MembershipCategoryModel>(), "MembershipCategoryNumber", "MembershipCategoryDescription", memberModel.MembershipCategoryNumber);
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

        public async Task<IActionResult> SleepingMembers()
        {
            var differenceDate = DateTime.Now.AddDays(-31);
            var loanTakenByMembers = (from loans in _context.LoanModel
                                     where loans.DateOut >= differenceDate
                                     select loans.MemberNumber).Distinct();

            var loanNotTakenByMembers = from members in _context.MemberModel                                       
                                        where !(loanTakenByMembers).Contains(members.MemberNumber)
                                        select new
                                        {
                                            MemberName = members.MemberFirstName + " " + members.MemberLastName,
                                            MemberAddress = members.MemberAddress,
                                            LastLoan = (from loans in _context.LoanModel
                                                        join copy in _context.DVDCopyModel on loans.CopyNumber equals copy.CopyNumber
                                                        join dvdtitle in _context.DVDTitleModel on copy.DVDNumber equals dvdtitle.DVDNumber
                                                        where loans.MemberNumber == members.MemberNumber
                                                        orderby loans.DateOut descending
                                                        select new
                                                        {
                                                            DateOut = loans.DateOut,
                                                            DVDTitle = dvdtitle.DVDTitle,
                                                        }
                                                        ).FirstOrDefault()
                                        };

            return View(loanNotTakenByMembers);
        }
    }
}
