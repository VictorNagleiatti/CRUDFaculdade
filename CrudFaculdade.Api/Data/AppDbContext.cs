using System;
using System.Collections.Generic;
using CrudFaculdade.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudFaculdade.Api.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Produto> Produtos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=RS-DS08\\SQLEXPRESS;Database=CrudFaculdade;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Produto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Produtos__3214EC07680EAF5E");

            entity.Property(e => e.Ativo).HasDefaultValue(true);
            entity.Property(e => e.Categoria).HasMaxLength(60);
            entity.Property(e => e.CriadoEm).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Nome).HasMaxLength(120);
            entity.Property(e => e.Preco).HasColumnType("decimal(10, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
