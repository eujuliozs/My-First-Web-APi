using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using FirstWebApi.Models;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FirstWebApi.Data
{
    public partial class catalogoapidataContext : DbContext
    {
        public catalogoapidataContext()
        {
        }

        public catalogoapidataContext(DbContextOptions<catalogoapidataContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<Category> Categories { get; set; } = default!;
        public DbSet<Product> Products { get; set; } = default!;
    }
}
