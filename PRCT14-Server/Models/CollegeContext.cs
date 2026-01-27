using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PRCT14_Server.Models;

public partial class CollegeContext : DbContext
{
    public CollegeContext()
    {
    }

    public CollegeContext(DbContextOptions<CollegeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AcademicLoad> AcademicLoads { get; set; }

    public virtual DbSet<Discipline> Disciplines { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\sqlexpress; Database=College; User=sa; Password=1234567890; Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AcademicLoad>(entity =>
        {
            entity.HasKey(e => e.AcademLoadCode);

            entity.ToTable("AcademicLoad");

            entity.HasOne(d => d.DisciplineCodeNavigation).WithMany(p => p.AcademicLoads)
                .HasForeignKey(d => d.DisciplineCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_УчебнаяНагрузка_Дисциплины");

            entity.HasOne(d => d.TeacherCodeNavigation).WithMany(p => p.AcademicLoads)
                .HasForeignKey(d => d.TeacherCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_УчебнаяНагрузка_Преподаватели");
        });

        modelBuilder.Entity<Discipline>(entity =>
        {
            entity.HasKey(e => e.DisciplineCode).HasName("PK_Дисциплины");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.ServiceNumber).HasName("PK_Преподаватели");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
