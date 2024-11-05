using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models;

public class User
{
    [Key]
    public int UserId { get; set; }
    public string? Name { get; set; }
    public decimal Balance { get; set; }  // Saldo pengguna
    public List<Transaction>? PurchaseHistory { get; set; } // Riwayat pembelian
}