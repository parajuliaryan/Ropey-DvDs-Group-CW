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
    public class ProducersController : Controller
    {
        private readonly ApplicationDBContext _context;

        public ProducersController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Producers
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProducerModel.ToListAsync());
        }

        // GET: Producers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producerModel = await _context.ProducerModel
                .FirstOrDefaultAsync(m => m.ProducerNumber == id);
            if (producerModel == null)
            {
                return NotFound();
            }

            return View(producerModel);
        }

        // GET: Producers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Producers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProducerNumber,ProducerName")] ProducerModel producerModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producerModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(producerModel);
        }

        // GET: Producers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producerModel = await _context.ProducerModel.FindAsync(id);
            if (producerModel == null)
            {
                return NotFound();
            }
            return View(producerModel);
        }

        // POST: Producers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProducerNumber,ProducerName")] ProducerModel producerModel)
        {
            if (id != producerModel.ProducerNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producerModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProducerModelExists(producerModel.ProducerNumber))
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
            return View(producerModel);
        }

        // GET: Producers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producerModel = await _context.ProducerModel
                .FirstOrDefaultAsync(m => m.ProducerNumber == id);
            if (producerModel == null)
            {
                return NotFound();
            }

            return View(producerModel);
        }

        // POST: Producers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producerModel = await _context.ProducerModel.FindAsync(id);
            _context.ProducerModel.Remove(producerModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProducerModelExists(int id)
        {
            return _context.ProducerModel.Any(e => e.ProducerNumber == id);
        }
    }
}
