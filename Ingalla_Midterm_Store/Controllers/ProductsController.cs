using Microsoft.AspNetCore.Mvc;
using Ingalla_Midterm_Store.Data;
using Ingalla_Midterm_Store.Models;

namespace Ingalla_Midterm_Store.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductsController(ApplicationDbContext db)
        {
            _db = db;
        }

        // READ
        public IActionResult Index()
        {
            var products = _db.Products.ToList();
            return View(products);
        }

        // CREATE - display form
        public IActionResult Create()
        {
            return View();
        }

        // CREATE - save
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Add(product);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(product);
        }

        // EDIT - display form
        public IActionResult Edit(int id)
        {
            var product = _db.Products.Find(id);

            if (product == null)
            {
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // EDIT - save
        [HttpPost]
        public IActionResult Edit(int id, Product product)
        {
            var existingProduct = _db.Products.Find(id);

            if (existingProduct == null)
            {
                return RedirectToAction("Index");
            }

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.Category = product.Category;

            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // DELETE
        public IActionResult Delete(int id)
        {
            var product = _db.Products.Find(id);

            if (product != null)
            {
                _db.Products.Remove(product);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // ADD TO CART
        public IActionResult AddToCart(int id)
        {
            var product = _db.Products.Find(id);

            if (product == null)
            {
                return RedirectToAction("Index");
            }

            var existingItem = _db.CartItems
                .FirstOrDefault(c => c.ProductId == product.Id);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                var cartItem = new CartItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = 1
                };

                _db.CartItems.Add(cartItem);
            }

            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}