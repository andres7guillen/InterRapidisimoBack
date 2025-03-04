using AutoMapper;
using InterRapidisimoDomain.DTOs;
using InterRapidisimoDomain.Entities;

namespace InterRapidisimoApplication.Utilities;

public class ProfessorProfile : Profile
{
    public ProfessorProfile()
    {
        CreateMap<Professor, ProfessorDto>();
    }
}
