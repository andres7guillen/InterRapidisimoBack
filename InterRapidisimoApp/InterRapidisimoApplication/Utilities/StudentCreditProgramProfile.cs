using AutoMapper;
using InterRapidisimoDomain.DTOs;
using InterRapidisimoDomain.Entities;

namespace InterRapidisimoApplication.Utilities;

public class StudentCreditProgramProfile : Profile
{
    public StudentCreditProgramProfile()
    {
        CreateMap<StudentCreditProgram, StudentCreditProgramDto>();
    }
}
