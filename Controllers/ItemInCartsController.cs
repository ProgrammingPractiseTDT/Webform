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
    public class ItemInCartsController : Controller
    {
        private readonly WebformContext _context;

        public ItemInCartsController(WebformContext context)
        {
            _context = context;
        }

        // GET: ItemInCarts
        public async Task<IActionResult> Index()
        {
            return View(await _context.ItemInCart.ToListAsync());
        }

        // GET: ItemInCarts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemInCart = await _context.ItemInCart
                .FirstOrDefaultAsync(m => m.ID == id);
            if (itemInCart == null)
            {
                return NotFound();
            }

            return View(itemInCart);
        }

        // GET: ItemInCarts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ItemInCarts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ProductID,CartID,Quantity")] ItemInCart itemInCart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itemInCart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(itemInCart);
        }

        // GET: ItemInCarts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemInCart = await _context.ItemInCart.FindAsync(id);
            if (itemInCart == null)
            {
                return NotFound();
            }
            return View(itemInCart);
        }

        // POST: ItemInCarts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ProductID,CartID,Quantity")] ItemInCart itemInCart)
        {
            if (id != itemInCart.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemInCart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemInCartExists(itemInCart.ID))
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
            return View(itemInCart);
        }

        // GET: ItemInCarts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemInCart = await _context.ItemInCart
                .FirstOrDefaultAsync(m => m.ID == id);
            if (itemInCart == null)
            {
                return NotFound();
            }

            return View(itemInCart);
        }

        // POST: ItemInCarts/Delete/5
        /*[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]*/
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itemInCart = await _context.ItemInCart.FindAsync(id);
            _context.ItemInCart.Remove(itemInCart);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Cart");
        }

        private bool ItemInCartExists(int id)
        {
            return _context.ItemInCart.Any(e => e.ID == id);
        }
    }
}
