using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        public int? UserId { get; set; }
        public int? ProductId { get; set; } // Nullable, karena tidak semua transaksi melibatkan barang
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public decimal BalanceAfterTransaction { get; set; } // Menyimpan saldo setelah transaksi
        public string? TransactionType { get; set; } // "Penambahan Saldo" atau "Pembelian"

        public User? User { get; set; }
        public Product? Product { get; set; } // Nullable jika transaksi adalah penambahan saldo
    }
}