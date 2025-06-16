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

        public DbSet<Users> Users { get; set; }

        public DbSet<Venda> Vendas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Venda>()
                .HasOne(v => v.Users)
                .WithMany(u => u.Vendas)
                .HasForeignKey(v => v.UserId);

            modelBuilder.Entity<Venda>()
                .Property<int>("UserId")
                .HasColumnName("UserId"); 

        }

    }
}