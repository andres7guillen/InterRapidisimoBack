using AutoMapper;
using CSharpFunctionalExtensions;
using InterRapidisimoDomain.DTOs;
using InterRapidisimoDomain.Repositories;
using MediatR;

namespace InterRapidisimoApplication.Queries;

public class GetSubjectsByStudentQuery : IRequest<Result<List<SubjectDto>>>
{
    public Guid StudentId { get; }

    public GetSubjectsByStudentQuery(Guid studentId)
    {
        StudentId = studentId;
    }

    public class GetSubjectsByStudentQueryHandler : IRequestHandler<GetSubjectsByStudentQuery, Result<List<SubjectDto>>>
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;

        public GetSubjectsByStudentQueryHandler(ISubjectRepository subjectRepository, IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<SubjectDto>>> Handle(GetSubjectsByStudentQuery request, CancellationToken cancellationToken)
        {
            var subjects = await _subjectRepository.GetSubjectsByStudentId(request.StudentId);
            if (subjects.HasNoValue)
                return Result.Failure<List<SubjectDto>>("No subjects found for this student");

            return Result.Success(_mapper.Map<List<SubjectDto>>(subjects.Value));
        }
    }
}
