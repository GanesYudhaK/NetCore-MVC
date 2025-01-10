namespace MvcMovie.Models;

public class VendingMachine
{
    public List<Product>? Products { get; set; }  // Daftar produk dalam mesin
    public string? MachineStatus { get; set; }  // Status mesin (aktif, tidak aktif)
    public decimal CurrentBalance { get; set; }  // Saldo mesin (berfungsi jika ingin mengelola uang dalam mesin)
}
