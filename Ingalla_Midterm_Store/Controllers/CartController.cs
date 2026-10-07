using Microsoft.AspNetCore.Mvc;
using Ingalla_Midterm_Store.Data;
using Ingalla_Midterm_Store.Models;

namespace Ingalla_Midterm_Store.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CartController(ApplicationDbContext db)
        {
            _db = db;
        }

        // READ CART
        public IActionResult Index()
        {
            var cartItems = _db.CartItems.ToList();

            return View(cartItems);
        }

        // UPDATE QUANTITY
        [HttpPost]
        public IActionResult UpdateQuantity(int id, int quantity)
        {
            var item = _db.CartItems.Find(id);

            if (item != null)
            {
                if (quantity <= 0)
                {
                    _db.CartItems.Remove(item);
                }
                else
                {
                    item.Quantity = quantity;
                }

                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // DELETE FROM CART
        public IActionResult Remove(int id)
        {
            var item = _db.CartItems.Find(id);

            if (item != null)
            {
                _db.CartItems.Remove(item);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}