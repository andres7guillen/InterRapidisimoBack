using InterRapidisimoDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InterRapidisimoData.SeedData;

public class SubjectSeeder
{
    public static void SeedSubjects(ModelBuilder modelBuilder)
    {
        var subjects = new List<Subject>
        {
            Subject.CreateSubject("Matemáticas").Value,
            Subject.CreateSubject("Física").Value,
            Subject.CreateSubject("Química").Value,
            Subject.CreateSubject("Historia").Value,
            Subject.CreateSubject("Lengua").Value,
            Subject.CreateSubject("Filosofía").Value,
            Subject.CreateSubject("Arte").Value,
            Subject.CreateSubject("Biología").Value,
            Subject.CreateSubject("Educación Físic").Value,
            Subject.CreateSubject("Informática").Value
        };
        modelBuilder.Entity<Subject>().HasData(subjects);
    }
}
