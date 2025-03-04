using InterRapidisimoApplication.Commands;
using InterRapidisimoApplication.Queries;
using InterRapidisimoApplication.Utilities;
using InterRapidisimoDomain.Repositories;
using InterRapidisimoDomain.Services;
using InterRapidisimoInfrastructure.Repositories;
using InterRapidisimoInfrastructure.Services;
using System.Reflection;

namespace InterRapidisimoApi.Utilities;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterBusinessServices(this IServiceCollection services)
    {
        // Repositorios
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<ISubjectRepository, SubjectRepository>();
        services.AddScoped<IProfessorRepository, ProfessorRepository>();
        services.AddScoped<ICreditProgramRepository, CreditProgramRepository>();
        services.AddScoped<IStudentSubjectRepository, StudentSubjectRepository>();

        // AutoMapper - Registra todos los perfiles
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<StudentProfile>();
            cfg.AddProfile<SubjectProfile>();
            cfg.AddProfile<ProfessorProfile>();
            cfg.AddProfile<StudentSubjectProfile>();
            cfg.AddProfile<ProfessorSubjectProfile>();
            cfg.AddProfile<CreditProgramProfile>();
            cfg.AddProfile<StudentCreditProgramProfile>();
        });

        // Servicios de dominio
        services.AddScoped<IProfessorService, ProfessorService>();

        // MeddiatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
            Assembly.GetExecutingAssembly(),
            typeof(CreateProfessorCommand).Assembly,
            typeof(RegisterStudentCommand).Assembly,
            typeof(CreateSubjectCommand).Assembly,
            typeof(EnrollStudentInCreditProgramCommand).Assembly,
            typeof(GetAllStudentsQuery).Assembly,
            typeof(GetClassmatesQuery).Assembly,
            typeof(GetProfessorByIdQuery).Assembly,
            typeof(GetStudentByIdQuery).Assembly,
            typeof(GetStudentsBySubjectQuery).Assembly,
            typeof(GetSubjectByIdQuery).Assembly,
            typeof(GetSubjectsByProfessorQuery).Assembly
        ));

        return services;
    }
}
