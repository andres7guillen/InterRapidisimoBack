using AutoMapper;
using InterRapidisimoDomain.DTOs;
using InterRapidisimoDomain.Entities;

namespace InterRapidisimoApplication.Utilities;

public class SubjectProfile : Profile
{
    public SubjectProfile()
    {
        CreateMap<Subject, SubjectDto>();
    }
}
