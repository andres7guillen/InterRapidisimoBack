using InterRapidisimoData.SeedData;
using InterRapidisimoDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InterRapidisimoData.Context;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Student> Students { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Professor> Professors { get; set; }
    public DbSet<CreditProgram> CreditPrograms { get; set; }
    public DbSet<StudentSubject> StudentSubjects { get; set; }
    public DbSet<StudentCreditProgram> StudentCreditPrograms { get; set; }
    public DbSet<ProfessorSubject> ProfessorSubjects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StudentSubject>()
        .HasKey(ss => new { ss.StudentId, ss.SubjectId });

        modelBuilder.Entity<StudentSubject>()
        .HasOne(ss => ss.Student)
        .WithMany(s => s.StudentSubjects)
        .HasForeignKey(ss => ss.StudentId)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<StudentSubject>()
        .HasOne(ss => ss.Subject)
        .WithMany(s => s.StudentSubjects)
        .HasForeignKey(ss => ss.SubjectId)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<StudentSubject>()
        .HasOne(ss => ss.Professor)
        .WithMany(p => p.StudentSubjects)
        .HasForeignKey(ss => ss.ProfessorId)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProfessorSubject>()
        .HasKey(ps => new { ps.ProfessorId, ps.SubjectId });

        modelBuilder.Entity<ProfessorSubject>()
            .HasOne(ps => ps.Professor)
            .WithMany(p => p.ProfessorSubjects)
            .HasForeignKey(ps => ps.ProfessorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProfessorSubject>()
            .HasOne(ps => ps.Subject)
            .WithMany(s => s.ProfessorSubjects)
            .HasForeignKey(ps => ps.SubjectId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<StudentCreditProgram>()
        .HasKey(scp => new { scp.StudentId, scp.CreditProgramId });

        modelBuilder.Entity<StudentCreditProgram>()
            .HasOne(scp => scp.Student)
            .WithMany(s => s.StudentCreditPrograms)
            .HasForeignKey(scp => scp.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<StudentCreditProgram>()
            .HasOne(scp => scp.CreditProgram)
            .WithMany(cp => cp.StudentCreditPrograms)
            .HasForeignKey(scp => scp.CreditProgramId)
            .OnDelete(DeleteBehavior.Cascade);

        SubjectSeeder.SeedSubjects(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }
}
