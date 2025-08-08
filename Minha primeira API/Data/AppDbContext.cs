using Microsoft.EntityFrameworkCore;
using Minha_primeira_API.Models;
using System.Collections.Generic;

namespace Minha_primeira_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Products> Products { get; set; }
    }
}