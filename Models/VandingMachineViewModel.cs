using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MvcMovie.Models;

public class VandingMachineViewModel
{
    public List<Product>? AvailableProducts { get; set; } // Daftar produk yang tersedia di vending machine
    public decimal? UserBalance { get; set; } // Saldo pengguna yang dimasukkan ke mesin
    public string? TransactionStatus { get; set; } // Status transaksi
    public Product? SelectedProduct { get; set; } // Produk yang dipilih oleh pengguna
    public string? ErrorMessage { get; set; } // Pesan error atau info tambahan (misalnya, jika uang tidak cukup)
}
