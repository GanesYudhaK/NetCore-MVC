using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models;

public class Product
{
    [Key]
    public int IdProduct { get; set; }

    [StringLength(60, MinimumLength = 3), Required]
    public string? Name { get; set; }

    [Range(1, 100), DataType(DataType.Currency), Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
    public int? Quantity {  get; set; }
}