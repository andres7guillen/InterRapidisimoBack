using InterRapidisimoDomain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRapidisimoData.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options){}

        public DbSet<Student> Students { get; set; }
        public DbSet<CreditProgram> CreditPrograms { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<StudentCreditProgram> StudentCreditPrograms { get; set; }
        public DbSet<ProfessorSubject> ProfessorSubjects { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Name).IsRequired().HasMaxLength(100);
                entity.Property(s => s.SurName).IsRequired().HasMaxLength(100);
                entity.Property(s => s.Email).IsRequired().HasMaxLength(255);
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Credits).IsRequired();
            });

            modelBuilder.Entity<StudentCreditProgram>()
                .HasKey(scp => new { scp.StudentId, scp.CreditProgramId });

            modelBuilder.Entity<Professor>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<ProfessorSubject>()
                .HasKey(ps => new { ps.ProfessorId, ps.SubjectId });

            modelBuilder.Entity<StudentSubject>(entity =>
            {
                entity.HasKey(sc => new { sc.StudentId, sc.SubjectId });

                entity.HasOne(sc => sc.Student)
                      .WithMany(s => s.StudentCourses)
                      .HasForeignKey(sc => sc.StudentId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(sc => sc.Subject)
                      .WithMany()
                      .HasForeignKey(sc => sc.SubjectId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(sc => sc.Professor)
                      .WithMany()
                      .HasForeignKey(sc => sc.ProfessorId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
