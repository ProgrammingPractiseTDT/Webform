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
    public class DeliverySlipsController : Controller
    {
        private readonly WebformContext _context;

        public DeliverySlipsController(WebformContext context)
        {
            _context = context;
        }

        // GET: DeliverySlips
        public async Task<IActionResult> Index()
        {
            return View(await _context.DeliverySlip.ToListAsync());
        }

        // GET: DeliverySlips/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliverySlip = await _context.DeliverySlip
                .FirstOrDefaultAsync(m => m.ID == id);
            if (deliverySlip == null)
            {
                return NotFound();
            }

            return View(deliverySlip);
        }

        // GET: DeliverySlips/Create
       /* public IActionResult Create()
        {
            return View();
        }*/

        // POST: DeliverySlips/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]*/
        public async Task<IActionResult> Create( int CartID, string ShipAdress, bool Payment)
        {
            if (ModelState.IsValid)
            {

                var deliverySlip = new DeliverySlip();
                deliverySlip.CartID = CartID;
                deliverySlip.ShipAdress = ShipAdress;
                deliverySlip.Payment = Payment;
                _context.Add(deliverySlip);

                var cart = _context.Cart.Where(s => s.ID == CartID).FirstOrDefault();
                cart.Ordered = true;
                _context.Update(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: DeliverySlips/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliverySlip = await _context.DeliverySlip.FindAsync(id);
            if (deliverySlip == null)
            {
                return NotFound();
            }
            return View(deliverySlip);
        }

        // POST: DeliverySlips/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CartID,ShipAdress,status,Payment")] DeliverySlip deliverySlip)
        {
            if (id != deliverySlip.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deliverySlip);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeliverySlipExists(deliverySlip.ID))
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
            return View(deliverySlip);
        }

        // GET: DeliverySlips/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliverySlip = await _context.DeliverySlip
                .FirstOrDefaultAsync(m => m.ID == id);
            if (deliverySlip == null)
            {
                return NotFound();
            }

            return View(deliverySlip);
        }

        // POST: DeliverySlips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deliverySlip = await _context.DeliverySlip.FindAsync(id);
            _context.DeliverySlip.Remove(deliverySlip);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeliverySlipExists(int id)
        {
            return _context.DeliverySlip.Any(e => e.ID == id);
        }
    }
}
