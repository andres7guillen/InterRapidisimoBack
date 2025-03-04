using AutoMapper;
using CSharpFunctionalExtensions;
using InterRapidisimoDomain.DTOs;
using InterRapidisimoDomain.Repositories;
using MediatR;

namespace InterRapidisimoApplication.Queries;

public class GetSubjectByIdQuery : IRequest<Result<SubjectDto>>
{
    public Guid SubjectId { get; }

    public GetSubjectByIdQuery(Guid subjectId)
    {
        SubjectId = subjectId;
    }

    public class GetSubjectByIdQueryHandler : IRequestHandler<GetSubjectByIdQuery, Result<SubjectDto>>
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;

        public GetSubjectByIdQueryHandler(ISubjectRepository subjectRepository, IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _mapper = mapper;
        }

        public async Task<Result<SubjectDto>> Handle(GetSubjectByIdQuery request, CancellationToken cancellationToken)
        {
            var subject = await _subjectRepository.GetById(request.SubjectId);
            if (subject.HasNoValue)
                return Result.Failure<SubjectDto>("Subject not found.");

            return Result.Success(_mapper.Map<SubjectDto>(subject.Value));
        }
    }

}
