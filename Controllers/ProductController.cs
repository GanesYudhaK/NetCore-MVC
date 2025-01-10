using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;
using MvcMovie.ModelViewModel;

namespace MvcMovie.Controllers
{
    public class ProductController : Controller
    {
        private readonly MvcMovieContext _context;

        public ProductController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: Product
        // public async Task<IActionResult> Index(string searchString)
        // {
        //     var  products = from p in _context.Product
        //                     select p;

        //     if (!String.IsNullOrEmpty(searchString))
        //     {
        //         products = products.Where(s => s.Name!.ToUpper().Contains(searchString.ToUpper()));
        //     }
        //     var ProductList = new VendingMachineViewModel
        //     {
        //         AvailableProducts = await products.ToListAsync()
        //     };
        //     return View(ProductList);
        // }

         private static List<Product> products = new List<Product>
        {
            new Product { IdProduct = 1, Name = "Coke", Price = 2.00m, Quantity = 10 },
            new Product { IdProduct = 2, Name = "Pepsi", Price = 1.50m, Quantity = 8 },
            new Product { IdProduct = 3, Name = "Chips", Price = 1.00m, Quantity = 5 }
        };

        public IActionResult Index()
        {
            return View(products); // Menampilkan semua produk
        }

        int userId = int.Parse(User.FindFirst("UserId").Value);

        [HttpPost]
        public IActionResult ProcessTransaction(int productId, decimal amountInserted, int userId)
        {
            
            var product = products.FirstOrDefault(p => p.IdProduct == productId);
            var user = User.FirstOrDefault(u => u.UserId == userId);

            if (product == null || user == null)
            {
                return NotFound("Product or User not found.");
            }

            decimal change = amountInserted - product.Price;
            bool isSuccess = change >= 0; // Transaksi berhasil jika uang yang dimasukkan cukup

            // Menyimpan transaksi untuk pembelian produk
            var transaction = new Transaction
            {
                TransactionId = transactions.Count + 1,  // Asumsikan ID transaksi berurutan
                UserId = user.UserId,
                ProductId = product.IdProduct,
                Amount = amountInserted,
                Date = DateTime.Now,
                BalanceAfterTransaction = user.Balance - product.Price + change,
                TransactionType = "Pembelian",
                User = user,
                Product = product
            };

            // Jika transaksi sukses, update saldo pengguna dan stok produk
            if (isSuccess)
            {
                user.Balance -= product.Price; // Mengurangi saldo pengguna
                user.Balance += change; // Menambahkan kembalian ke saldo pengguna
                product.Quantity--; // Mengurangi stok produk
                transactions.Add(transaction); // Menyimpan transaksi
            }

            // Simpan pesan transaksi di TempData
            TempData["TransactionMessage"] = isSuccess ? $"You have successfully purchased {product.Name}. Your change: {change}. New balance: {user.Balance}" : "Not enough money inserted.";

            return RedirectToAction("Index"); // Redirect kembali ke halaman utama
        }

        [HttpPost]
        public IActionResult AddBalance(decimal amountInserted, int userId)
        {
            var user = user.FirstOrDefault(u => u.UserId == userId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Menambahkan saldo
            user.Balance += amountInserted;

            // Menyimpan transaksi penambahan saldo
            var transaction = new Transaction
            {
                TransactionId = transactions.Count + 1,  // Asumsikan ID transaksi berurutan
                UserId = user.UserId,
                Amount = amountInserted,
                Date = DateTime.Now,
                BalanceAfterTransaction = user.Balance,
                TransactionType = "Penambahan Saldo",
                User = user
            };

            transactions.Add(transaction); // Menyimpan transaksi penambahan saldo

            // Simpan pesan transaksi di TempData
            TempData["TransactionMessage"] = $"You have successfully added {amountInserted} to your balance. New balance: {user.Balance}";

            return RedirectToAction("Index"); // Redirect kembali ke halaman utama
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.IdProduct == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public IActionResult Create()
        {
            return View();
        }

        // Done
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProduct,Name,Price,Quantity")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // Err
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Product product)
        {
            if (id != product.IdProduct)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.IdProduct))
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
            return View(product);
        }

        // Uncomplete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.IdProduct == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.IdProduct == id);
        }
    }
}
