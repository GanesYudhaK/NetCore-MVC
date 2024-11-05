using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models;

public class Transaction
{
    [Key]
    public int TransactionId { get; set; }
    public int UserId { get; set; }  // ID pengguna yang melakukan transaksi
    public int ProductId { get; set; }  // ID produk yang dibeli
    public decimal AmountPaid { get; set; }  // Jumlah yang dibayar
    public string? Status { get; set; }  // Status transaksi
}
