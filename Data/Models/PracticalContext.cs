using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Data.Models;

public partial class PracticalContext : DbContext
{
    public PracticalContext()
    {
    }

    public PracticalContext(DbContextOptions<PracticalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Trainer> Trainers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=.;initial catalog=Practical;user id=sa;password=aptech; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__C7D16A74462E40C5");

            entity.ToTable("Department");

            entity.Property(e => e.DepartmentId).HasColumnName("Department Id");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(50)
                .HasColumnName("Department Name");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__tmp_ms_x__70AFEE798774167A");

            entity.ToTable("Student");

            entity.Property(e => e.StudentId).HasColumnName("Student ID");
            entity.Property(e => e.DepartmentId).HasColumnName("Department ID");
            entity.Property(e => e.StudentEmail)
                .HasMaxLength(50)
                .HasColumnName("Student Email");
            entity.Property(e => e.StudentImages).HasColumnName("Student Images");
            entity.Property(e => e.StudentName)
                .HasMaxLength(50)
                .HasColumnName("Student Name");
            entity.Property(e => e.StudentPhone).HasColumnName("Student Phone");
            entity.Property(e => e.TrainerId).HasColumnName("Trainer Id");

            entity.HasOne(d => d.Department).WithMany(p => p.Students)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Student_ToDepartment");

            entity.HasOne(d => d.Trainer).WithMany(p => p.Students)
                .HasForeignKey(d => d.TrainerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Student_ToTrainer");
        });

        modelBuilder.Entity<Trainer>(entity =>
        {
            entity.HasKey(e => e.TrainerId).HasName("PK__Trainer__13638F14C92DE1D6");

            entity.ToTable("Trainer");

            entity.Property(e => e.TrainerId).HasColumnName("Trainer Id");
            entity.Property(e => e.TrainerName)
                .HasMaxLength(50)
                .HasColumnName("Trainer Name");
            entity.Property(e => e.TrainerPhone).HasColumnName("Trainer Phone");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
