using AutoMapper;
using CSharpFunctionalExtensions;
using InterRapidisimoDomain.DTOs;
using InterRapidisimoDomain.Repositories;
using MediatR;

namespace InterRapidisimoApplication.Queries;

public class GetAllSubjectsQuery : IRequest<Result<List<SubjectDto>>>
{
    public class GetAllSubjectsQueryHandler : IRequestHandler<GetAllSubjectsQuery, Result<List<SubjectDto>>>
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;

        public GetAllSubjectsQueryHandler(ISubjectRepository subjectRepository, IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<SubjectDto>>> Handle(GetAllSubjectsQuery request, CancellationToken cancellationToken)
        {
            var subjects = await _subjectRepository.GetAll();
            var subjectDtos = _mapper.Map<List<SubjectDto>>(subjects.Value);
            return Result.Success(subjectDtos);
        }
    }

}
