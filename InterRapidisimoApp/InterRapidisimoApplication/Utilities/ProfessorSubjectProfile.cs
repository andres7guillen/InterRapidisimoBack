using AutoMapper;
using InterRapidisimoDomain.DTOs;
using InterRapidisimoDomain.Entities;

namespace InterRapidisimoApplication.Utilities;

public class ProfessorSubjectProfile : Profile
{
    public ProfessorSubjectProfile()
    {
        CreateMap<ProfessorSubject, ProfessorSubjectDto>();
    }
}
