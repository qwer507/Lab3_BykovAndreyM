using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1.Models;

public partial class Bykov2307a2TechContext : DbContext
{
    public Bykov2307a2TechContext()
    {
    }

    public Bykov2307a2TechContext(DbContextOptions<Bykov2307a2TechContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Registration> Registrations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=dbsrv\\gor2025;Database=Bykov_2307A2_TECH;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Registration>(entity =>
        {
            entity.HasKey(e => e.RId);

            entity.ToTable("registrations");

            entity.Property(e => e.RId).HasColumnName("R_ID");
            entity.Property(e => e.RErrorMessage)
                .HasMaxLength(255)
                .HasColumnName("R_ERROR_MESSAGE");
            entity.Property(e => e.RLogin)
                .HasMaxLength(255)
                .HasColumnName("R_LOGIN");
            entity.Property(e => e.RPassword)
                .HasMaxLength(255)
                .HasColumnName("R_PASSWORD");
            entity.Property(e => e.RRepeatPassword)
                .HasMaxLength(255)
                .HasColumnName("R_REPEAT_PASSWORD");
            entity.Property(e => e.RResult).HasColumnName("R_RESULT");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
