using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace MvcMovie.Data
{
    public class MvcMovieContext : DbContext
    {
        public MvcMovieContext (DbContextOptions<MvcMovieContext> options)
            : base(options)
        {}
        public DbSet<Movie> Movie { get; set; } = default!;
        public DbSet<Product> Product { get; set; } = default!;
        public DbSet<User> User { get; set; } = default!;
        public DbSet<Transaction> Transaction { get; set; } = default!;
    }
}
