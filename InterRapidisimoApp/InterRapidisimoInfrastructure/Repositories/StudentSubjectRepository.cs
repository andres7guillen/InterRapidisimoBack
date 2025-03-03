using CSharpFunctionalExtensions;
using InterRapidisimoData.Context;
using InterRapidisimoDomain.Entities;
using InterRapidisimoDomain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace InterRapidisimoInfrastructure.Repositories;

public class StudentSubjectRepository : IStudentSubjectRepository
{
    private readonly AppDbContext _context;

    public StudentSubjectRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Result<StudentSubject>> CreateStudentSubjectAsync(StudentSubject studentSubject)
    {
        await _context.StudentSubjects.AddAsync(studentSubject);
        await _context.SaveChangesAsync();
        return Result.Success(studentSubject);
    }
}

