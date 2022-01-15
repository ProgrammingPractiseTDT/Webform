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
    public class CartController : Controller
    {
        private readonly WebformContext _context;

        public CartController(WebformContext context)
        {
            _context = context;
        }

        // GET: Cart
        public async Task<IActionResult> Index()
        {
            //create cart if there is not
            if (ModelState.IsValid && HttpContext.Session.GetInt32("SalesAgentID")!=null)
            {

                var data = _context.Cart.Where(s => s.SalesAgentID == (HttpContext.Session.GetInt32("SalesAgentID")) && s.Ordered == false).ToList();
                if (data.Count() == 0)
                {
                    var cart = new Cart();
                    cart.SalesAgentID = (int)HttpContext.Session.GetInt32("SalesAgentID");
                    cart.Ordered = false;
                    _context.Add(cart);
                    await _context.SaveChangesAsync();
                   
                }
                data = _context.Cart.Where(s => s.SalesAgentID == (HttpContext.Session.GetInt32("SalesAgentID")) && s.Ordered == false).ToList();
                var cartID = data.FirstOrDefault().ID;
                /*var items = await _context.ItemInCart.
                        Where(s => s.CartID == cartID).
                        ToListAsync();
*/
                var ItemAndProduct = await (from item in _context.ItemInCart where item.CartID == cartID
                                            join product in _context.Product on item.ProductID equals product.ID
                                            join img in _context.Image on product.ID equals img.productID
                                            select new ProductInCartViewModel()
                                            {
                                                CartItem = item,
                                                CartProduct = product,
                                                Cost = item.Quantity * product.DeliveryPrice,
                                                ImageSrc = img.src,
                                                CartID = cartID
                                            }

                    )
               .
                ToListAsync();
               
               //*//* ViewData["product"] = products;*/
                return View(  ItemAndProduct);






            }
            else
            {
                return RedirectToAction("Index", "SignIn");
            }
            
        }
        public String getNameAndBrandFromProductID(int ProductID)
        {
            var data = _context.Product.Where(s => s.ID == ProductID).ToList();
            return data.FirstOrDefault().Brand +" "+ data.FirstOrDefault().Name;

        }
        //Add item to cart
        public async Task<ActionResult> AddItem(int ProductID, int Quantity)
        {       if(HttpContext.Session.GetInt32("SalesAgentID") == null)
            {
                return RedirectToAction("Index", "SignIn");
            }
           

                var data = _context.Cart.Where(s => s.SalesAgentID == (HttpContext.Session.GetInt32("SalesAgentID")) && s.Ordered == false).ToList();
                if (data.Count() == 0)
                {
                    var cart = new Cart();
                    cart.SalesAgentID = (int)HttpContext.Session.GetInt32("SalesAgentID");
                    cart.Ordered = false;
                    _context.Add(cart);
                    await _context.SaveChangesAsync();

                }
            data = _context.Cart.Where(s => s.SalesAgentID == (HttpContext.Session.GetInt32("SalesAgentID")) && s.Ordered == false).ToList();

            var cartID = data.FirstOrDefault().ID;

                var itemInCart = new ItemInCart();
                itemInCart.CartID = cartID;
                itemInCart.ProductID = ProductID;
                itemInCart.Quantity = Quantity;

                _context.Add(itemInCart);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Cart");



            
           
        }
        // GET: Cart/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // GET: Cart/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cart/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,SalesAgentID,Ordered")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cart);
        }

        // GET: Cart/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            return View(cart);
        }

        // POST: Cart/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,SalesAgentID,Ordered")] Cart cart)
        {
            if (id != cart.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.ID))
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
            return View(cart);
        }

        // GET: Cart/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Cart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cart = await _context.Cart.FindAsync(id);
            _context.Cart.Remove(cart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(int id)
        {
            return _context.Cart.Any(e => e.ID == id);
        }
    }
}
