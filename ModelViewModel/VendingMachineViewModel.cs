using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using MvcMovie.Models;
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.ModelViewModel;

public class VendingMachineViewModel
{
    public List<Product>? AvailableProducts { get; set; } // Daftar produk yang tersedia di vending machine
    
    public List<Transaction>? TransData { get; set; }
    
    public string? SearchString { get; set; }
    
    // Data untuk dropdown list
    public SelectList? Products { get; set; }
    public SelectList? Users { get; set; }
    // 
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public decimal AmountPaid { get; set; }
    public string? Status { get; set; }
    // 
    public decimal AmountInserted { get; set; }
    [Range(0, double.MaxValue)]
    public decimal? InputAmount { get; set; }
    public void InsertAmount(decimal amount)
    {
        AmountInserted += amount;
    }
    public decimal? UserBalance { get; set; } // Saldo pengguna yang dimasukkan ke mesin
    public string? TransactionStatus { get; set; } // Status transaksi
    public Product? SelectedProduct { get; set; } // Produk yang dipilih oleh pengguna
    public string? ErrorMessage { get; set; } // Pesan error atau info tambahan (misalnya, jika uang tidak cukup)
}
