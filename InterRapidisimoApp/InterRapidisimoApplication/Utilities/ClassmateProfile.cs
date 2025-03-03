using AutoMapper;
using InterRapidisimoDomain.DTOs;
using InterRapidisimoDomain.Entities;

namespace InterRapidisimoApplication.Utilities;

public class ClassmateProfile : Profile
{
    public ClassmateProfile()
    {
        CreateMap<Student, ClassmateDto>();
    }
}
