#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Webform.Data;
using Webform.Models;


namespace Webform.Controllers
{
    public class SalesAgentsController : Controller
    {
        private readonly WebformContext _context;

        public SalesAgentsController(WebformContext context)
        {
            _context = context;
        }


        public IActionResult SignOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "SignIn");
        }
        // GET: SalesAgents
        public  IActionResult Index()
        {
            return View();
        }

        // GET: SalesAgents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesAgent = await _context.SalesAgent
                .FirstOrDefaultAsync(m => m.ID == id);
            if (salesAgent == null)
            {
                return NotFound();
            }

            return View(salesAgent);
        }

        // GET: SalesAgents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SalesAgents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Email,Password")] SalesAgent salesAgent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salesAgent);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","SignIn");
            }
            return View(salesAgent);
        }

        // GET: SalesAgents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesAgent = await _context.SalesAgent.FindAsync(id);
            if (salesAgent == null)
            {
                return NotFound();
            }
            return View(salesAgent);
        }

        // POST: SalesAgents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Email,Password")] SalesAgent salesAgent)
        {
            if (id != salesAgent.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesAgent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesAgentExists(salesAgent.ID))
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
            return View(salesAgent);
        }

        // GET: SalesAgents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesAgent = await _context.SalesAgent
                .FirstOrDefaultAsync(m => m.ID == id);
            if (salesAgent == null)
            {
                return NotFound();
            }

            return View(salesAgent);
        }

        // POST: SalesAgents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salesAgent = await _context.SalesAgent.FindAsync(id);
            _context.SalesAgent.Remove(salesAgent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesAgentExists(int id)
        {
            return _context.SalesAgent.Any(e => e.ID == id);
        }
    }
}
