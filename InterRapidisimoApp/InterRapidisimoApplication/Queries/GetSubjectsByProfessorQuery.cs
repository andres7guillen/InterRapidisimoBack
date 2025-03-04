using AutoMapper;
using CSharpFunctionalExtensions;
using InterRapidisimoDomain.DTOs;
using InterRapidisimoDomain.Repositories;
using MediatR;

namespace InterRapidisimoApplication.Queries;

public class GetSubjectsByProfessorQuery : IRequest<Result<List<SubjectDto>>>
{
    public Guid ProfessorId { get; }

    public GetSubjectsByProfessorQuery(Guid professorId)
    {
        ProfessorId = professorId;
    }

    public class GetSubjectsByProfessorQueryHandler : IRequestHandler<GetSubjectsByProfessorQuery, Result<List<SubjectDto>>>
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;

        public GetSubjectsByProfessorQueryHandler(ISubjectRepository subjectRepository, IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<SubjectDto>>> Handle(GetSubjectsByProfessorQuery request, CancellationToken cancellationToken)
        {
            var subjects = await _subjectRepository.GetSubjectsByProfessorId(request.ProfessorId);
            if(subjects.IsFailure)
                return Result.Failure<List<SubjectDto>>("There are not subjects that professor teach");
            return Result.Success(_mapper.Map<List<SubjectDto>>(subjects.Value));
        }
    }

}
